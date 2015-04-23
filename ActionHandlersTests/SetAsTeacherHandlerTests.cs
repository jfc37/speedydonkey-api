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

        [Test]
        public void Then_the_user_should_have_teacher_claim()
        {
            PerformAction();

            Assert.IsTrue(_userInDatabase.Claims.Contains(Claim.Teacher.ToString()));
        }

        [Test]
        public void Then_previous_claims_shouldnt_be_overriden()
        {
            _userInDatabase.Claims = Claim.Admin.ToString();

            PerformAction();

            Assert.IsTrue(_userInDatabase.Claims.Contains(Claim.Admin.ToString()));
            Assert.IsTrue(_userInDatabase.Claims.Contains(Claim.Teacher.ToString()));
        }
    }
}
