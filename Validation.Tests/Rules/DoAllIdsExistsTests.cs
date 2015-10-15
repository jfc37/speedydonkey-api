using System.Linq;
using Data.Repositories;
using FizzWare.NBuilder;
using Models;
using Moq;
using NUnit.Framework;
using Validation.Rules;

namespace Validation.Tests.Rules
{
    [TestFixture]
    public class DoAllIdsExistsTests
    {
        [Test]
        public void It_should_fail_when_id_doesnt_exist()
        {
            var repository = new Mock<IRepository<Pass>>(MockBehavior.Loose);
            repository.SetReturnsDefault<Pass>(null);

            var passes = Builder<Pass>.CreateListOfSize(1)
                .All().With(x => x.Id = 1)
                .Build();

            var result = new DoAllIdExists<Pass>(repository.Object, passes)
                .IsValid();

            Assert.IsFalse(result);
        }

        [Test]
        public void It_should_pass_when_all_ids_exists()
        {
            var passes = Builder<Pass>.CreateListOfSize(2)
                .TheFirst(1).With(x => x.Id = 1)
                .TheNext(1).With(x => x.Id = 2)
                .Build();

            var repository = new Mock<IRepository<Pass>>(MockBehavior.Loose);
            repository.SetReturnsDefault(passes.AsEnumerable());

            var result = new DoAllIdExists<Pass>(repository.Object, passes)
                .IsValid();

            Assert.IsTrue(result);
        }

        [Test]
        public void It_should_fail_when_only_some_ids_exists()
        {
            var existingPasses = Builder<Pass>.CreateListOfSize(1)
                .TheFirst(1).With(x => x.Id = 1)
                .Build();

            var passes = Builder<Pass>.CreateListOfSize(2)
                .TheFirst(1).With(x => x.Id = 1)
                .TheNext(1).With(x => x.Id = 2)
                .Build();

            var repository = new Mock<IRepository<Pass>>(MockBehavior.Loose);
            repository.SetReturnsDefault(existingPasses.AsEnumerable());

            var result = new DoAllIdExists<Pass>(repository.Object, passes)
                .IsValid();

            Assert.IsFalse(result);
        }
    }
}