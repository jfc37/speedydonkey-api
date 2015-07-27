using Common.Extensions;
using Data.Repositories;
using FizzWare.NBuilder;
using Models;
using Moq;
using NUnit.Framework;
using Validation.Rules;

namespace Validation.Tests.Rules
{
    [TestFixture]
    public class AreUsersExistingTeachersRuleTests
    {
        private ITeacher _user;
        private Teacher _savedTeacher;

        private bool PerformAction()
        {
            var repository = new Mock<IRepository<Teacher>>(MockBehavior.Loose);
            repository.SetReturnsDefault(_savedTeacher);

            return new AreUsersExistingTeachersRule(_user.PutIntoList(), repository.Object)
                .IsValid();
        }

        [Test]
        public void True_when_user_is_an_existing_teacher()
        {
            _savedTeacher = Builder<Teacher>.CreateNew()
                .With(x => x.Claims = Claim.Teacher.ToString())
                .Build();
            _user = _savedTeacher;

            var result = PerformAction();

            Assert.IsTrue(result);
        }

        [Test]
        public void False_when_user_is_not_an_existing_teacher()
        {
            _savedTeacher = null;
            _user = Builder<Teacher>.CreateNew()
                .With(x => x.Claims = Claim.Teacher.ToString())
                .With(x => x.Id = 2)
                .Build();

            var result = PerformAction();

            Assert.IsFalse(result);
        }
    }
}
