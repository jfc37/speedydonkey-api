using System.Collections.Generic;
using FizzWare.NBuilder;
using Models;
using NUnit.Framework;
using Validation.Rules;

namespace Validation.Tests.Rules
{
    [TestFixture]
    public class HasExactlyOneInSetTests
    {
        [Test]
        public void It_should_fail_when_set_is_empty()
        {
            var result = new HasExactlyOneInSetRule(new List<Pass>())
                .IsValid();

            Assert.IsFalse(result);
        }

        [Test]
        public void It_should_fail_when_set_is_null()
        {
            var result = new HasExactlyOneInSetRule(null)
                .IsValid();

            Assert.IsFalse(result);
        }

        [Test]
        public void It_should_fail_when_set_has_two_items()
        {
            var set = Builder<User>
                .CreateListOfSize(2)
                .Build();

            var result = new HasExactlyOneInSetRule(set)
                .IsValid();

            Assert.IsFalse(result);
        }

        [Test]
        public void It_should_pass_when_set_has_one_item()
        {
            var set = Builder<User>
                .CreateListOfSize(1)
                .Build();

            var result = new HasExactlyOneInSetRule(set)
                .IsValid();

            Assert.IsTrue(result);
        }
    }
}