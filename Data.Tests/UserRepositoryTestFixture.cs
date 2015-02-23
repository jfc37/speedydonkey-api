using System.Linq;
using Data.Repositories;
using Data.Tests.Builders;
using Data.Tests.Builders.MockBuilders;
using Models;
using Models.Tests.Builders;
using NUnit.Framework;

namespace Data.Tests
{
    [TestFixture]
    public class UserRepositoryTestFixture
    {
        private UserRepositoryBuilder _userRepositoryBuilder;
        private MockDbContextBuilder _contextBuilder;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _userRepositoryBuilder = new UserRepositoryBuilder();
            _contextBuilder = new MockDbContextBuilder();
        }

        private UserRepository BuildUserRepository()
        {
            return _userRepositoryBuilder
                .WithContext(_contextBuilder.BuildObject())
                .Build();
        }

        public class Get : UserRepositoryTestFixture
        {
            private int _userId;

            [SetUp]
            public void Setup()
            {
                _userId = 553;
            }

            [Test]
            public void It_should_return_null_when_no_user_id_isnt_found()
            {
                _contextBuilder.WithNoUsers();

                var userRepository = BuildUserRepository();
                var user = userRepository.Get(_userId);

                Assert.IsNull(user);
            }

            [Test]
            public void It_should_return_all_users()
            {
                var savedUser = new User {Id = _userId};
                _contextBuilder.WithUser(savedUser);

                var userRepository = BuildUserRepository();
                var user = userRepository.Get(_userId);

                Assert.AreSame(savedUser, user);
            }
        }

        public class GetAll : UserRepositoryTestFixture
        {
            [Test]
            public void It_should_return_empty_when_no_users_exists()
            {
                _contextBuilder.WithNoUsers();

                var userRepository = BuildUserRepository();
                var results = userRepository.GetAll();

                Assert.IsEmpty(results);
            }

            [Test]
            public void It_should_return_all_users()
            {
                _contextBuilder.WithUser()
                    .WithUser()
                    .WithUser()
                    .WithUser();

                var userRepository = BuildUserRepository();
                var results = userRepository.GetAll();

                Assert.AreEqual(_contextBuilder.BuildObject().Users.Count(), results.Count());
            }
        }

        public class Create : UserRepositoryTestFixture
        {
            private User _user;

            [SetUp]
            public void Setup()
            {
                _user = new User();
                _contextBuilder.WithNoUsers();
            }

            [Test]
            public void It_should_add_user_to_the_database()
            {
                var userRepository = BuildUserRepository();
                userRepository.Create(_user);

                Assert.IsNotEmpty(_contextBuilder.BuildObject().Users.ToList());
            }
        }

        public class Update : UserRepositoryTestFixture
        {
            [Test]
            public void It_should_not_add_new_user_to_database_on_update()
            {
                User user = new UserBuilder()
                    .WithId(1)
                    .Build();
                _contextBuilder.WithUser(user);

                var repository = BuildUserRepository();
                repository.Update(user);

                Assert.AreEqual(1, _contextBuilder.Mock.Object.Users.Count());
            }
        }

        public class Delete : UserRepositoryTestFixture
        {
            private User _user;

            [SetUp]
            public void Setup()
            {
                _user = new UserBuilder().WithId(1).Build();
                _contextBuilder.WithUser(_user);
            }

            [Test]
            public void It_should_delete_user_to_the_database()
            {
                var userRepository = BuildUserRepository();
                userRepository.Delete(_user);

                Assert.IsEmpty(_contextBuilder.BuildObject().Users.ToList());
            }
        }
    }
}
