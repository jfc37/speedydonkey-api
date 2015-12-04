using System;
using System.Collections.Generic;
using Data.QueryFilters;
using FizzWare.NBuilder;
using Models;
using NUnit.Framework;

namespace Data.Tests.QueryFilters
{
    [TestFixture]
    public class AvailableEventsForRegistrationFilterTests
    {
        private IEnumerable<StandAloneEvent> PerformAction(IEnumerable<StandAloneEvent> query)
        {
            return new AvailableEventsForRegistrationFilter()
                .Filter(query);
        }

        [Test]
        public void It_should_exclude_private_events()
        {
            var query = Builder<StandAloneEvent>.CreateListOfSize(1)
                .All()
                .With(x => x.IsPrivate = true)
                .Build();

            var result = PerformAction(query);

            Assert.IsEmpty(result);
        }

        [Test]
        public void It_should_exclude_past_events()
        {
            var query = Builder<StandAloneEvent>.CreateListOfSize(1)
                .All()
                .With(x => x.StartTime = DateTime.Today.AddMinutes(-1))
                .Build();

            var result = PerformAction(query);

            Assert.IsEmpty(result);
        }
    }
}