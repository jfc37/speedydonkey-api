using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using ActionHandlers;
using ActionHandlersTests.Builders.MockBuilders;
using Actions;
using Data.Tests.Builders.MockBuilders;
using Models;
using Moq;
using NUnit.Framework;
using SpeedyDonkeyApi.Controllers;
using SpeedyDonkeyApi.Models;
using SpeedyDonkeyApi.Tests.Builders;
using SpeedyDonkeyApi.Tests.Builders.MockBuilders;

namespace SpeedyDonkeyApi.Tests.Controllers
{
    [TestFixture]
    public class ProfessorApiControllerTestFixture
    {
        private ProfessorApiControllerBuilder _professorApiControllerBuilder;
        private MockPersonRepositoryBuilder<Professor> _personRepositoryBuilder;
        private MockActionHandlerOverlordBuilder _actionHandlerOverlordBuilder;
        private MockModelFactoryBuilder _modelFactoryBuilder;
        private HttpRequestMessageBuilder _httpRequestMessageBuilder;
        private MockSearchBuilder<Professor> _searchBuilder;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _professorApiControllerBuilder = new ProfessorApiControllerBuilder();
            _personRepositoryBuilder = new MockPersonRepositoryBuilder<Professor>();
            _actionHandlerOverlordBuilder = new MockActionHandlerOverlordBuilder();
            _modelFactoryBuilder = new MockModelFactoryBuilder().WithEntityToModelMapping();
            _httpRequestMessageBuilder = new HttpRequestMessageBuilder()
                .WithResponseAttached();
            _searchBuilder = new MockSearchBuilder<Professor>();
        }

        private ProfessorApiController GetController()
        {
            var controller = _professorApiControllerBuilder
                .WithRepository(_personRepositoryBuilder.BuildObject())
                .WithActionHandlerOverlord(_actionHandlerOverlordBuilder.BuildObject())
                .WithModelFactory(_modelFactoryBuilder.BuildObject())
                .WithEntitySearch(_searchBuilder.BuildObject())
                .BuildProfessorApiController();

            controller.ControllerContext.Request = _httpRequestMessageBuilder.Build();

            return controller;
        }

        public class Get : ProfessorApiControllerTestFixture
        {
            [Test]
            public void It_should_return_status_code_not_found_when_no_persons_exists()
            {
                _personRepositoryBuilder.WithNoPeople();

                var controller = GetController();
                var response = controller.Get();

                Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            }

            [Test]
            public void It_should_return_status_code_ok_when_persons_exist()
            {
                _personRepositoryBuilder
                    .WithSomeValidPerson()
                    .WithSomeValidPerson()
                    .WithSomeValidPerson()
                    .WithSomeValidPerson();

                var controller = GetController();
                var response = controller.Get();

                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }

            [Test]
            public void It_should_return_all_persons_when_persons_exist()
            {
                _personRepositoryBuilder
                    .WithSomeValidPerson()
                    .WithSomeValidPerson()
                    .WithSomeValidPerson()
                    .WithSomeValidPerson();

                var controller = GetController();
                var response = controller.Get();

                IEnumerable<PersonModel> people;
                Assert.IsTrue(response.TryGetContentValue(out people));
                Assert.AreEqual(4, people.Count());
            }

            [Test]
            public void It_should_return_status_code_not_found_when_no_persons_match_id()
            {
                _modelFactoryBuilder.WithModelMappingReturningNull();
                const int personId = 5;
                _personRepositoryBuilder.WithPerson(new Professor { Id = 1 });

                var controller = GetController();
                var response = controller.Get(personId);

                Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            }

            [Test]
            public void It_should_return_status_code_ok_when_people_exist()
            {
                const int personId = 5;
                _personRepositoryBuilder.WithPerson(new Professor { Id = personId });
                _modelFactoryBuilder.WithEntityToModelMapping();

                var controller = GetController();
                var response = controller.Get(personId);

                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }

            [Test]
            public void It_should_return_person_with_matching_id()
            {
                const int personId = 5;
                _personRepositoryBuilder.WithPerson(new Professor { Id = personId });
                _modelFactoryBuilder.WithEntityToModelMapping();

                var controller = GetController();
                var response = controller.Get(personId);

                PersonModel professorInResponse;
                Assert.IsTrue(response.TryGetContentValue(out professorInResponse));
                Assert.AreEqual(personId, professorInResponse.Id);
            }
        }

        public class GetWithQuery : ProfessorApiControllerTestFixture
        {
            private string _q;

            private HttpResponseMessage PerformAction()
            {
                var controller = GetController();
                return controller.Get(_q);
            }

            [Test]
            public void It_should_return_status_code_ok_when_query_has_matches()
            {
                _searchBuilder.WithQueryMatchings();

                var response = PerformAction();

                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }

            [Test]
            public void It_should_return_status_code_not_found_when_query_has_no_matches()
            {
                _searchBuilder.WithNoQueryMatchings();

                var response = PerformAction();

                Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            }
        }

