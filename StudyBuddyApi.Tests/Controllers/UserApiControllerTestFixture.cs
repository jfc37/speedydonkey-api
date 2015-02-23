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
    public class UserApiControllerTestFixture
    {
        private UserApiControllerBuilder _controllerBuilder;
        private MockUserRepositoryBuilder _userRepositoryBuilder;
        private MockActionHandlerOverlordBuilder _actionHandlerOverlordBuilder;
        private MockModelFactoryBuilder _modelFactoryBuilder;
        private HttpRequestMessageBuilder _httpRequestMessageBuilder;
        private MockSearchBuilder<User> _searchBuilder;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            _controllerBuilder = new UserApiControllerBuilder();
            _userRepositoryBuilder = new MockUserRepositoryBuilder();
            _actionHandlerOverlordBuilder = new MockActionHandlerOverlordBuilder();
            _modelFactoryBuilder = new MockModelFactoryBuilder()
                .WithEntityToModelMapping()
                .WithModelParsing();
            _httpRequestMessageBuilder = new HttpRequestMessageBuilder()
                .WithResponseAttached();
            _searchBuilder = new MockSearchBuilder<User>();
        }

        private UserApiController GetController()
        {
            var controller =  _controllerBuilder
                .WithUserRepository(_userRepositoryBuilder.BuildObject())
                .WithActionHandlerOverlord(_actionHandlerOverlordBuilder.BuildObject())
                .WithModelFactory(_modelFactoryBuilder.BuildObject())
                .WithUserSearch(_searchBuilder.BuildObject())
                .Build();

            controller.ControllerContext.Request = _httpRequestMessageBuilder.Build();

            return controller;
        }

        public class Get : UserApiControllerTestFixture
        {
            [Test]
            public void It_should_return_status_code_not_found_when_no_users_exists()
            {
                _userRepositoryBuilder.WithNoUsers();

                var controller = GetController();
                var response = controller.Get();

                Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            }

            [Test]
            public void It_should_return_stuats_code_ok_when_users_exist()
            {
                _modelFactoryBuilder.WithModelMappingReturningNull();
                _userRepositoryBuilder
                    .WithSomeValidUser()
                    .WithSomeValidUser()
                    .WithSomeValidUser()
                    .WithSomeValidUser();

                var controller = GetController();
                var response = controller.Get();

                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }

            [Test]
            public void It_should_return_all_users_when_users_exist()
            {
                _modelFactoryBuilder.WithModelMappingReturningNull();
                _userRepositoryBuilder
                    .WithSomeValidUser()
                    .WithSomeValidUser()
                    .WithSomeValidUser()
                    .WithSomeValidUser();

                var controller = GetController();
                var response = controller.Get();

                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

                IEnumerable<UserModel> usersResponse;
                Assert.IsTrue(response.TryGetContentValue(out usersResponse));
                Assert.AreEqual(4, usersResponse.Count());
            }

            [Test]
            public void It_should_return_status_code_not_found_when_no_users_match_id()
            {
                const int userId = 5;
                _userRepositoryBuilder.WithUser(new User{Id = 1});
                _modelFactoryBuilder.WithModelMappingReturningNull();

                var controller = GetController();
                var response = controller.Get(userId);

                Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            }

            [Test]
            public void It_should_return_stauts_code_ok_when_user_found()
            {
                const int userId = 5;
                var userInDatabase = new User {Id = userId};
                _userRepositoryBuilder.WithUser(userInDatabase);
                _modelFactoryBuilder.WithEntityToModelMapping();

                var controller = GetController();
                var response = controller.Get(userId);

                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }

            [Test]
            public void It_should_return_user_with_matching_id()
            {
                const int userId = 5;
                var userInDatabase = new User {Id = userId};
                _userRepositoryBuilder.WithUser(userInDatabase);
                _modelFactoryBuilder.WithEntityToModelMapping();

                var controller = GetController();
                var response = controller.Get(userId);

                UserModel userResponse;
                Assert.IsTrue(response.TryGetContentValue(out userResponse));
                Assert.AreEqual(userInDatabase.Id, userResponse.Id);
            }
        }

        public class GetWithQuery : UserApiControllerTestFixture
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

        public class Post : UserApiControllerTestFixture
        {
            private UserModel _userModel;

            [SetUp]
            public void Setup()
            {
                _userModel = new UserModel { Id = 54 };

                _actionHandlerOverlordBuilder.WithNoErrorsOnHandling<CreateUser, User>();
                _modelFactoryBuilder.WithModelParsing();
            }

            [Test]
            public void It_should_return_status_code_bad_request_when_user_is_invalid()
            {
                _actionHandlerOverlordBuilder.WithErrorsOnHandling<CreateUser, User>();

                var controller = GetController();
                var response = controller.Post(_userModel);

                Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            }

            [Test]
            public void It_should_return_validation_errors_when_user_is_invalid()
            {
                _actionHandlerOverlordBuilder.WithErrorsOnHandling<CreateUser, User>();

                var controller = GetController();
                var response = controller.Post(_userModel);

                ActionReponse<UserModel> actionReponse;
                Assert.IsTrue(response.TryGetContentValue(out actionReponse));
                Assert.IsFalse(actionReponse.ValidationResult.IsValid);
            }

            [Test]
            public void It_should_return_status_code_created_when_user_is_valid()
            {
                _actionHandlerOverlordBuilder.WithNoErrorsOnHandling<CreateUser, User>();

                var controller = GetController();
                var response = controller.Post(_userModel);

                Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            }

            [Test]
            public void It_should_return_no_validation_errors_when_user_is_valid()
            {
                _actionHandlerOverlordBuilder.WithNoErrorsOnHandling<CreateUser, User>();

                var controller = GetController();
                var response = controller.Post(_userModel);

                ActionReponse<UserModel> actionReponse;
                Assert.IsTrue(response.TryGetContentValue(out actionReponse));
                Assert.IsTrue(actionReponse.ValidationResult.IsValid);
            }
        }

        public class Put : UserApiControllerTestFixture
        {
            private UserModel _userModel;
            private int _userId;

            [SetUp]
            public void Setup()
            {
                _userId = 1;
                _userModel = new UserModel { Id = _userId };

                _actionHandlerOverlordBuilder.WithNoErrorsOnHandling<UpdateUser, User>();
                _modelFactoryBuilder.WithModelParsing();
            }

            [Test]
            public void It_should_return_status_code_bad_request_when_user_is_invalid()
            {
                _actionHandlerOverlordBuilder.WithErrorsOnHandling<UpdateUser, User>();

                var controller = GetController();
                var response = controller.Put(_userId, _userModel);

                Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            }

            [Test]
            public void It_should_return_validation_errors_when_user_is_invalid()
            {
                _actionHandlerOverlordBuilder.WithErrorsOnHandling<UpdateUser, User>();

                var controller = GetController();
                var response = controller.Put(_userId, _userModel);

                ActionReponse<UserModel> actionReponse;
                Assert.IsTrue(response.TryGetContentValue(out actionReponse));
                Assert.IsFalse(actionReponse.ValidationResult.IsValid);
            }

            [Test]
            public void It_should_return_status_code_ok_when_user_is_valid()
            {
                _actionHandlerOverlordBuilder.WithNoErrorsOnHandling<UpdateUser, User>();

                var controller = GetController();
                var response = controller.Put(_userId, _userModel);

                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }

            [Test]
            public void It_should_return_no_validation_errors_when_user_is_valid()
            {
                _actionHandlerOverlordBuilder.WithNoErrorsOnHandling<UpdateUser, User>();

                var controller = GetController();
                var response = controller.Put(_userId, _userModel);

                ActionReponse<UserModel> actionReponse;
                Assert.IsTrue(response.TryGetContentValue(out actionReponse));
                Assert.IsTrue(actionReponse.ValidationResult.IsValid);
            }
        }

        public class Delete : UserApiControllerTestFixture
        {
            private int _userId;

            [SetUp]
            public void Setup()
            {
                _userId = 54;

                _userRepositoryBuilder
                    .WithAnyUser()
                    .WithSuccessfulDelete();

                _actionHandlerOverlordBuilder
                    .WithNoErrorsOnHandling<DeleteUser, User>();

                _userRepositoryBuilder = new MockUserRepositoryBuilder()
                    .WithNoUsers();
            }

            [Test]
            public void It_should_delete_user()
            {
                _userRepositoryBuilder
                    .WithSuccessfulDelete()
                    .WithAnyUser();

                var controller = GetController();
                controller.Delete(_userId);

                _actionHandlerOverlordBuilder.Mock.Verify(x => x.HandleAction<DeleteUser, User>(It.IsAny<DeleteUser>()), Times.Once);
            }

            [Test]
            public void It_should_return_status_code_ok_on_successful_delete()
            {
                _userRepositoryBuilder
                    .WithAnyUser()
                    .WithSuccessfulDelete();

                var controller = GetController();
                var response = controller.Delete(_userId);

                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }

            [Test]
            public void It_should_return_status_code_not_found_when_user_doesnt_exist()
            {
                _userRepositoryBuilder.WithNoUsers();

                var controller = GetController();
                var response = controller.Delete(_userId);

                Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
            }
        }
    }
}
