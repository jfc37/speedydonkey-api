using System;
using ActionHandlers;
using Actions;
using Data.Tests.Builders;
using Models;
using NUnit.Framework;

namespace ActionHandlersTests
{
    [TestFixture]
    public class GivenSetAsTeacherIsCalled
    {
        private MockRepositoryBuilder<User> _repositoryBuilder;
        private User _userInDatabase;
        private SetAsTeacher _action;

        [SetUp]
        public void Setup()
        {
            _action = new SetAsTeacher(new User());
            _userInDatabase = new User();
            _repositoryBuilder = new MockRepositoryBuilder<User>()
                .WithGet(_userInDatabase)
                .WithUpdate();
        }

        private SetAsTeacherHandler GetHandler()
        {
            return new SetAsTeacherHandler(_repositoryBuilder.BuildObject());
        }

        protected void PerformAction()
        {
            GetHandler().Handle(_action);
        }

        [Test]
        public void Then_the_users_teacher_concerns_should_exist()
        {
            PerformAction();

            Assert.IsNotNull(_userInDatabase.TeachingConcerns);
        }
    }
}
