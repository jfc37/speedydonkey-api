using System;
using ActionHandlers;
using Actions;
using Data.Tests.Builders;
using Models;
using NUnit.Framework;

namespace ActionHandlersTests
{
    [TestFixture]
    public class GivenRemoveAsTeacherIsCalled
    {
        private MockRepositoryBuilder<User> _repositoryBuilder;
        private User _userInDatabase;
        private RemoveAsTeacher _action;

        [SetUp]
        public void Setup()
        {
            _action = new RemoveAsTeacher(new User());
            _userInDatabase = new User();
            _repositoryBuilder = new MockRepositoryBuilder<User>()
                .WithGet(_userInDatabase)
                .WithUpdate();
        }

        private RemoveAsTeacherHandler GetHandler()
        {
            return new RemoveAsTeacherHandler(_repositoryBuilder.BuildObject());
        }

        protected void PerformAction()
        {
            GetHandler().Handle(_action);
        }

        [Test]
        public void Then_the_users_teacher_concerns_should_be_removed()
        {
            _userInDatabase.TeachingConcerns = new TeachingConcerns();

            PerformAction();

            Assert.IsNull(_userInDatabase.TeachingConcerns);
        }

        [Test]
        public void Then_the_user_should_have_teacher_claim_removed()
        {
            _userInDatabase.Claims = Claim.Teacher.ToString();

            PerformAction();

            Assert.IsFalse(_userInDatabase.Claims.Contains(Claim.Teacher.ToString()));
        }

        [Test]
        public void Then_previous_claims_shouldnt_be_overriden()
        {
            _userInDatabase.Claims = Claim.Admin + "," + Claim.Teacher;

            PerformAction();

            Assert.IsTrue(_userInDatabase.Claims.Contains(Claim.Admin.ToString()));
        }
    }
}
