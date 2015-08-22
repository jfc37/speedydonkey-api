using Common.Extensions;
using NUnit.Framework;

namespace Common.Tests.Extensions.Strings
{
    [TestFixture]
    public class IsNotSameAsTests
    {
        [Test]
        public void Identical_strings_are_the_same()
        {
            Assert.IsFalse("Test".IsNotSameAs("Test"));
        }

        [Test]
        public void Different_case_strings_are_the_same()
        {
            Assert.IsFalse("Test".IsNotSameAs(("tesT")));
        }

        [Test]
        public void Different_strings_are_not_the_same()
        {
            Assert.IsTrue("Test".IsNotSameAs(("Tests")));
        }
    }
}