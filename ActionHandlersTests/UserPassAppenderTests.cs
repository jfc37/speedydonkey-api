using System;
using System.Collections.Generic;
using System.Linq;
using ActionHandlers.UserPasses;
using Data.Tests.Builders;
using Models;
using NUnit.Framework;

namespace ActionHandlersTests
{
    [TestFixture]
    public class UserPassAppenderTests
    {
        protected User User;
        protected Pass Pass;
        protected PassTemplate PassTemplate;
        protected MockRepositoryBuilder<Class> ClassRepositoryBuilder; 

        private UserPassAppender GetUserPassAppender()
        {
            return new UserPassAppender(new PassCreatorFactory(ClassRepositoryBuilder.BuildObject()));
        }

        protected void PerformAction()
        {
            GetUserPassAppender().AddPassToUser(User, Pass.PaymentStatus, PassTemplate);
        }
    }

    public class GivenAUserDoesntHaveAnyPasses : UserPassAppenderTests
    {
        [SetUp]
        public void Setup()
        {
            User = new User();
            PassTemplate = new PassTemplate
            {
                PassType = PassType.Unlimited.ToString()
            };
            Pass = new Pass();
            ClassRepositoryBuilder = new MockRepositoryBuilder<Class>()
                .WithGetAll();
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
            PassTemplate = new PassTemplate
            {
                PassType = PassType.Unlimited.ToString()
            };
            Pass = new Pass();
            ClassRepositoryBuilder = new MockRepositoryBuilder<Class>()
                .WithGetAll();
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
            PassTemplate = new PassTemplate
            {
                PassType = PassType.Unlimited.ToString()
            };
            Pass = new Pass();
            ClassRepositoryBuilder = new MockRepositoryBuilder<Class>()
                .WithGetAll();
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
            PassTemplate = new PassTemplate
            {
                PassType = PassType.Unlimited.ToString()
            };
            Pass = new Pass();
            ClassRepositoryBuilder = new MockRepositoryBuilder<Class>()
                .WithGetAll();
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

    public class GivenSomePassTemplate : UserPassAppenderTests
    {
        [SetUp]
        public void Setup()
        {
            User = new User();
            PassTemplate = new PassTemplate
            {
                PassType = PassType.Unlimited.ToString()
            };
            Pass = new Pass();
            ClassRepositoryBuilder = new MockRepositoryBuilder<Class>()
                .WithGetAll();
        }

        [Test]
        public void Then_the_new_pass_should_be_the_same_type()
        {
            PassTemplate.PassType = PassType.Clip.ToString();

            PerformAction();

            var newPass = User.Passes.Single();
            Assert.AreEqual(PassTemplate.PassType, newPass.PassType);
        }

        [Test]
        public void Then_the_new_pass_should_be_valid_for_the_same_number_of_classes()
        {
            PassTemplate.PassType = PassType.Clip.ToString();
            PassTemplate.ClassesValidFor = 8;

            PerformAction();

            var newPass = (ClipPass) User.Passes.Single();
            Assert.AreEqual(PassTemplate.ClassesValidFor, newPass.ClipsRemaining);
        }

        [Test]
        public void Then_the_new_pass_should_be_valid_for_the_same_number_of_weeks()
        {
            PassTemplate.WeeksValidFor = 3;

            PerformAction();

            var newPass = User.Passes.Single();
            var daysBeforeExpiry = newPass.EndDate.Subtract(newPass.StartDate).Days;
            Assert.AreEqual(PassTemplate.WeeksValidFor * 7, daysBeforeExpiry);
        }

        [Test]
        public void Then_the_new_pass_should_be_the_same_cost()
        {
            PassTemplate.Cost = 36;

            PerformAction();

            var newPass = User.Passes.Single();
            Assert.AreEqual(PassTemplate.Cost, newPass.Cost);
        }
    }
}
