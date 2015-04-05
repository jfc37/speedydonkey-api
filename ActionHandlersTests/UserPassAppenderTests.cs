using System;
using System.Collections.Generic;
using System.Linq;
using ActionHandlers.EnrolmentProcess;
using ActionHandlers.UserPasses;
using Models;
using NUnit.Framework;

namespace ActionHandlersTests
{
    [TestFixture]
    public class UserPassAppenderTests
    {
        protected User User;
        protected Pass Pass;

        private UserPassAppender GetUserPassAppender()
        {
            return new UserPassAppender(new PassCreatorFactory());
        }

        protected void PerformAction()
        {
            GetUserPassAppender().AddPassToUser(User, Pass);
        }
    }

    public class GivenAUserDoesntHaveAnyPasses : UserPassAppenderTests
    {
        [SetUp]
        public void Setup()
        {
            User = new User();
            Pass = new Pass
            {
                PassType = PassType.Unlimited.ToString()
            };
        }

        [Test]
        public void Then_the_new_pass_should_start_from_today()
        {
            PerformAction();

            var newPass = User.Passes.Single();
            Assert.AreEqual(DateTime.Now.Date, newPass.StartDate.Date);
            Assert.IsTrue(newPass.IsValid());
        }
    }

    public class GivenAUserHasInvalidPasses : UserPassAppenderTests
    {
        [SetUp]
        public void Setup()
        {
            User = new User
            {
                Passes = new List<IPass>
                {
                    new ClipPass
                    {
                        ClipsRemaining = 0,
                        EndDate = DateTime.Now.AddDays(4)
                    },
                    new Pass
                    {
                        EndDate = DateTime.Now.AddDays(-2)
                    }
                }
            };
            Pass = new Pass
            {
                PassType = PassType.Unlimited.ToString()
            };
        }

        [Test]
        public void Then_the_new_pass_should_start_from_today()
        {
            PerformAction();

            var newPass = User.Passes.Single(x => x.IsValid());
            Assert.AreEqual(DateTime.Now.Date, newPass.StartDate.Date);
        }
    }

    public class GivenAUserHasAValidPass : UserPassAppenderTests
    {
        private Pass _existingPass;

        [SetUp]
        public void Setup()
        {
            _existingPass = new Pass
            {
                EndDate = DateTime.Now.AddDays(4)
            };
            User = new User
            {
                Passes = new List<IPass>
                {
                    _existingPass
                }
            };
            Pass = new Pass
            {
                PassType = PassType.Unlimited.ToString()
            };
        }

        [Test]
        public void Then_the_new_pass_should_start_the_day_the_valid_pass_expires()
        {
            PerformAction();

            var expectedStartDate = _existingPass.EndDate.AddDays(1).Date;
            var newPass = User.Passes.Single(x => x.StartDate == User.Passes.Max(y => y.StartDate));
            Assert.AreEqual(expectedStartDate, newPass.StartDate);
        }
    }

    public class GivenAUserHasAValidPassAndAPendingPass : UserPassAppenderTests
    {
        private Pass _existingPass;
        private Pass _pendingPass;

        [SetUp]
        public void Setup()
        {
            _existingPass = new Pass
            {
                EndDate = DateTime.Now.AddDays(4)
            };
            _pendingPass = new Pass
            {
                StartDate = DateTime.Now.AddDays(5),
                EndDate = DateTime.Now.AddDays(10)
            };
            User = new User
            {
                Passes = new List<IPass>
                {
                    _existingPass,
                    _pendingPass
                }
            };
            Pass = new Pass
            {
                PassType = PassType.Unlimited.ToString()
            };
        }
        [Test]
        public void Then_the_new_pass_should_start_the_day_the_pending_pass_expires()
        {
            PerformAction();

            var expectedStartDate = _pendingPass.EndDate.AddDays(1).Date;
            var newPass = User.Passes.Single(x => x.StartDate == User.Passes.Max(y => y.StartDate));
            Assert.AreEqual(expectedStartDate, newPass.StartDate);
        }
    }
}
