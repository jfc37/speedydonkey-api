using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Action;
using ActionHandlers.ClassCheckIn;
using Data.Repositories;
using Data.Tests.Builders;
using Models;
using NUnit.Framework;

namespace ActionHandlersTests
{
    [TestFixture]
    public class CheckStudentIntoClassHandlerTests
    {
        protected MockRepositoryBuilder<Class> ClassRepositoryBuilder;
        protected MockRepositoryBuilder<User> UserRepositoryBuilder;
        protected CheckStudentIntoClass Action;
        protected User UserInDatabase;
        protected Class ClassInDatabase;

        [SetUp]
        public virtual void Setup()
        {
            UserInDatabase = new User
            {
                Passes = new List<IPass>()
            };
            ClassInDatabase = new Class();
            ClassRepositoryBuilder = new MockRepositoryBuilder<Class>()
                .WithGet(ClassInDatabase)
                .WithUpdate();
            UserRepositoryBuilder = new MockRepositoryBuilder<User>()
                .WithGet(UserInDatabase);
            Action = new CheckStudentIntoClass(new Class
            {
                ActualStudents = new List<IUser>
                {
                    new User()
                }
            });
        }

        private CheckStudentIntoClassHandler GetHandler()
        {
            return new CheckStudentIntoClassHandler(ClassRepositoryBuilder.BuildObject(), UserRepositoryBuilder.BuildObject());
        }

        protected void PerformAction()
        {
            GetHandler().Handle(Action);
        }
    }

    public class GivenAUserIsCheckedIntoClass : CheckStudentIntoClassHandlerTests
    {
        [SetUp]
        public override void Setup()
        {
            base.Setup();
            UserInDatabase.Passes.Add(new Pass
            {
                StartDate = DateTime.Now.AddDays(-1),
                EndDate = DateTime.Now.AddDays(1)
            });
        }

        [Test]
        public void Then_the_student_should_be_added_to_the_class_attendance()
        {
            PerformAction();

            Assert.IsNotEmpty(ClassInDatabase.ActualStudents);
        }
    }

    public class GivenAUserHasAClipPass : GivenAUserIsCheckedIntoClass
    {
        protected int ClipsBeforeCheckIn;

        [SetUp]
        public override void Setup()
        {
            base.Setup();
            ClipsBeforeCheckIn = 3;
            UserInDatabase.Passes.Clear();
            UserInDatabase.Passes.Add(new ClipPass
            {
                ClipsRemaining = ClipsBeforeCheckIn,
                StartDate = DateTime.Now.AddDays(-1),
                EndDate = DateTime.Now.AddDays(1)
            });
        }

        [Test]
        public void Then_a_clip_should_be_removed()
        {
            PerformAction();

            var updatedUserPass = ClassInDatabase.ActualStudents.Single().Passes.OfType<ClipPass>().Single();
            Assert.AreEqual(ClipsBeforeCheckIn - 1, updatedUserPass.ClipsRemaining);
        }
    }

    public class WhenLastClipRemainingIsRemoved : GivenAUserHasAClipPass
    {
        private Pass _nextPass;
        private TimeSpan _expiryPeriod;

        [SetUp]
        public override void Setup()
        {
            base.Setup();
            ClipsBeforeCheckIn = 1;
            UserInDatabase.Passes.Clear();
            var today = DateTime.Now.Date;
            UserInDatabase.Passes.Add(new ClipPass
            {
                ClipsRemaining = ClipsBeforeCheckIn,
                StartDate = today.AddDays(-1),
                EndDate = today.AddDays(5)
            });
            _nextPass = new Pass
            {
                StartDate = today.AddDays(6),
                EndDate = today.AddDays(16)
            };
            _expiryPeriod = _nextPass.EndDate.Subtract(_nextPass.StartDate);
            UserInDatabase.Passes.Add(_nextPass);
        }

        [Test]
        public void Then_the_next_pass_should_have_its_start_date_moved_to_today()
        {
            PerformAction();

            var updatedNextPass = ClassInDatabase.ActualStudents.Single().Passes.Single(x => x.IsValid());
            Assert.AreEqual(DateTime.Now.Date, updatedNextPass.StartDate);
        }

        [Test]
        public void Then_the_next_pass_should_have_its_end_date_moved_based_on_today()
        {
            PerformAction();

            var updatedNextPass = ClassInDatabase.ActualStudents.Single().Passes.Single(x => x.IsValid());
            Assert.AreEqual(DateTime.Now.Date.Add(_expiryPeriod), updatedNextPass.EndDate);
        }
    }
}
