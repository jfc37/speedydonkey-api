using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using ActionHandlers;
using ActionHandlersTests.Builders.MockBuilders;
using Actions;
using Data.Tests.Builders.MockBuilders;
using Models;
using Models.Tests.Builders;
using Moq;
using NUnit.Framework;
using SpeedyDonkeyApi.Controllers;
using SpeedyDonkeyApi.Models;
using SpeedyDonkeyApi.Tests.Builders;
using SpeedyDonkeyApi.Tests.Builders.MockBuilders;

namespace SpeedyDonkeyApi.Tests.Controllers
{
    [TestFixture]
    public class CourseWorkGradeApiControllerTestFixture
    {
        private CourseWorkGradeApiControllerBuilder _controllerBuilder;
        private MockCourseWorkGradeRepositoryBuilder _courseWorkGradeRepositoryBuilder;
        private MockActionHandlerOverlordBuilder _actionHandlerOverlordBuilder;
        private MockModelFactoryBuilder _modelFactoryBuilder;
        private HttpRequestMessageBuilder _httpRequestMessageBuilder;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _controllerBuilder = new CourseWorkGradeApiControllerBuilder();
            _courseWorkGradeRepositoryBuilder = new MockCourseWorkGradeRepositoryBuilder();
            _actionHandlerOverlordBuilder = new MockActionHandlerOverlordBuilder();
            _modelFactoryBuilder = new MockModelFactoryBuilder()
                .WithEntityToModelMapping()
                .WithModelParsing();
            _httpRequestMessageBuilder = new HttpRequestMessageBuilder()
                .WithResponseAttached();
        }

        private CourseWorkGradeApiController GetController()
        {
            var controller = _controllerBuilder
                .WithCourseWorkGradeRepository(_courseWorkGradeRepositoryBuilder.BuildObject())
                .WithActionHandlerOverlord(_actionHandlerOverlordBuilder.BuildObject())
                .WithModelFactory(_modelFactoryBuilder.BuildObject())
                .Build();

            controller.ControllerContext.Request = _httpRequestMessageBuilder.Build();

            return controller;
        }

        public class Get : CourseWorkGradeApiControllerTestFixture
        {
            private int _courseId;
            private int _studentId;
            private CourseWorkGradeBuilder _courseWorkGradeBuilder;

            [SetUp]
            public void Setup()
            {
                _courseWorkGradeRepositoryBuilder = new MockCourseWorkGradeRepositoryBuilder();
                _modelFactoryBuilder = new MockModelFactoryBuilder()
                    .WithEntityToModelMapping()
                    .WithModelParsing();

                _courseId = 454;
                _studentId = 543;

                _courseWorkGradeBuilder = new CourseWorkGradeBuilder()
                    .WithCourseId(_courseId)
                    .WithStudentId(_studentId);
            }

            [Test]
            public void It_should_return_status_code_not_found_when_no_courseWorkGrades_exists()
            {
                _courseWorkGradeRepositoryBuilder.WithNoCourseWorkGrades();

                var controller = GetController();
                var response = controller.Get(_studentId, _courseId);

                Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            }

            [Test]
            public void It_should_return_status_code_ok_when_courseWorkGrades_exist()
            {
                _courseWorkGradeRepositoryBuilder
                    .WithSomeValidCourseWorkGrade()
                    .WithSomeValidCourseWorkGrade()
                    .WithSomeValidCourseWorkGrade()
                    .WithSomeValidCourseWorkGrade();

                var controller = GetController();
                var response = controller.Get(_studentId, _courseId);

                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }

            [Test]
            public void It_should_return_all_courseWorkGrades_when_courseWorkGrades_exist()
            {
                _courseWorkGradeRepositoryBuilder
                    .WithSomeValidCourseWorkGrade()
                    .WithSomeValidCourseWorkGrade()
                    .WithSomeValidCourseWorkGrade()
                    .WithSomeValidCourseWorkGrade();

                var controller = GetController();
                var response = controller.Get(_studentId, _courseId);

                IEnumerable<CourseWorkGradeModel> courseWorkGradesInResponse;
                Assert.IsTrue(response.TryGetContentValue(out courseWorkGradesInResponse));
                Assert.AreEqual(4, courseWorkGradesInResponse.Count());
            }

            [Test]
            public void It_should_return_status_code_not_found_when_no_courseWorkGrades_match_id()
            {
                _modelFactoryBuilder.WithModelMappingReturningNull();
                const int courseWorkGradeId = 5;
                _courseWorkGradeRepositoryBuilder
                    .WithNoCourseWorkGrades()
                    .WithCourseWorkGrade(_courseWorkGradeBuilder.WithCourseWorkId(1).Build());

                var controller = GetController();
                var response = controller.Get(_studentId, _courseId, courseWorkGradeId);

                Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            }

