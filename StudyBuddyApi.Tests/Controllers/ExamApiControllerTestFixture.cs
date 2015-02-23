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
    public class ExamApiControllerTestFixture
    {
        private ExamApiControllerBuilder _controllerBuilder;
        private MockExamRepositoryBuilder _examRepositoryBuilder;
        private MockActionHandlerOverlordBuilder _actionHandlerOverlordBuilder;
        private MockModelFactoryBuilder _modelFactoryBuilder;
        private HttpRequestMessageBuilder _httpRequestMessageBuilder;
        private MockSearchBuilder<Exam> _searchBuilder;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _controllerBuilder = new ExamApiControllerBuilder();
            _examRepositoryBuilder = new MockExamRepositoryBuilder();
            _actionHandlerOverlordBuilder = new MockActionHandlerOverlordBuilder();
            _modelFactoryBuilder = new MockModelFactoryBuilder()
                .WithEntityToModelMapping()
                .WithModelParsing();
            _httpRequestMessageBuilder = new HttpRequestMessageBuilder()
                .WithResponseAttached();
            _searchBuilder = new MockSearchBuilder<Exam>();
        }

        private ExamApiController GetController()
        {
            var controller = _controllerBuilder
                .WithExamRepository(_examRepositoryBuilder.BuildObject())
                .WithActionHandlerOverlord(_actionHandlerOverlordBuilder.BuildObject())
                .WithModelFactory(_modelFactoryBuilder.BuildObject())
                .WithEntitySearch(_searchBuilder.BuildObject())
                .Build();

            controller.ControllerContext.Request = _httpRequestMessageBuilder.Build();

            return controller;
        }

        public class Get : ExamApiControllerTestFixture
        {
            private int _courseId;

            [SetUp]
            public void Setup()
            {
                _examRepositoryBuilder = new MockExamRepositoryBuilder();
                _modelFactoryBuilder = new MockModelFactoryBuilder()
                    .WithEntityToModelMapping()
                    .WithModelParsing();

                _courseId = 454;
            }

            [Test]
            public void It_should_return_status_code_not_found_when_no_exams_exists()
            {
                _examRepositoryBuilder.WithNoExams();

                var controller = GetController();
                var response = controller.Get(_courseId);

                Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            }

            [Test]
            public void It_should_return_status_code_ok_when_exams_exist()
            {
                _examRepositoryBuilder
                    .WithSomeValidExam()
                    .WithSomeValidExam()
                    .WithSomeValidExam()
                    .WithSomeValidExam();

                var controller = GetController();
                var response = controller.Get(_courseId);

                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }

            [Test]
            public void It_should_return_all_exams_when_exams_exist()
            {
                _examRepositoryBuilder
                    .WithSomeValidExam()
                    .WithSomeValidExam()
                    .WithSomeValidExam()
                    .WithSomeValidExam();

                var controller = GetController();
                var response = controller.Get(_courseId);

                IEnumerable<ExamModel> examsInResponse;
                Assert.IsTrue(response.TryGetContentValue(out examsInResponse));
                Assert.AreEqual(4, examsInResponse.Count());
            }

            [Test]
            public void It_should_return_status_code_not_found_when_no_exams_match_id()
            {
                _modelFactoryBuilder.WithModelMappingReturningNull();
                const int examId = 5;
                _examRepositoryBuilder.WithExam(new Exam{Id = 1});

                var controller = GetController();
                var response = controller.Get(_courseId, examId);

                Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            }

            [Test]
            public void It_should_return_status_code_ok_when_exam_exists()
            {
                const int examId = 5;
                _examRepositoryBuilder.WithExam(new Exam{Id = examId});

                var controller = GetController();
                var response = controller.Get(examId);

                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }

            [Test]
            public void It_should_return_exam_with_matching_id()
            {
                const int examId = 5;
                _examRepositoryBuilder.WithExam(new Exam{Id = examId});

                var controller = GetController();
                var response = controller.Get(_courseId, examId);

                ExamModel examInResponse;
                Assert.IsTrue(response.TryGetContentValue(out examInResponse));
                Assert.AreEqual(examId, examInResponse.Id);
            }
        }

        public class GetWithQuery : ExamApiControllerTestFixture
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

        public class Post : ExamApiControllerTestFixture
        {
            private ExamModel _examModel;
            private int _courseId;

            [SetUp]
            public void Setup()
            {
                _examModel = new ExamModel { Id = 54 };
                _courseId = 54;

                _modelFactoryBuilder.WithModelParsing();
                _actionHandlerOverlordBuilder.WithNoErrorsOnHandling<CreateExam, Exam>();
            }

            [Test]
            public void It_should_return_status_code_bad_request_when_exam_is_invalid()
            {
                _actionHandlerOverlordBuilder.WithErrorsOnHandling<CreateExam, Exam>();

                var controller = GetController();
                var response = controller.Post(_courseId, _examModel);

                Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            }

            [Test]
            public void It_should_return_validation_errors_when_exam_is_invalid()
            {
                _actionHandlerOverlordBuilder.WithErrorsOnHandling<CreateExam, Exam>();

                var controller = GetController();
                var response = controller.Post(_courseId, _examModel);

                ActionReponse<ExamModel> actionReponse;
                Assert.IsTrue(response.TryGetContentValue(out actionReponse));
                Assert.IsFalse(actionReponse.ValidationResult.IsValid);
            }

            [Test]
            public void It_should_return_status_code_created_when_exam_is_valid()
            {
                _actionHandlerOverlordBuilder.WithNoErrorsOnHandling<CreateExam, Exam>();

                var controller = GetController();
                var response = controller.Post(_courseId, _examModel);

                Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            }

            [Test]
            public void It_should_return_no_validation_errors_when_exam_is_valid()
            {
                _actionHandlerOverlordBuilder.WithNoErrorsOnHandling<CreateExam, Exam>();

                var controller = GetController();
                var response = controller.Post(_courseId, _examModel);

                ActionReponse<ExamModel> actionReponse;
                Assert.IsTrue(response.TryGetContentValue(out actionReponse));
                Assert.IsTrue(actionReponse.ValidationResult.IsValid);
            }
        }

        public class Put : ExamApiControllerTestFixture
        {
            private ExamModel _examModel;
            private int _examId;
            private int _courseId;

            [SetUp]
            public void Setup()
            {
                _courseId = 1;
                _examId = 1;
                _examModel = new ExamModel { Id = _examId };

                _actionHandlerOverlordBuilder.WithNoErrorsOnHandling<UpdateExam, Exam>();
                _modelFactoryBuilder.WithModelParsing();
            }

            [Test]
            public void It_should_return_status_code_bad_request_when_exam_is_invalid()
            {
                _actionHandlerOverlordBuilder.WithErrorsOnHandling<UpdateExam, Exam>();

                var controller = GetController();
                var response = controller.Put(_courseId, _examId, _examModel);

                Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            }

            [Test]
            public void It_should_return_validation_errors_when_exam_is_invalid()
            {
                _actionHandlerOverlordBuilder.WithErrorsOnHandling<UpdateExam, Exam>();

                var controller = GetController();
                var response = controller.Put(_courseId, _examId, _examModel);

                ActionReponse<ExamModel> actionReponse;
                Assert.IsTrue(response.TryGetContentValue(out actionReponse));
                Assert.IsFalse(actionReponse.ValidationResult.IsValid);
            }

            [Test]
            public void It_should_return_status_code_ok_when_exam_is_valid()
            {
                _actionHandlerOverlordBuilder.WithNoErrorsOnHandling<UpdateExam, Exam>();

                var controller = GetController();
                var response = controller.Put(_courseId, _examId, _examModel);

                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }

            [Test]
            public void It_should_return_no_validation_errors_when_exam_is_valid()
            {
                _actionHandlerOverlordBuilder.WithNoErrorsOnHandling<UpdateExam, Exam>();

                var controller = GetController();
                var response = controller.Put(_courseId, _examId, _examModel);

                ActionReponse<ExamModel> actionReponse;
                Assert.IsTrue(response.TryGetContentValue(out actionReponse));
                Assert.IsTrue(actionReponse.ValidationResult.IsValid);
            }
        }

        public class Delete : ExamApiControllerTestFixture
        {
            private int _examId;
            private int _courseId;

            [SetUp]
            public void Setup()
            {
                _examId = 54;
                _actionHandlerOverlordBuilder.WithNoErrorsOnHandling<DeleteCourseWork, CourseWork>();
            }

            [Test]
            public void It_should_delete_exam_in_database()
            {
                _examRepositoryBuilder.WithExam(new Exam { Id = _examId });

                var controller = GetController();
                controller.Delete(_courseId, _examId);

                _actionHandlerOverlordBuilder.Mock.Verify(
                    x => x.HandleAction<DeleteCourseWork, CourseWork>(It.IsAny<DeleteCourseWork>()), Times.Once);
            }

            [Test]
            public void It_should_return_status_code_ok_on_successful_delete()
            {
                _examRepositoryBuilder.WithExam(new Exam { Id = _examId });

                var controller = GetController();
                var response = controller.Delete(_courseId, _examId);

                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }

            [Test]
            public void It_should_return_status_code_not_found_when_exam_doesnt_exist()
            {
                _examRepositoryBuilder = new MockExamRepositoryBuilder()
                    .WithNoExams();

                var controller = GetController();
                var response = controller.Delete(_courseId, _examId);

                Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            }
        }
    }
}
