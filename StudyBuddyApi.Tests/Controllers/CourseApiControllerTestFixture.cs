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
    public class CourseApiControllerTestFixture
    {
        private CourseApiControllerBuilder _controllerBuilder;
        private MockCourseRepositoryBuilder _courseRepositoryBuilder;
        private MockActionHandlerOverlordBuilder _actionHandlerOverlordBuilder;
        private MockModelFactoryBuilder _modelFactoryBuilder;
        private HttpRequestMessageBuilder _httpRequestMessageBuilder;
        private MockSearchBuilder<Course> _searchBuilder;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _controllerBuilder = new CourseApiControllerBuilder();
            _courseRepositoryBuilder = new MockCourseRepositoryBuilder();
            _actionHandlerOverlordBuilder = new MockActionHandlerOverlordBuilder();
            _modelFactoryBuilder = new MockModelFactoryBuilder()
                .WithEntityToModelMapping()
                .WithModelParsing();
            _httpRequestMessageBuilder = new HttpRequestMessageBuilder()
                .WithResponseAttached();
            _searchBuilder = new MockSearchBuilder<Course>();
        }

        private CourseApiController GetController()
        {
            var controller = _controllerBuilder
                .WithCourseRepository(_courseRepositoryBuilder.BuildObject())
                .WithActionHandlerOverlord(_actionHandlerOverlordBuilder.BuildObject())
                .WithModelFactory(_modelFactoryBuilder.BuildObject())
                .WithCourseSearch(_searchBuilder.BuildObject())
                .Build();

            controller.ControllerContext.Request = _httpRequestMessageBuilder.Build();

            return controller;
        }

        public class Get : CourseApiControllerTestFixture
        {

            [SetUp]
            public void Setup()
            {
                _courseRepositoryBuilder = new MockCourseRepositoryBuilder();
                _modelFactoryBuilder = new MockModelFactoryBuilder()
                    .WithEntityToModelMapping()
                    .WithModelParsing();
            }

            [Test]
            public void It_should_return_status_code_not_found_when_no_courses_exists()
            {
                _courseRepositoryBuilder.WithNoCourses();

                var controller = GetController();
                var response = controller.Get();

                Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            }

            [Test]
            public void It_should_return_stauts_code_ok_when_courses_exist()
            {
                _courseRepositoryBuilder
                    .WithSomeValidCourse()
                    .WithSomeValidCourse()
                    .WithSomeValidCourse()
                    .WithSomeValidCourse();

                var controller = GetController();
                var response = controller.Get();

                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }

            [Test]
            public void It_should_return_all_courses_when_courses_exist()
            {
                _courseRepositoryBuilder
                    .WithSomeValidCourse()
                    .WithSomeValidCourse()
                    .WithSomeValidCourse()
                    .WithSomeValidCourse();

                var controller = GetController();
                var response = controller.Get();

                IEnumerable<CourseModel> coursesInResponse;
                Assert.IsTrue(response.TryGetContentValue(out coursesInResponse));
                Assert.AreEqual(4, coursesInResponse.Count());
            }

            [Test]
            public void It_should_return_status_code_not_found_when_no_courses_match_id()
            {
                _modelFactoryBuilder.WithModelMappingReturningNull();
                const int courseId = 5;
                _courseRepositoryBuilder.WithCourse(new Course{Id = 1});

                var controller = GetController();
                var response = controller.Get(courseId);

                Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            }

            [Test]
            public void It_should_return_status_code_ok_when_course_exists()
            {
                const int courseId = 5;
                _courseRepositoryBuilder.WithCourse(new Course{Id = courseId});

                var controller = GetController();
                var response = controller.Get(courseId);

                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }

            [Test]
            public void It_should_return_course_with_matching_id()
            {
                const int courseId = 5;
                _courseRepositoryBuilder.WithCourse(new Course{Id = courseId});

                var controller = GetController();
                var response = controller.Get(courseId);

                CourseModel courseInResponse;
                Assert.IsTrue(response.TryGetContentValue(out courseInResponse));
                Assert.AreEqual(courseId, courseInResponse.Id);
            }
        }

        public class GetWithQuery : CourseApiControllerTestFixture
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

        public class Post : CourseApiControllerTestFixture
        {
            private CourseModel _courseModel;
            private int _professorId;

            [SetUp]
            public void Setup()
            {
                _courseModel = new CourseModel { Id = 54 };
                _professorId = 54;

                _actionHandlerOverlordBuilder.WithNoErrorsOnHandling<CreateCourse, Course>();
                _modelFactoryBuilder.WithModelParsing();
            }

            [Test]
            public void It_should_return_status_code_bad_request_when_course_is_invalid()
            {
                _actionHandlerOverlordBuilder.WithErrorsOnHandling<CreateCourse, Course>();

                var controller = GetController();
                var response = controller.Post(_professorId, _courseModel);

                Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            }

            [Test]
            public void It_should_return_validation_errors_when_course_is_invalid()
            {
                _actionHandlerOverlordBuilder.WithErrorsOnHandling<CreateCourse, Course>();

                var controller = GetController();
                var response = controller.Post(_professorId, _courseModel);

                ActionReponse<CourseModel> actionReponse;
                Assert.IsTrue(response.TryGetContentValue(out actionReponse));
                Assert.IsFalse(actionReponse.ValidationResult.IsValid);
            }

            [Test]
            public void It_should_return_status_code_created_when_course_is_valid()
            {
                _actionHandlerOverlordBuilder.WithNoErrorsOnHandling<CreateCourse, Course>();

                var controller = GetController();
                var response = controller.Post(_professorId, _courseModel);

                Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            }

            [Test]
            public void It_should_return_no_validation_errors_when_course_is_valid()
            {
                _actionHandlerOverlordBuilder.WithNoErrorsOnHandling<CreateCourse, Course>();

                var controller = GetController();
                var response = controller.Post(_professorId, _courseModel);

                ActionReponse<CourseModel> actionReponse;
                Assert.IsTrue(response.TryGetContentValue(out actionReponse));
                Assert.IsTrue(actionReponse.ValidationResult.IsValid);
            }
        }

        public class Put : CourseApiControllerTestFixture
        {
            private CourseModel _courseModel;
            private int _courseId;

            [SetUp]
            public void Setup()
            {
                _courseId = 1;
                _courseModel = new CourseModel { Id = _courseId };

                _actionHandlerOverlordBuilder.WithNoErrorsOnHandling<UpdateCourse, Course>();
                _modelFactoryBuilder
                    .WithEntityToModelMapping()
                    .WithModelParsing();
            }

            [Test]
            public void It_should_return_status_code_bad_request_when_course_is_invalid()
            {
                _actionHandlerOverlordBuilder.WithErrorsOnHandling<UpdateCourse, Course>();

                var controller = GetController();
                var response = controller.Put(_courseId, _courseModel);

                Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            }

            [Test]
            public void It_should_return_validation_errors_when_course_is_invalid()
            {
                _actionHandlerOverlordBuilder.WithErrorsOnHandling<UpdateCourse, Course>();

                var controller = GetController();
                var response = controller.Put(_courseId, _courseModel);

                ActionReponse<CourseModel> actionReponse;
                Assert.IsTrue(response.TryGetContentValue(out actionReponse));
                Assert.IsFalse(actionReponse.ValidationResult.IsValid);
            }

            [Test]
            public void It_should_return_status_code_ok_when_course_is_valid()
            {
                _actionHandlerOverlordBuilder.WithNoErrorsOnHandling<UpdateCourse, Course>();

                var controller = GetController();
                var response = controller.Put(_courseId, _courseModel);

                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }

            [Test]
            public void It_should_return_no_validation_errors_when_course_is_valid()
            {
                _actionHandlerOverlordBuilder.WithNoErrorsOnHandling<UpdateCourse, Course>();

                var controller = GetController();
                var response = controller.Put(_courseId, _courseModel);

                ActionReponse<CourseModel> actionReponse;
                Assert.IsTrue(response.TryGetContentValue(out actionReponse));
                Assert.IsTrue(actionReponse.ValidationResult.IsValid);
            }
        }

        public class Delete : CourseApiControllerTestFixture
        {
            private int _courseId;

            [SetUp]
            public void Setup()
            {
                _courseId = 54;
                _courseRepositoryBuilder.WithCourse(new Course { Id = _courseId });
                _actionHandlerOverlordBuilder.WithNoErrorsOnHandling<DeleteCourse, Course>();
            }

            [Test]
            public void It_should_delete_course_in_database()
            {
                var controller = GetController();
                controller.Delete(_courseId);

                _actionHandlerOverlordBuilder.Mock.Verify(
                    x => x.HandleAction<DeleteCourse, Course>(It.IsAny<DeleteCourse>()), Times.Once);
            }

            [Test]
            public void It_should_return_status_code_ok_on_successful_delete()
            {
                var controller = GetController();
                var response = controller.Delete(_courseId);

                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }

            [Test]
            public void It_should_return_status_code_not_found_when_course_doesnt_exist()
            {
                _courseRepositoryBuilder.WithNoCourses();

                var controller = GetController();
                var response = controller.Delete(_courseId);

                Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            }
        }
    }
}