            [Test]
            public void It_should_return_status_code_ok_when_courseWorkGrade_exists()
            {
                const int courseWorkId = 5;
                _courseWorkGradeRepositoryBuilder.WithCourseWorkGrade(_courseWorkGradeBuilder.WithCourseWorkId(courseWorkId).Build());

                var controller = GetController();
                var response = controller.Get(_studentId, _courseId, courseWorkId);

                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }

            [Test]
            public void It_should_return_courseWorkGrade_with_matching_id()
            {
                const int courseWorkId = 5;
                _courseWorkGradeRepositoryBuilder.WithCourseWorkGrade(_courseWorkGradeBuilder.WithCourseWorkId(courseWorkId).WithId(66).Build());

                var controller = GetController();
                var response = controller.Get(_studentId, _courseId, courseWorkId);

                CourseWorkGradeModel courseWorkGradeInResponse;
                Assert.IsTrue(response.TryGetContentValue(out courseWorkGradeInResponse));
                Assert.AreEqual(66, courseWorkGradeInResponse.Id);
            }
        }

        public class Post : CourseWorkGradeApiControllerTestFixture
        {
            private int _courseWorkId;
            private int _courseId;
            private int _studentId;
            private CourseWorkGradeModel _courseGradeModel;

            [SetUp]
            public void Setup()
            {
                _modelFactoryBuilder.WithEntityToModelMapping();
                _courseGradeModel = new CourseWorkGradeModel();

                _courseWorkId = 543;
                _courseId = 1;
                _studentId = 2;
            }

            [Test]
            public void It_should_return_status_code_bad_request_when_exception_thrown()
            {
                _modelFactoryBuilder.WithModelMappingBlowUp();

                var controller = GetController();
                var response = controller.Post(_studentId, _courseId, _courseWorkId, _courseGradeModel);

                Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            }

            [Test]
            public void It_should_return_status_code_bad_request_when_adding_grade_fails()
            {
                _actionHandlerOverlordBuilder.WithErrorsOnHandling<AddCourseWorkGrade, CourseWorkGrade>();

                var controller = GetController();
                var response = controller.Post(_studentId, _courseId, _courseWorkId, _courseGradeModel);

                Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            }

            [Test]
            public void It_should_return_validation_errors_when_adding_grade_fails()
            {
                _actionHandlerOverlordBuilder.WithErrorsOnHandling<AddCourseWorkGrade, CourseWorkGrade>();

                var controller = GetController();
                var response = controller.Post(_studentId, _courseId, _courseWorkId, _courseGradeModel);

                ActionReponse<CourseWorkGradeModel> actionReponse;
                Assert.IsTrue(response.TryGetContentValue(out actionReponse));
                Assert.IsFalse(actionReponse.ValidationResult.IsValid);
            }

            [Test]
            public void It_should_return_status_code_OK_when_adding_grade_succeeds()
            {
                _actionHandlerOverlordBuilder.WithNoErrorsOnHandling<AddCourseWorkGrade, CourseWorkGrade>();

                var controller = GetController();
                var response = controller.Post(_studentId, _courseId, _courseWorkId, _courseGradeModel);

                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }

            [Test]
            public void It_should_return_no_validation_errors_when_adding_grade_succeeds()
            {
                _actionHandlerOverlordBuilder.WithNoErrorsOnHandling<AddCourseWorkGrade, CourseWorkGrade>();

                var controller = GetController();
                var response = controller.Post(_studentId, _courseId, _courseWorkId, _courseGradeModel);

                ActionReponse<CourseWorkGradeModel> actionReponse;
                Assert.IsTrue(response.TryGetContentValue(out actionReponse));
                Assert.IsTrue(actionReponse.ValidationResult.IsValid);
            }
        }

        public class Put : CourseWorkGradeApiControllerTestFixture
        {
            private CourseWorkGradeModel _courseWorkGradeModel;
            private int _courseWorkGradeId;
            private int _courseId;
            private int _personId;

            [SetUp]
            public void Setup()
            {
                _personId = 1;
                _courseId = 1;
                _courseWorkGradeId = 1;
                _courseWorkGradeModel = new CourseWorkGradeModel { Id = _courseWorkGradeId };

                _actionHandlerOverlordBuilder.WithNoErrorsOnHandling<UpdateCourseWorkGrade, CourseWorkGrade>();
                _modelFactoryBuilder.WithModelParsing();
            }

