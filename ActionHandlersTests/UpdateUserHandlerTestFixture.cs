using ActionHandlers;
using Actions;
using Common.Tests.Builders.MockBuilders;
using Data.Repositories;
using Data.Tests.Builders.MockBuilders;
using Models.Tests.Builders;
using NUnit.Framework;

namespace ActionHandlersTests
{
    [TestFixture]
    public class UpdateUserHandlerTestFixture
    {
        private UpdateUserHandlerBuilder _createUserHandlerBuilder;
        private MockUserRepositoryBuilder _userRepositoryBuilder;
        private MockPasswordHasherBuilder _passwordHasherBuilder;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _createUserHandlerBuilder = new UpdateUserHandlerBuilder();
            _userRepositoryBuilder = new MockUserRepositoryBuilder()
                .WithSuccessfulUpdate();
            _passwordHasherBuilder = new MockPasswordHasherBuilder()
                .WithHashCreation();
        }

        private UpdateUserHandler BuildUpdateUserHandler()
        {
            return _createUserHandlerBuilder
                .WithUserRepository(_userRepositoryBuilder.BuildObject())
                .WithPasswordHasher(_passwordHasherBuilder.BuildObject())
                .Build();
        }

        public class Handle : UpdateUserHandlerTestFixture
        {
            private UpdateUser _createUser;

            [SetUp]
            public void Setup()
            {
                _createUser = new UpdateUser(new UserBuilder().WithPassword("password").Build());
                _userRepositoryBuilder.WithUser(_createUser.ActionAgainst);
            }

            [Test]
            public void It_should_call_update_on_user_repository()
            {
                var createUserHanlder = BuildUpdateUserHandler();
                createUserHanlder.Handle(_createUser);

                _userRepositoryBuilder.Mock.Verify(x => x.Update(_createUser.ActionAgainst));
            }
        }
    }

    public class UpdateUserHandlerBuilder
    {
        private IUserRepository _userRepository;
        private IPasswordHasher _passwordHasher;

        public UpdateUserHandler Build()
        {
            return new UpdateUserHandler(_userRepository, _passwordHasher);
        }

        public UpdateUserHandlerBuilder WithUserRepository(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            return this;
        }

        public UpdateUserHandlerBuilder WithPasswordHasher(IPasswordHasher passwordHasher)
        {
            _passwordHasher = passwordHasher;
            return this;
        }
    }
}
