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
    public class AssignmentApiControllerTestFixture
    {
        private AssignmentApiControllerBuilder _controllerBuilder;
        private MockAssignmentRepositoryBuilder _assignmentRepositoryBuilder;
        private MockActionHandlerOverlordBuilder _actionHandlerOverlordBuilder;
        private MockModelFactoryBuilder _modelFactoryBuilder;
        private HttpRequestMessageBuilder _httpRequestMessageBuilder;
        private MockSearchBuilder<Assignment> _searchBuilder;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _controllerBuilder = new AssignmentApiControllerBuilder();
            _assignmentRepositoryBuilder = new MockAssignmentRepositoryBuilder();
            _actionHandlerOverlordBuilder = new MockActionHandlerOverlordBuilder();
            _modelFactoryBuilder = new MockModelFactoryBuilder()
                .WithEntityToModelMapping()
                .WithModelParsing();
            _httpRequestMessageBuilder = new HttpRequestMessageBuilder()
                .WithResponseAttached();
            _searchBuilder = new MockSearchBuilder<Assignment>();
        }

        private AssignmentApiController GetController()
        {
            var controller = _controllerBuilder
                .WithAssignmentRepository(_assignmentRepositoryBuilder.BuildObject())
                .WithActionHandlerOverlord(_actionHandlerOverlordBuilder.BuildObject())
                .WithModelFactory(_modelFactoryBuilder.BuildObject())
                .WithEntitySearch(_searchBuilder.BuildObject())
                .Build();

            controller.ControllerContext.Request = _httpRequestMessageBuilder.Build();

            return controller;
        }

        public class Get : AssignmentApiControllerTestFixture
        {
            private int _courseId;

            [SetUp]
            public void Setup()
            {
                _assignmentRepositoryBuilder = new MockAssignmentRepositoryBuilder();
                _modelFactoryBuilder = new MockModelFactoryBuilder()
                    .WithEntityToModelMapping()
                    .WithModelParsing();

                _courseId = 454;
            }

            [Test]
            public void It_should_return_status_code_not_found_when_no_assignments_exists()
            {
                _assignmentRepositoryBuilder.WithNoAssignments();

                var controller = GetController();
                var response = controller.Get(_courseId);

                Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            }

            [Test]
            public void It_should_return_status_code_ok_when_assignments_exist()
            {
                _assignmentRepositoryBuilder
                    .WithSomeValidAssignment()
                    .WithSomeValidAssignment()
                    .WithSomeValidAssignment()
                    .WithSomeValidAssignment();

                var controller = GetController();
                var response = controller.Get(_courseId);

                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }

            [Test]
            public void It_should_return_all_assignments_when_assignments_exist()
            {
                _assignmentRepositoryBuilder
                    .WithSomeValidAssignment()
                    .WithSomeValidAssignment()
                    .WithSomeValidAssignment()
                    .WithSomeValidAssignment();

                var controller = GetController();
                var response = controller.Get(_courseId);

                IEnumerable<AssignmentModel> assignmentsInResponse;
                Assert.IsTrue(response.TryGetContentValue(out assignmentsInResponse));
                Assert.AreEqual(4, assignmentsInResponse.Count());
            }

            [Test]
            public void It_should_return_status_code_not_found_when_no_assignments_match_id()
            {
                _modelFactoryBuilder.WithModelMappingReturningNull();
                const int assignmentId = 5;
                _assignmentRepositoryBuilder.WithAssignment(new Assignment{Id = 1});

                var controller = GetController();
                var response = controller.Get(_courseId, assignmentId);

                Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            }

            [Test]
            public void It_should_return_status_code_ok_when_assignment_exists()
            {
                const int assignmentId = 5;
                _assignmentRepositoryBuilder.WithAssignment(new Assignment{Id = assignmentId});

                var controller = GetController();
                var response = controller.Get(assignmentId);

                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }

            [Test]
            public void It_should_return_assignment_with_matching_id()
            {
                const int assignmentId = 5;
                _assignmentRepositoryBuilder.WithAssignment(new Assignment{Id = assignmentId});

                var controller = GetController();
                var response = controller.Get(_courseId, assignmentId);

                AssignmentModel assignmentInResponse;
                Assert.IsTrue(response.TryGetContentValue(out assignmentInResponse));
                Assert.AreEqual(assignmentId, assignmentInResponse.Id);
            }
        }

        public class GetWithQuery : AssignmentApiControllerTestFixture
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
        public class Post : AssignmentApiControllerTestFixture
        {
            private AssignmentModel _assignmentModel;
            private int _courseId;

            [SetUp]
            public void Setup()
            {
                _assignmentModel = new AssignmentModel { Id = 54 };
                _courseId = 54;

                _modelFactoryBuilder.WithModelParsing();
                _actionHandlerOverlordBuilder.WithNoErrorsOnHandling<CreateAssignment, Assignment>();
            }

            [Test]
            public void It_should_return_status_code_bad_request_when_assignment_is_invalid()
            {
                _actionHandlerOverlordBuilder.WithErrorsOnHandling<CreateAssignment, Assignment>();

                var controller = GetController();
                var response = controller.Post(_courseId, _assignmentModel);

                Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            }

            [Test]
            public void It_should_return_validation_errors_when_assignment_is_invalid()
            {
                _actionHandlerOverlordBuilder.WithErrorsOnHandling<CreateAssignment, Assignment>();

                var controller = GetController();
                var response = controller.Post(_courseId, _assignmentModel);

                ActionReponse<AssignmentModel> actionReponse;
                Assert.IsTrue(response.TryGetContentValue(out actionReponse));
                Assert.IsFalse(actionReponse.ValidationResult.IsValid);
            }

            [Test]
            public void It_should_return_status_code_created_when_assignment_is_valid()
            {
                _actionHandlerOverlordBuilder.WithNoErrorsOnHandling<CreateAssignment, Assignment>();

                var controller = GetController();
                var response = controller.Post(_courseId, _assignmentModel);

                Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            }

            [Test]
            public void It_should_return_no_validation_errors_when_assignment_is_valid()
            {
                _actionHandlerOverlordBuilder.WithNoErrorsOnHandling<CreateAssignment, Assignment>();

                var controller = GetController();
                var response = controller.Post(_courseId, _assignmentModel);

                ActionReponse<AssignmentModel> actionReponse;
                Assert.IsTrue(response.TryGetContentValue(out actionReponse));
                Assert.IsTrue(actionReponse.ValidationResult.IsValid);
            }
        }

        public class Put : AssignmentApiControllerTestFixture
        {
            private AssignmentModel _assignmentModel;
            private int _assignmentId;
            private int _courseId;

            [SetUp]
            public void Setup()
            {
                _courseId = 1;
                _assignmentId = 1;
                _assignmentModel = new AssignmentModel { Id = _assignmentId };

                _actionHandlerOverlordBuilder.WithNoErrorsOnHandling<UpdateAssignment, Assignment>();
                _modelFactoryBuilder.WithModelParsing();
            }

            [Test]
            public void It_should_return_status_code_bad_request_when_assignment_is_invalid()
            {
                _actionHandlerOverlordBuilder.WithErrorsOnHandling<UpdateAssignment, Assignment>();

                var controller = GetController();
                var response = controller.Put(_courseId, _assignmentId, _assignmentModel);

                Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            }

            [Test]
            public void It_should_return_validation_errors_when_assignment_is_invalid()
            {
                _actionHandlerOverlordBuilder.WithErrorsOnHandling<UpdateAssignment, Assignment>();

                var controller = GetController();
                var response = controller.Put(_courseId, _assignmentId, _assignmentModel);

                ActionReponse<AssignmentModel> actionReponse;
                Assert.IsTrue(response.TryGetContentValue(out actionReponse));
                Assert.IsFalse(actionReponse.ValidationResult.IsValid);
            }

            [Test]
            public void It_should_return_status_code_ok_when_assignment_is_valid()
            {
                _actionHandlerOverlordBuilder.WithNoErrorsOnHandling<UpdateAssignment, Assignment>();

                var controller = GetController();
                var response = controller.Put(_courseId, _assignmentId, _assignmentModel);

                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }

            [Test]
            public void It_should_return_no_validation_errors_when_assignment_is_valid()
            {
                _actionHandlerOverlordBuilder.WithNoErrorsOnHandling<UpdateAssignment, Assignment>();

                var controller = GetController();
                var response = controller.Put(_courseId, _assignmentId, _assignmentModel);

                ActionReponse<AssignmentModel> actionReponse;
                Assert.IsTrue(response.TryGetContentValue(out actionReponse));
                Assert.IsTrue(actionReponse.ValidationResult.IsValid);
            }
        }

        public class Delete : AssignmentApiControllerTestFixture
        {
            private int _assignmentId;
            private int _courseId;

            [SetUp]
            public void Setup()
            {
                _assignmentId = 54;
                _actionHandlerOverlordBuilder.WithNoErrorsOnHandling<DeleteCourseWork, CourseWork>();
            }

            [Test]
            public void It_should_delete_assignment_in_database()
            {
                _assignmentRepositoryBuilder.WithAssignment(new Assignment { Id = _assignmentId });

                var controller = GetController();
                controller.Delete(_courseId, _assignmentId);

                _actionHandlerOverlordBuilder.Mock.Verify(
                    x => x.HandleAction<DeleteCourseWork, CourseWork>(It.IsAny<DeleteCourseWork>()), Times.Once);
            }

            [Test]
            public void It_should_return_status_code_ok_on_successful_delete()
            {
                _assignmentRepositoryBuilder.WithAssignment(new Assignment { Id = _assignmentId });

                var controller = GetController();
                var response = controller.Delete(_courseId, _assignmentId);

                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }

            [Test]
            public void It_should_return_status_code_not_found_when_assignment_doesnt_exist()
            {
                _assignmentRepositoryBuilder = new MockAssignmentRepositoryBuilder()
                    .WithNoAssignments();

                var controller = GetController();
                var response = controller.Delete(_courseId, _assignmentId);

                Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            }
        }
    }
}