            [Test]
            public void It_should_return_status_code_bad_request_when_courseWorkGrade_is_invalid()
            {
                _actionHandlerOverlordBuilder.WithErrorsOnHandling<UpdateCourseWorkGrade, CourseWorkGrade>();

                var controller = GetController();
                var response = controller.Put(_personId, _courseId, _courseWorkGradeId, _courseWorkGradeModel);

                Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            }

            [Test]
            public void It_should_return_validation_errors_when_courseWorkGrade_is_invalid()
            {
                _actionHandlerOverlordBuilder.WithErrorsOnHandling<UpdateCourseWorkGrade, CourseWorkGrade>();

                var controller = GetController();
                var response = controller.Put(_personId, _courseId, _courseWorkGradeId, _courseWorkGradeModel);

                ActionReponse<CourseWorkGradeModel> actionReponse;
                Assert.IsTrue(response.TryGetContentValue(out actionReponse));
                Assert.IsFalse(actionReponse.ValidationResult.IsValid);
            }

            [Test]
            public void It_should_return_status_code_ok_when_courseWorkGrade_is_valid()
            {
                _actionHandlerOverlordBuilder.WithNoErrorsOnHandling<UpdateCourseWorkGrade, CourseWorkGrade>();

                var controller = GetController();
                var response = controller.Put(_personId, _courseId, _courseWorkGradeId, _courseWorkGradeModel);

                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }

            [Test]
            public void It_should_return_no_validation_errors_when_courseWorkGrade_is_valid()
            {
                _actionHandlerOverlordBuilder.WithNoErrorsOnHandling<UpdateCourseWorkGrade, CourseWorkGrade>();

                var controller = GetController();
                var response = controller.Put(_personId, _courseId, _courseWorkGradeId, _courseWorkGradeModel);

                ActionReponse<CourseWorkGradeModel> actionReponse;
                Assert.IsTrue(response.TryGetContentValue(out actionReponse));
                Assert.IsTrue(actionReponse.ValidationResult.IsValid);
            }

            [Test]
            public void It_should_return_status_code_bad_request_when_model_factory_blows_up()
            {
                _modelFactoryBuilder.WithModelParsingBlowUp();
                _actionHandlerOverlordBuilder.WithNoErrorsOnHandling<UpdateCourseWorkGrade, CourseWorkGrade>();

                var controller = GetController();
                var response = controller.Put(_personId, _courseId, _courseWorkGradeId, _courseWorkGradeModel);

                Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            }
        }

        public class Delete : CourseWorkGradeApiControllerTestFixture
        {
            private int _courseWorkGradeId;
            private int _courseId;
            private int _personId;
            private int _courseWorkdId;
            private CourseWorkGrade _courseWorkGrade;

            [SetUp]
            public void Setup()
            {
                _courseWorkGradeId = 54;
                _courseId = 1;
                _personId = 1;
                _courseWorkdId = 1;
                _actionHandlerOverlordBuilder.WithNoErrorsOnHandling<DeleteCourseWorkGrade, CourseWorkGrade>();

                _courseWorkGrade = new CourseWorkGradeBuilder()
                    .WithCourseId(_courseId)
                    .WithCourseWorkId(_courseWorkdId)
                    .WithStudentId(_personId)
                    .WithId(_courseWorkGradeId)
                    .Build();

                _courseWorkGradeRepositoryBuilder.WithCourseWorkGrade(_courseWorkGrade);
            }

            [Test]
            public void It_should_delete_courseWorkGrade_in_database()
            {
                _courseWorkGradeRepositoryBuilder.WithCourseWorkGrade(_courseWorkGrade);

                var controller = GetController();
                controller.Delete(_personId, _courseId, _courseWorkdId);

                _actionHandlerOverlordBuilder.Mock.Verify(
                    x => x.HandleAction<DeleteCourseWorkGrade, CourseWorkGrade>(It.IsAny<DeleteCourseWorkGrade>()), Times.Once);
            }

            [Test]
            public void It_should_return_status_code_ok_on_successful_delete()
            {
                _courseWorkGradeRepositoryBuilder.WithCourseWorkGrade(_courseWorkGrade);

                var controller = GetController();
                var response = controller.Delete(_personId, _courseId, _courseWorkdId);

                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }

            [Test]
            public void It_should_return_status_code_not_found_when_courseWorkGrade_doesnt_exist()
            {
                _courseWorkGradeRepositoryBuilder = new MockCourseWorkGradeRepositoryBuilder()
                    .WithNoCourseWorkGrades();

                var controller = GetController();
                var response = controller.Delete(_personId, _courseId, _courseWorkdId);

                Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            }
        }
    }
}
