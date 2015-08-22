using System;
using NUnit.Framework;
using Validation.Rules;

namespace Validation.Tests.Rules
{
    [TestFixture]
    public class DateIsNotTooFarInThePastRuleTests
    {
        [Test]
        public void It_should_fail_for_min_date()
        {
            var result = new DateIsNotTooFarInThePastRule(DateTime.MinValue)
                .IsValid();

            Assert.IsFalse(result);
        }

        [Test]
        public void It_should_fail_for_date_11_years_in_the_past()
        {
            var result = new DateIsNotTooFarInThePastRule(DateTime.Now.AddYears(-11))
                .IsValid();

            Assert.IsFalse(result);
        }

        [Test]
        public void It_should_pass_for_date_9_years_in_the_past()
        {
            var result = new DateIsNotTooFarInThePastRule(DateTime.Now.AddYears(-9))
                .IsValid();

            Assert.IsTrue(result);
        }

        [Test]
        public void It_should_pass_for_today()
        {
            var result = new DateIsNotTooFarInThePastRule(DateTime.Now)
                .IsValid();

            Assert.IsTrue(result);
        }
    }
}