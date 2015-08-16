using Data.Repositories;
using Models;
using Moq;
using NUnit.Framework;
using Validation.Rules;

namespace Validation.Tests.Rules
{
    [TestFixture]
    public class DoesIdExistTests
    {
        [Test]
        public void It_should_fail_when_id_doesnt_exist()
        {
            var repository = new Mock<IRepository<Pass>>(MockBehavior.Loose);
            repository.SetReturnsDefault<Pass>(null);

            var result = new DoesIdExist<Pass>(repository.Object, 1)
                .IsValid();

            Assert.IsFalse(result);
        }

        [Test]
        public void It_should_pass_when_id_exists()
        {
            var repository = new Mock<IRepository<Pass>>(MockBehavior.Loose);
            repository.SetReturnsDefault<Pass>(new Pass());

            var result = new DoesIdExist<Pass>(repository.Object, 1)
                .IsValid();

            Assert.IsTrue(result);
        }
    }
}