using System.Collections.Generic;
using Data.QueryFilters;
using FizzWare.NBuilder;
using Models;
using NUnit.Framework;

namespace Data.Tests.QueryFilters
{
    [TestFixture]
    public class AvailableBlocksForEnrolmentFilterTests
    {
        private IEnumerable<Block> PerformAction(IEnumerable<Block> query)
        {
            return new AvailableBlocksForEnrolmentFilter()
                .Filter(query);
        }

        [Test]
        public void It_should_exclude_invite_only_blocks()
        {
            var query = Builder<Block>.CreateListOfSize(1)
                .All()
                .With(x => x.IsInviteOnly = true)
                .Build();

            var result = PerformAction(query);

            Assert.IsEmpty(result);
        }
    }
}
