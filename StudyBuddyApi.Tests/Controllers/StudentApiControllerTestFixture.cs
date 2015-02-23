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
    public class StudentApiControllerTestFixture
    {
        private StudentApiControllerBuilder _studentApiControllerBuilder;
        private MockPersonRepositoryBuilder<Student> _personRepositoryBuilder;
        private MockActionHandlerOverlordBuilder _actionHandlerOverlordBuilder;
        private MockModelFactoryBuilder _modelFactoryBuilder;
        private HttpRequestMessageBuilder _httpRequestMessageBuilder;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _studentApiControllerBuilder = new StudentApiControllerBuilder();
            _personRepositoryBuilder = new MockPersonRepositoryBuilder<Student>();
            _actionHandlerOverlordBuilder = new MockActionHandlerOverlordBuilder();
            _modelFactoryBuilder = new MockModelFactoryBuilder()
                .WithEntityToModelMapping()
                .WithModelParsing();
            _httpRequestMessageBuilder = new HttpRequestMessageBuilder()
                .WithResponseAttached();
        }

        private StudentApiController GetController()
        {
            var controller = _studentApiControllerBuilder
                .WithRepository(_personRepositoryBuilder.BuildObject())
                .WithActionHandlerOverlord(_actionHandlerOverlordBuilder.BuildObject())
                .WithModelFactory(_modelFactoryBuilder.BuildObject())
                .BuildStudentApiController();

            controller.ControllerContext.Request = _httpRequestMessageBuilder.Build();

            return controller;
        }

        public class Get : StudentApiControllerTestFixture
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
                _personRepositoryBuilder.WithPerson(new Student { Id = 1 });

                var controller = GetController();
                var response = controller.Get(personId);

                Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            }

            [Test]
            public void It_should_return_status_code_ok_when_people_exist()
            {
                const int personId = 5;
                _personRepositoryBuilder.WithPerson(new Student { Id = personId });
                _modelFactoryBuilder.WithEntityToModelMapping();

                var controller = GetController();
                var response = controller.Get(personId);

                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }

            [Test]
            public void It_should_return_person_with_matching_id()
            {
                const int personId = 5;
                _personRepositoryBuilder.WithPerson(new Student { Id = personId });
                _modelFactoryBuilder.WithEntityToModelMapping();

                var controller = GetController();
                var response = controller.Get(personId);

                PersonModel studentInResponse;
                Assert.IsTrue(response.TryGetContentValue(out studentInResponse));
                Assert.AreEqual(personId, studentInResponse.Id);
            }
        }

        public class Post : StudentApiControllerTestFixture
        {
            private StudentModel _student;
            private int _userId;

            [SetUp]
            public void Setup()
            {
                _student = new StudentModel();
                _userId = 44;

                _actionHandlerOverlordBuilder.WithNoErrorsOnHandling<CreatePerson, Person>();
                _modelFactoryBuilder.WithModelParsing();
            }

            [Test]
            public void It_should_return_status_code_bad_request_when_person_is_invalid()
            {
                _actionHandlerOverlordBuilder.WithErrorsOnHandling<CreatePerson, Person>();

                var controller = GetController();
                var response = controller.Post(_userId, _student);

                Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            }

            [Test]
            public void It_should_return_validation_errors_when_person_is_invalid()
            {
                _actionHandlerOverlordBuilder.WithErrorsOnHandling<CreatePerson, Person>();

                var controller = GetController();
                var response = controller.Post(_userId, _student);

                ActionReponse<PersonModel> actionReponse;
                Assert.IsTrue(response.TryGetContentValue(out actionReponse));
                Assert.IsFalse(actionReponse.ValidationResult.IsValid);
            }

            [Test]
            public void It_should_return_no_validation_errors_when_person_is_valid()
            {
                _actionHandlerOverlordBuilder.WithNoErrorsOnHandling<CreateUser, User>();

                var controller = GetController();
                var response = controller.Post(_userId, _student);

                ActionReponse<PersonModel> actionReponse;
                Assert.IsTrue(response.TryGetContentValue(out actionReponse));
                Assert.IsTrue(actionReponse.ValidationResult.IsValid);
            }
        }

        public class Put : StudentApiControllerTestFixture
        {
            private StudentModel _studentModel;
            private int _studentId;

            [SetUp]
            public void Setup()
            {
                _studentId = 1;
                _studentModel = new StudentModel { Id = _studentId };

                _actionHandlerOverlordBuilder.WithNoErrorsOnHandling<UpdateStudent, Student>();
                _modelFactoryBuilder.WithModelParsing();
            }

            [Test]
            public void It_should_return_status_code_bad_request_when_student_is_invalid()
            {
                _actionHandlerOverlordBuilder.WithErrorsOnHandling<UpdateStudent, Student>();

                var controller = GetController();
                var response = controller.Put(_studentId, _studentModel);

                Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            }

            [Test]
            public void It_should_return_validation_errors_when_student_is_invalid()
            {
                _actionHandlerOverlordBuilder.WithErrorsOnHandling<UpdateStudent, Student>();

                var controller = GetController();
                var response = controller.Put(_studentId, _studentModel);

                ActionReponse<StudentModel> actionReponse;
                Assert.IsTrue(response.TryGetContentValue(out actionReponse));
                Assert.IsFalse(actionReponse.ValidationResult.IsValid);
            }

            [Test]
            public void It_should_return_status_code_ok_when_student_is_valid()
            {
                _actionHandlerOverlordBuilder.WithNoErrorsOnHandling<UpdateStudent, Student>();

                var controller = GetController();
                var response = controller.Put(_studentId, _studentModel);

                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }

            [Test]
            public void It_should_return_no_validation_errors_when_student_is_valid()
            {
                _actionHandlerOverlordBuilder.WithNoErrorsOnHandling<UpdateStudent, Student>();

                var controller = GetController();
                var response = controller.Put(_studentId, _studentModel);

                ActionReponse<StudentModel> actionReponse;
                Assert.IsTrue(response.TryGetContentValue(out actionReponse));
                Assert.IsTrue(actionReponse.ValidationResult.IsValid);
            }
        }

        public class EnrolInCoursePost : StudentApiControllerTestFixture
        {
            private int _courseId;
            private int _studentId;

            [SetUp]
            public void Setup()
            {
                _modelFactoryBuilder.WithEntityToModelMapping();
            }

            [Test]
            public void It_should_return_status_code_bad_request_when_enrolment_fails()
            {
                _actionHandlerOverlordBuilder.WithErrorsOnHandling<EnrolStudent, Student>();

                var controller = GetController();
                var response = controller.Post(_studentId, _courseId);

                Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            }

            [Test]
            public void It_should_return_validation_errors_when_enrolment_fails()
            {
                _actionHandlerOverlordBuilder.WithErrorsOnHandling<EnrolStudent, Student>();

                var controller = GetController();
                var response = controller.Post(_studentId, _courseId);

                ActionReponse<StudentModel> actionReponse;
                Assert.IsTrue(response.TryGetContentValue(out actionReponse));
                Assert.IsFalse(actionReponse.ValidationResult.IsValid);
            }

            [Test]
            public void It_should_return_status_code_OK_when_enrolment_succeeds()
            {
                _actionHandlerOverlordBuilder.WithNoErrorsOnHandling<EnrolStudent, Student>();

                var controller = GetController();
                var response = controller.Post(_studentId, _courseId);

                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }

            [Test]
            public void It_should_return_no_validation_errors_when_enrolment_succeeds()
            {
                _actionHandlerOverlordBuilder.WithNoErrorsOnHandling<EnrolStudent, Student>();

                var controller = GetController();
                var response = controller.Post(_studentId, _courseId);

                ActionReponse<StudentModel> actionReponse;
                Assert.IsTrue(response.TryGetContentValue(out actionReponse));
                Assert.IsTrue(actionReponse.ValidationResult.IsValid);
            }
        }

        public class Delete : StudentApiControllerTestFixture
        {
            private int _studentId;
            private int _courseId;

            [SetUp]
            public void Setup()
            {
                _studentId = 1;
                _courseId = 2;
                _actionHandlerOverlordBuilder.WithNoErrorsOnHandling<UnenrolStudent, Student>();
            }

            [Test]
            public void It_should_delete_student_in_database()
            {
                _personRepositoryBuilder.WithPerson(new Student { Id = _studentId, EnroledCourses = new []{new Course{Id = _courseId}}});

                var controller = GetController();
                var response = controller.Delete(_studentId, _courseId);

                _actionHandlerOverlordBuilder.Mock.Verify(
                    x => x.HandleAction<UnenrolStudent, Student>(It.IsAny<UnenrolStudent>()), Times.Once);
            }

            [Test]
            public void It_should_return_status_code_ok_on_successful_delete()
            {
                _personRepositoryBuilder.WithPerson(new Student { Id = _studentId, EnroledCourses = new[] { new Course { Id = _courseId } } });

                var controller = GetController();
                var response = controller.Delete(_studentId, _courseId);

                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }

            [Test]
            public void It_should_return_status_code_not_found_when_student_doesnt_exist()
            {
                _personRepositoryBuilder = new MockPersonRepositoryBuilder<Student>()
                    .WithNoPeople();

                var controller = GetController();
                var response = controller.Delete(_studentId, _courseId);

                Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            }
        }
    }
}
