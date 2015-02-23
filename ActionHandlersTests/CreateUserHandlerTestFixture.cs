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
    public class CreateUserHandlerTestFixture
    {
        private CreateUserHandlerBuilder _createUserHandlerBuilder;
        private MockUserRepositoryBuilder _userRepositoryBuilder;
        private MockPasswordHasherBuilder _passwordHasherBuilder;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _createUserHandlerBuilder = new CreateUserHandlerBuilder();
            _passwordHasherBuilder = new MockPasswordHasherBuilder()
                .WithHashCreation();
            _userRepositoryBuilder = new MockUserRepositoryBuilder()
                .WithSuccessfulCreation();
        }

        private CreateUserHandler BuildCreateUserHandler()
        {
            return _createUserHandlerBuilder
                .WithUserRepository(_userRepositoryBuilder.BuildObject())
                .WithPasswordHasher(_passwordHasherBuilder.BuildObject())
                .Build();
        }

        public class Handle : CreateUserHandlerTestFixture
        {
            private CreateUser _createUser;

            [SetUp]
            public void Setup()
            {
                _createUser = new CreateUser(new UserBuilder().WithPassword("password").Build());
            }

            [Test]
            public void It_should_call_create_on_user_repository()
            {
                var createUserHanlder = BuildCreateUserHandler();
                createUserHanlder.Handle(_createUser);

                _userRepositoryBuilder.Mock.Verify(x => x.Create(_createUser.ActionAgainst));
            }
        }
    }

    public class CreateUserHandlerBuilder
    {
        private IUserRepository _userRepository;
        private IPasswordHasher _passwordHasher;

        public CreateUserHandler Build()
        {
            return new CreateUserHandler(_userRepository, _passwordHasher);
        }

        public CreateUserHandlerBuilder WithUserRepository(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            return this;
        }

        public CreateUserHandlerBuilder WithPasswordHasher(IPasswordHasher passwordHasher)
        {
            _passwordHasher = passwordHasher;
            return this;
        }
    }
}
