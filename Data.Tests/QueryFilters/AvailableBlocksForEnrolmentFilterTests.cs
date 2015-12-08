using System;
using System.Collections.Generic;
using Common.Extensions;
using Data.QueryFilters;
using FizzWare.NBuilder;
using Models;
using NUnit.Framework;

namespace Data.Tests.QueryFilters
{
    [TestFixture]
    public class AvailableBlocksForEnrolmentFilterTests
    {
        private DateTime _today;

        public AvailableBlocksForEnrolmentFilterTests()
        {
            _today = DateTime.Today;
        }

        private Block GetValidBlock()
        {
            return new Block
            {
                IsInviteOnly = false,
                StartDate = _today.AddWeeks(-1),
                EndDate = _today.AddWeeks(2)
            };
        }

        private IEnumerable<Block> PerformAction(IEnumerable<Block> query)
        {
            return new AvailableBlocksForEnrolmentFilter(_today).Filter(query);
        }

        public class IncludesThoseThat : AvailableBlocksForEnrolmentFilterTests
        {
            [Test]
            public void Should_be_included()
            {
                PerformAssertion(GetValidBlock());
            }

            private void PerformAssertion(Block block)
            {
                var result = PerformAction(block.PutIntoList());

                Assert.IsNotEmpty(result);
            }
        }

        public class IgnoresThosesThat : AvailableBlocksForEnrolmentFilterTests
        {
            [Test]
            public void Are_invite_only()
            {
                var block = GetValidBlock();
                block.IsInviteOnly = true;

                PerformAssertion(block);
            }

            [Test]
            public void Have_finished()
            {
                var block = GetValidBlock();
                block.EndDate = DateTime.Today;

                PerformAssertion(block);
            }

            [Test]
            public void Are_past_their_first_week()
            {
                _today = new DateTime(2015, 12, 14);

                var block = GetValidBlock();
                block.StartDate = _today.AddWeeks(-1);

                PerformAssertion(block);
            }

            private void PerformAssertion(Block block)
            {
                var result = PerformAction(block.PutIntoList());

                Assert.IsEmpty(result);
            }
        }
    }
}
