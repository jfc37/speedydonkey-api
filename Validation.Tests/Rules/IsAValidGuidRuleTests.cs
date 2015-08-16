using System;
using NUnit.Framework;
using Validation.Rules;

namespace Validation.Tests.Rules
{
    [TestFixture]
    public class IsStringAValidGuidRuleTests
    {
        [Test]
        public void True_when_string_is_a_guid()
        {
            var result = new IsStringAValidGuidRule(Guid.NewGuid().ToString())
                .IsValid();

            Assert.IsTrue(result);
        }
        [Test]
        public void False_when_string_is_empty()
        {
            var result = new IsStringAValidGuidRule("")
                .IsValid();

            Assert.IsFalse(result);
        }
        [Test]
        public void False_when_string_is_null()
        {
            var result = new IsStringAValidGuidRule(null)
                .IsValid();

            Assert.IsFalse(result);
        }
        [Test]
        public void False_when_string_is_not_a_guid()
        {
            var result = new IsStringAValidGuidRule("random43")
                .IsValid();

            Assert.IsFalse(result);
        }
    }
}
