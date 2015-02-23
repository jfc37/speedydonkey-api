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
    public class NoticeApiControllerTestFixture
    {
        private NoticeApiControllerBuilder _controllerBuilder;
        private MockNoticeRepositoryBuilder _noticeRepositoryBuilder;
        private MockActionHandlerOverlordBuilder _actionHandlerOverlordBuilder;
        private MockModelFactoryBuilder _modelFactoryBuilder;
        private HttpRequestMessageBuilder _httpRequestMessageBuilder;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _controllerBuilder = new NoticeApiControllerBuilder();
            _noticeRepositoryBuilder = new MockNoticeRepositoryBuilder();
            _actionHandlerOverlordBuilder = new MockActionHandlerOverlordBuilder();
            _modelFactoryBuilder = new MockModelFactoryBuilder()
                .WithEntityToModelMapping()
                .WithModelParsing();
            _httpRequestMessageBuilder = new HttpRequestMessageBuilder()
                .WithResponseAttached();
        }

        private NoticeApiController GetController()
        {
            var controller = _controllerBuilder
                .WithNoticeRepository(_noticeRepositoryBuilder.BuildObject())
                .WithActionHandlerOverlord(_actionHandlerOverlordBuilder.BuildObject())
                .WithModelFactory(_modelFactoryBuilder.BuildObject())
                .Build();

            controller.ControllerContext.Request = _httpRequestMessageBuilder.Build();

            return controller;
        }

        public class Get : NoticeApiControllerTestFixture
        {
            private int _courseId;

            [SetUp]
            public void Setup()
            {
                _noticeRepositoryBuilder = new MockNoticeRepositoryBuilder();
                _modelFactoryBuilder = new MockModelFactoryBuilder()
                    .WithEntityToModelMapping()
                    .WithModelParsing();

                _courseId = 454;
            }

            [Test]
            public void It_should_return_status_code_not_found_when_no_notices_exists()
            {
                _noticeRepositoryBuilder.WithNoNotices();

                var controller = GetController();
                var response = controller.Get(_courseId);

                Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            }

            [Test]
            public void It_should_return_stauts_code_ok_when_notices_exist()
            {
                _noticeRepositoryBuilder
                    .WithSomeValidNotice()
                    .WithSomeValidNotice()
                    .WithSomeValidNotice()
                    .WithSomeValidNotice();

                var controller = GetController();
                var response = controller.Get(_courseId);

                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }

            [Test]
            public void It_should_return_all_notices_when_notices_exist()
            {
                _noticeRepositoryBuilder
                    .WithSomeValidNotice()
                    .WithSomeValidNotice()
                    .WithSomeValidNotice()
                    .WithSomeValidNotice();

                var controller = GetController();
                var response = controller.Get(_courseId);

                IEnumerable<NoticeModel> noticesInResponse;
                Assert.IsTrue(response.TryGetContentValue(out noticesInResponse));
                Assert.AreEqual(4, noticesInResponse.Count());
            }

            [Test]
            public void It_should_return_status_code_not_found_when_no_notices_match_id()
            {
                _modelFactoryBuilder.WithModelMappingReturningNull();
                const int noticeId = 5;
                _noticeRepositoryBuilder.WithNotice(new Notice{Id = 1});

                var controller = GetController();
                var response = controller.Get(_courseId, noticeId);

                Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            }

            [Test]
            public void It_should_return_status_code_ok_when_notice_exists()
            {
                const int noticeId = 5;
                _noticeRepositoryBuilder.WithNotice(new Notice{Id = noticeId});

                var controller = GetController();
                var response = controller.Get(noticeId);

                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }

            [Test]
            public void It_should_return_notice_with_matching_id()
            {
                const int noticeId = 5;
                _noticeRepositoryBuilder.WithNotice(new Notice{Id = noticeId});

                var controller = GetController();
                var response = controller.Get(_courseId, noticeId);

                NoticeModel noticeInResponse;
                Assert.IsTrue(response.TryGetContentValue(out noticeInResponse));
                Assert.AreEqual(noticeId, noticeInResponse.Id);
            }
        }

        public class Post : NoticeApiControllerTestFixture
        {
            private NoticeModel _noticeModel;
            private int _professorId;

            [SetUp]
            public void Setup()
            {
                _noticeModel = new NoticeModel { Id = 54 };
                _professorId = 54;

                _actionHandlerOverlordBuilder.WithNoErrorsOnHandling<CreateNotice, Notice>();
                _modelFactoryBuilder.WithModelParsing();
            }

            [Test]
            public void It_should_return_status_code_bad_request_when_notice_is_invalid()
            {
                _actionHandlerOverlordBuilder.WithErrorsOnHandling<CreateNotice, Notice>();

                var controller = GetController();
                var response = controller.Post(_professorId, _noticeModel);

                Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            }

            [Test]
            public void It_should_return_validation_errors_when_notice_is_invalid()
            {
                _actionHandlerOverlordBuilder.WithErrorsOnHandling<CreateNotice, Notice>();

                var controller = GetController();
                var response = controller.Post(_professorId, _noticeModel);

                ActionReponse<NoticeModel> actionReponse;
                Assert.IsTrue(response.TryGetContentValue(out actionReponse));
                Assert.IsFalse(actionReponse.ValidationResult.IsValid);
            }

            [Test]
            public void It_should_return_status_code_created_when_notice_is_valid()
            {
                _actionHandlerOverlordBuilder.WithNoErrorsOnHandling<CreateNotice, Notice>();

                var controller = GetController();
                var response = controller.Post(_professorId, _noticeModel);

                Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            }

            [Test]
            public void It_should_return_no_validation_errors_when_notice_is_valid()
            {
                _actionHandlerOverlordBuilder.WithNoErrorsOnHandling<CreateNotice, Notice>();

                var controller = GetController();
                var response = controller.Post(_professorId, _noticeModel);

                ActionReponse<NoticeModel> actionReponse;
                Assert.IsTrue(response.TryGetContentValue(out actionReponse));
                Assert.IsTrue(actionReponse.ValidationResult.IsValid);
            }
        }

        public class Put : NoticeApiControllerTestFixture
        {
            private NoticeModel _noticeModel;
            private int _noticeId;
            private int _courseId;

            [SetUp]
            public void Setup()
            {
                _courseId = 1;
                _noticeId = 1;
                _noticeModel = new NoticeModel { Id = _noticeId };

                _actionHandlerOverlordBuilder.WithNoErrorsOnHandling<UpdateNotice, Notice>();
                _modelFactoryBuilder.WithModelParsing();
            }

            [Test]
            public void It_should_return_status_code_bad_request_when_notice_is_invalid()
            {
                _actionHandlerOverlordBuilder.WithErrorsOnHandling<UpdateNotice, Notice>();

                var controller = GetController();
                var response = controller.Put(_courseId, _noticeId, _noticeModel);

                Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            }

            [Test]
            public void It_should_return_validation_errors_when_notice_is_invalid()
            {
                _actionHandlerOverlordBuilder.WithErrorsOnHandling<UpdateNotice, Notice>();

                var controller = GetController();
                var response = controller.Put(_courseId, _noticeId, _noticeModel);

                ActionReponse<NoticeModel> actionReponse;
                Assert.IsTrue(response.TryGetContentValue(out actionReponse));
                Assert.IsFalse(actionReponse.ValidationResult.IsValid);
            }

            [Test]
            public void It_should_return_status_code_ok_when_notice_is_valid()
            {
                _actionHandlerOverlordBuilder.WithNoErrorsOnHandling<UpdateNotice, Notice>();

                var controller = GetController();
                var response = controller.Put(_courseId, _noticeId, _noticeModel);

                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }

            [Test]
            public void It_should_return_no_validation_errors_when_notice_is_valid()
            {
                _actionHandlerOverlordBuilder.WithNoErrorsOnHandling<UpdateNotice, Notice>();

                var controller = GetController();
                var response = controller.Put(_courseId, _noticeId, _noticeModel);

                ActionReponse<NoticeModel> actionReponse;
                Assert.IsTrue(response.TryGetContentValue(out actionReponse));
                Assert.IsTrue(actionReponse.ValidationResult.IsValid);
            }
        }

        public class Delete : NoticeApiControllerTestFixture
        {
            private int _noticeId;
            private int _courseId;

            [SetUp]
            public void Setup()
            {
                _noticeId = 54;
                _actionHandlerOverlordBuilder.WithNoErrorsOnHandling<DeleteNotice, Notice>();
            }

            [Test]
            public void It_should_delete_notice_in_database()
            {
                _noticeRepositoryBuilder.WithNotice(new Notice { Id = _noticeId });

                var controller = GetController();
                controller.Delete(_courseId, _noticeId);

                _actionHandlerOverlordBuilder.Mock.Verify(
                    x => x.HandleAction<DeleteNotice, Notice>(It.IsAny<DeleteNotice>()), Times.Once);
            }

            [Test]
            public void It_should_return_status_code_ok_on_successful_delete()
            {
                _noticeRepositoryBuilder.WithNotice(new Notice { Id = _noticeId });

                var controller = GetController();
                var response = controller.Delete(_courseId, _noticeId);

                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }

            [Test]
            public void It_should_return_status_code_not_found_when_notice_doesnt_exist()
            {
                _noticeRepositoryBuilder = new MockNoticeRepositoryBuilder()
                    .WithNoNotices();

                var controller = GetController();
                var response = controller.Delete(_courseId, _noticeId);

                Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            }
        }
    }
}
