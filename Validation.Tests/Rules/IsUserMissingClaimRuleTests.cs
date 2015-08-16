using Common;
using Common.Extensions;
using FizzWare.NBuilder;
using Models;
using NUnit.Framework;
using Validation.Rules;

namespace Validation.Tests.Rules
{
    [TestFixture]
    public class IsUserMissingClaimRuleTests
    {
        [Test]
        public void False_when_user_has_claim()
        {
            var claim = Claim.Teacher;
            var user = Builder<User>.CreateNew()
                .With(x => x.Claims = claim.ToString())
                .Build();

            var result = new IsUserMissingClaimRule(user, claim)
                .IsValid();

            Assert.IsFalse(result);
        }

        [Test]
        public void False_when_user_has_multiple_claims()
        {
            var claim = Claim.Teacher;
            var user = Builder<User>.CreateNew()
                .With(x => x.Claims = "{0},{1}".FormatWith(claim.ToString(), Claim.Admin))
                .Build();

            var result = new IsUserMissingClaimRule(user, claim)
                .IsValid();

            Assert.IsFalse(result);
        }

        [Test]
        public void True_when_user_has_null_claims()
        {
            var claim = Claim.Teacher;
            var user = Builder<User>.CreateNew()
                .With(x => x.Claims = null)
                .Build();

            var result = new IsUserMissingClaimRule(user, claim)
                .IsValid();

            Assert.IsTrue(result);
        }

        [Test]
        public void True_when_user_has_empty_claims()
        {
            var claim = Claim.Teacher;
            var user = Builder<User>.CreateNew()
                .With(x => x.Claims = string.Empty)
                .Build();

            var result = new IsUserMissingClaimRule(user, claim)
                .IsValid();

            Assert.IsTrue(result);
        }

        [Test]
        public void True_when_user_has_a_different_claim()
        {
            var claim = Claim.Teacher;
            var user = Builder<User>.CreateNew()
                .With(x => x.Claims = Claim.Admin.ToString())
                .Build();

            var result = new IsUserMissingClaimRule(user, claim)
                .IsValid();

            Assert.IsTrue(result);
        }
    }
}