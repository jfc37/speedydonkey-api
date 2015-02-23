using ActionHandlers;
using ActionHandlersTests.Builders;
using Actions;
using Data.Tests.Builders.MockBuilders;
using Models.Tests.Builders;
using NUnit.Framework;

namespace ActionHandlersTests
{
    [TestFixture]
    public class DeleteUserHandlerTestFixture
    {
        private DeleteUserHandlerBuilder _deleteUserHandlerBuilder;
        private MockUserRepositoryBuilder _userRepositoryBuilder;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _deleteUserHandlerBuilder = new DeleteUserHandlerBuilder();
            _userRepositoryBuilder = new MockUserRepositoryBuilder()
                .WithSuccessfulDelete();
        }

        private DeleteUserHandler BuildDeleteUserHandler()
        {
            return _deleteUserHandlerBuilder
                .WithUserRepository(_userRepositoryBuilder.BuildObject())
                .Build();
        }

        public class Handle : DeleteUserHandlerTestFixture
        {
            private DeleteUser _deleteUser;

            [SetUp]
            public void Setup()
            {
                _deleteUser = new DeleteUser(new UserBuilder().WithPassword("password").Build());
            }

            [Test]
            public void It_should_call_delete_on_user_repository()
            {
                var deleteUserHanlder = BuildDeleteUserHandler();
                deleteUserHanlder.Handle(_deleteUser);

                _userRepositoryBuilder.Mock.Verify(x => x.Delete(_deleteUser.ActionAgainst));
            }
        }
    }
}