        public class Post : ProfessorApiControllerTestFixture
        {
            private ProfessorModel _professor;
            private int _userId;

            [SetUp]
            public void Setup()
            {
                _professor = new ProfessorModel();
                _userId = 44;

                _actionHandlerOverlordBuilder.WithNoErrorsOnHandling<CreatePerson, Person>();
                _modelFactoryBuilder.WithModelParsing();
            }

            [Test]
            public void It_should_return_status_code_bad_request_when_person_is_invalid()
            {
                _actionHandlerOverlordBuilder.WithErrorsOnHandling<CreatePerson, Person>();

                var controller = GetController();
                var response = controller.Post(_userId, _professor);

                Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            }

            [Test]
            public void It_should_return_validation_errors_when_person_is_invalid()
            {
                _actionHandlerOverlordBuilder.WithErrorsOnHandling<CreatePerson, Person>();

                var controller = GetController();
                var response = controller.Post(_userId, _professor);

                ActionReponse<PersonModel> actionReponse;
                Assert.IsTrue(response.TryGetContentValue(out actionReponse));
                Assert.IsFalse(actionReponse.ValidationResult.IsValid);
            }

            [Test]
            public void It_should_return_no_validation_errors_when_person_is_valid()
            {
                _actionHandlerOverlordBuilder.WithNoErrorsOnHandling<CreateUser, User>();

                var controller = GetController();
                var response = controller.Post(_userId, _professor);

                ActionReponse<PersonModel> actionReponse;
                Assert.IsTrue(response.TryGetContentValue(out actionReponse));
                Assert.IsTrue(actionReponse.ValidationResult.IsValid);
            }
        }

        public class Put : ProfessorApiControllerTestFixture
        {
            private ProfessorModel _professorModel;
            private int _professorId;

            [SetUp]
            public void Setup()
            {
                _professorId = 1;
                _professorModel = new ProfessorModel { Id = _professorId };

                _actionHandlerOverlordBuilder.WithNoErrorsOnHandling<UpdateProfessor, Professor>();
                _modelFactoryBuilder
                    .WithEntityToModelMapping()
                    .WithModelParsing();
            }

            [Test]
            public void It_should_return_status_code_bad_request_when_professor_is_invalid()
            {
                _actionHandlerOverlordBuilder.WithErrorsOnHandling<UpdateProfessor, Professor>();

                var controller = GetController();
                var response = controller.Put(_professorId, _professorModel);

                Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            }

            [Test]
            public void It_should_return_validation_errors_when_professor_is_invalid()
            {
                _actionHandlerOverlordBuilder.WithErrorsOnHandling<UpdateProfessor, Professor>();

                var controller = GetController();
                var response = controller.Put(_professorId, _professorModel);

                ActionReponse<ProfessorModel> actionReponse;
                Assert.IsTrue(response.TryGetContentValue(out actionReponse));
                Assert.IsFalse(actionReponse.ValidationResult.IsValid);
            }

            [Test]
            public void It_should_return_status_code_ok_when_professor_is_valid()
            {
                _actionHandlerOverlordBuilder.WithNoErrorsOnHandling<UpdateProfessor, Professor>();

                var controller = GetController();
                var response = controller.Put(_professorId, _professorModel);

                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }

            [Test]
            public void It_should_return_no_validation_errors_when_professor_is_valid()
            {
                _actionHandlerOverlordBuilder.WithNoErrorsOnHandling<UpdateProfessor, Professor>();

                var controller = GetController();
                var response = controller.Put(_professorId, _professorModel);

                ActionReponse<ProfessorModel> actionReponse;
                Assert.IsTrue(response.TryGetContentValue(out actionReponse));
                Assert.IsTrue(actionReponse.ValidationResult.IsValid);
            }
        }

        public class Delete : ProfessorApiControllerTestFixture
        {
            private int _personId;

            [SetUp]
            public void Setup()
            {
                _personId = 54;
                _personRepositoryBuilder.WithPerson(new Professor {Id = _personId});
                _actionHandlerOverlordBuilder.WithNoErrorsOnHandling<DeletePerson, Person>();
            }

            [Test]
            public void It_should_delete_person_in_database()
            {
                var controller = GetController();
                controller.Delete(_personId);

                _actionHandlerOverlordBuilder.Mock.Verify(
                    x => x.HandleAction<DeletePerson, Person>(It.IsAny<DeletePerson>()), Times.Once);
            }

            [Test]
            public void It_should_return_status_code_ok_on_successful_delete()
            {
                var controller = GetController();
                var response = controller.Delete(_personId);

                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }

            [Test]
            public void It_should_return_status_code_not_found_when_person_doesnt_exist()
            {
                _personRepositoryBuilder.WithNoPeople();

                var controller = GetController();
                var response = controller.Delete(_personId);

                Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            }
        }
    }
}
