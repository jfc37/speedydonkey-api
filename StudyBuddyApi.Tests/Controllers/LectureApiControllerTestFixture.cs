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
    public class LectureApiControllerTestFixture
    {
        private LectureApiControllerBuilder _controllerBuilder;
        private MockLectureRepositoryBuilder _lectureRepositoryBuilder;
        private MockActionHandlerOverlordBuilder _actionHandlerOverlordBuilder;
        private MockModelFactoryBuilder _modelFactoryBuilder;
        private HttpRequestMessageBuilder _httpRequestMessageBuilder;
        private MockSearchBuilder<Lecture> _searchBuilder;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _controllerBuilder = new LectureApiControllerBuilder();
            _lectureRepositoryBuilder = new MockLectureRepositoryBuilder();
            _actionHandlerOverlordBuilder = new MockActionHandlerOverlordBuilder();
            _modelFactoryBuilder = new MockModelFactoryBuilder()
                .WithEntityToModelMapping()
                .WithModelParsing();
            _httpRequestMessageBuilder = new HttpRequestMessageBuilder()
                .WithResponseAttached();
            _searchBuilder = new MockSearchBuilder<Lecture>();
        }

        private LectureApiController GetController()
        {
            var controller = _controllerBuilder
                .WithLectureRepository(_lectureRepositoryBuilder.BuildObject())
                .WithActionHandlerOverlord(_actionHandlerOverlordBuilder.BuildObject())
                .WithModelFactory(_modelFactoryBuilder.BuildObject())
                .WithEntitySearch(_searchBuilder.BuildObject())
                .Build();

            controller.ControllerContext.Request = _httpRequestMessageBuilder.Build();

            return controller;
        }

        public class Get : LectureApiControllerTestFixture
        {
            private int _courseId;

            [SetUp]
            public void Setup()
            {
                _lectureRepositoryBuilder = new MockLectureRepositoryBuilder();
                _modelFactoryBuilder = new MockModelFactoryBuilder()
                    .WithEntityToModelMapping()
                    .WithModelParsing();

                _courseId = 454;
            }

            [Test]
            public void It_should_return_status_code_not_found_when_no_lectures_exists()
            {
                _lectureRepositoryBuilder.WithNoLectures();

                var controller = GetController();
                var response = controller.Get(_courseId);

                Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            }

            [Test]
            public void It_should_return_status_code_ok_when_lectures_exist()
            {
                _lectureRepositoryBuilder
                    .WithSomeValidLecture()
                    .WithSomeValidLecture()
                    .WithSomeValidLecture()
                    .WithSomeValidLecture();

                var controller = GetController();
                var response = controller.Get(_courseId);

                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }

            [Test]
            public void It_should_return_all_lectures_when_lectures_exist()
            {
                _lectureRepositoryBuilder
                    .WithSomeValidLecture()
                    .WithSomeValidLecture()
                    .WithSomeValidLecture()
                    .WithSomeValidLecture();

                var controller = GetController();
                var response = controller.Get(_courseId);

                IEnumerable<LectureModel> lecturesInResponse;
                Assert.IsTrue(response.TryGetContentValue(out lecturesInResponse));
                Assert.AreEqual(4, lecturesInResponse.Count());
            }

            [Test]
            public void It_should_return_status_code_not_found_when_no_lectures_match_id()
            {
                _modelFactoryBuilder.WithModelMappingReturningNull();
                const int lectureId = 5;
                _lectureRepositoryBuilder.WithLecture(new Lecture{Id = 1});

                var controller = GetController();
                var response = controller.Get(_courseId, lectureId);

                Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            }

            [Test]
            public void It_should_return_status_code_ok_when_lecture_exists()
            {
                const int lectureId = 5;
                _lectureRepositoryBuilder.WithLecture(new Lecture{Id = lectureId});

                var controller = GetController();
                var response = controller.Get(lectureId);

                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }

            [Test]
            public void It_should_return_lecture_with_matching_id()
            {
                const int lectureId = 5;
                _lectureRepositoryBuilder.WithLecture(new Lecture{Id = lectureId});

                var controller = GetController();
                var response = controller.Get(_courseId, lectureId);

                LectureModel lectureInResponse;
                Assert.IsTrue(response.TryGetContentValue(out lectureInResponse));
                Assert.AreEqual(lectureId, lectureInResponse.Id);
            }
        }

        public class GetWithQuery : LectureApiControllerTestFixture
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

        public class Post : LectureApiControllerTestFixture
        {
            private LectureModel _lectureModel;
            private int _courseId;

            [SetUp]
            public void Setup()
            {
                _lectureModel = new LectureModel { Id = 54 };
                _courseId = 54;

                _actionHandlerOverlordBuilder.WithNoErrorsOnHandling<CreateLecture, Lecture>();
                _modelFactoryBuilder.WithModelParsing();
            }

            [Test]
            public void It_should_return_status_code_bad_request_when_lecture_is_invalid()
            {
                _actionHandlerOverlordBuilder.WithErrorsOnHandling<CreateLecture, Lecture>();

                var controller = GetController();
                var response = controller.Post(_courseId, _lectureModel);

                Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            }

            [Test]
            public void It_should_return_validation_errors_when_lecture_is_invalid()
            {
                _actionHandlerOverlordBuilder.WithErrorsOnHandling<CreateLecture, Lecture>();

                var controller = GetController();
                var response = controller.Post(_courseId, _lectureModel);

                ActionReponse<LectureModel> actionReponse;
                Assert.IsTrue(response.TryGetContentValue(out actionReponse));
                Assert.IsFalse(actionReponse.ValidationResult.IsValid);
            }

            [Test]
            public void It_should_return_status_code_updated_when_lecture_is_valid()
            {
                _actionHandlerOverlordBuilder.WithNoErrorsOnHandling<CreateLecture, Lecture>();

                var controller = GetController();
                var response = controller.Post(_courseId, _lectureModel);

                Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            }

            [Test]
            public void It_should_return_no_validation_errors_when_lecture_is_valid()
            {
                _actionHandlerOverlordBuilder.WithNoErrorsOnHandling<CreateLecture, Lecture>();

                var controller = GetController();
                var response = controller.Post(_courseId, _lectureModel);

                ActionReponse<LectureModel> actionReponse;
                Assert.IsTrue(response.TryGetContentValue(out actionReponse));
                Assert.IsTrue(actionReponse.ValidationResult.IsValid);
            }
        }

        public class Put : LectureApiControllerTestFixture
        {
            private LectureModel _lectureModel;
            private int _lectureId;
            private int _courseId;

            [SetUp]
            public void Setup()
            {
                _courseId = 1;
                _lectureId = 1;
                _lectureModel = new LectureModel { Id = _lectureId };

                _actionHandlerOverlordBuilder.WithNoErrorsOnHandling<UpdateLecture, Lecture>();
                _modelFactoryBuilder.WithModelParsing();
            }

            [Test]
            public void It_should_return_status_code_bad_request_when_lecture_is_invalid()
            {
                _actionHandlerOverlordBuilder.WithErrorsOnHandling<UpdateLecture, Lecture>();

                var controller = GetController();
                var response = controller.Put(_courseId, _lectureId, _lectureModel);

                Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            }

            [Test]
            public void It_should_return_validation_errors_when_lecture_is_invalid()
            {
                _actionHandlerOverlordBuilder.WithErrorsOnHandling<UpdateLecture, Lecture>();

                var controller = GetController();
                var response = controller.Put(_courseId, _lectureId, _lectureModel);

                ActionReponse<LectureModel> actionReponse;
                Assert.IsTrue(response.TryGetContentValue(out actionReponse));
                Assert.IsFalse(actionReponse.ValidationResult.IsValid);
            }

            [Test]
            public void It_should_return_status_code_ok_when_lecture_is_valid()
            {
                _actionHandlerOverlordBuilder.WithNoErrorsOnHandling<UpdateLecture, Lecture>();

                var controller = GetController();
                var response = controller.Put(_courseId, _lectureId, _lectureModel);

                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }

            [Test]
            public void It_should_return_no_validation_errors_when_lecture_is_valid()
            {
                _actionHandlerOverlordBuilder.WithNoErrorsOnHandling<UpdateLecture, Lecture>();

                var controller = GetController();
                var response = controller.Put(_courseId, _lectureId, _lectureModel);

                ActionReponse<LectureModel> actionReponse;
                Assert.IsTrue(response.TryGetContentValue(out actionReponse));
                Assert.IsTrue(actionReponse.ValidationResult.IsValid);
            }
        }

        public class Delete : LectureApiControllerTestFixture
        {
            private int _lectureId;
            private int _courseId;

            [SetUp]
            public void Setup()
            {
                _lectureId = 54;
                _actionHandlerOverlordBuilder.WithNoErrorsOnHandling<DeleteLecture, Lecture>();
            }

            [Test]
            public void It_should_delete_lecture_in_database()
            {
                _lectureRepositoryBuilder.WithLecture(new Lecture { Id = _lectureId });

                var controller = GetController();
                controller.Delete(_courseId, _lectureId);

                _actionHandlerOverlordBuilder.Mock.Verify(
                    x => x.HandleAction<DeleteLecture, Lecture>(It.IsAny<DeleteLecture>()), Times.Once);
            }

            [Test]
            public void It_should_return_status_code_ok_on_successful_delete()
            {
                _lectureRepositoryBuilder.WithLecture(new Lecture { Id = _lectureId });

                var controller = GetController();
                var response = controller.Delete(_courseId, _lectureId);

                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }

            [Test]
            public void It_should_return_status_code_not_found_when_lecture_doesnt_exist()
            {
                _lectureRepositoryBuilder = new MockLectureRepositoryBuilder()
                    .WithNoLectures();

                var controller = GetController();
                var response = controller.Delete(_courseId, _lectureId);

                Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            }
        }
    }
}
