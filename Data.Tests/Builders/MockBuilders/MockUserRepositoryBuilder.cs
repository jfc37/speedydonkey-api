using System.Collections.Generic;
using Common.Tests.Builders.MockBuilders;
using Data.Repositories;
using Models;
using Moq;

namespace Data.Tests.Builders.MockBuilders
{
    public class MockUserRepositoryBuilder : MockBuilder<IUserRepository>
    {
        private readonly IList<User> _users;

        public MockUserRepositoryBuilder()
        {
            _users = new List<User>();

            Mock.Setup(x => x.Get(It.IsAny<int>()))
                .Returns((User) null);
        }

        protected override void BeforeBuild()
        {
            Mock.Setup(x => x.GetAll())
                .Returns(_users);
        }

        public MockUserRepositoryBuilder WithSomeValidUser()
        {
            _users.Add(new User());
            return this;
        }

        public MockUserRepositoryBuilder WithNoUsers()
        {
            _users.Clear();
            return this;
        }

        public MockUserRepositoryBuilder WithUser(User user)
        {
            _users.Add(user);

            Mock.Setup(x => x.Get(user.Id))
                .Returns(user);

            return this;
        }

        public MockUserRepositoryBuilder WithSuccessfulCreation()
        {
            Mock.Setup(x => x.Create(It.IsAny<User>()))
                .Returns<User>(x => new User{Id = 543});

            return this;
        }

        public MockUserRepositoryBuilder WithSuccessfulUpdate()
        {
            Mock.Setup(x => x.Update(It.IsAny<User>()))
                .Returns<User>(x => x);

            return this;
        }

        public MockUserRepositoryBuilder WithSuccessfulDelete()
        {
            Mock.Setup(x => x.Delete(It.IsAny<User>()));

            return this;
        }

        public MockUserRepositoryBuilder WithAnyUser()
        {
            Mock.Setup(x => x.Get(It.IsAny<int>()))
                .Returns<int>(x => new User {Id = x});

            return this;
        }
    }
}
