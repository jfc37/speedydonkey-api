using System.Collections.Generic;
using ActionHandlers.CreateHandlers;
using Common.Extensions;
using Models;
using NUnit.Framework;

namespace ActionHandlersTests.Calculations
{
    [TestFixture]
    public class WindyLindyPriceCalculationTests
    {
        [TestCase(WindyLindyEvents.Heats, 10)]
        [TestCase(WindyLindyEvents.GrammyAwards, 20)]
        [TestCase(WindyLindyEvents.RockStarsBall, 60)]
        [TestCase(WindyLindyEvents.GroupiesPrivateParty, 25)]
        [TestCase(WindyLindyEvents.HardRockBusTour, 30)]
        [TestCase(WindyLindyEvents.BackStageParty, 60)]
        [TestCase(WindyLindyEvents.SundayNightAfterParty, 25)]
        [TestCase(WindyLindyEvents.GroupiesGoodbyeJam, 25)]
        public void It_should_have_the_correct_price_for_all_events(WindyLindyEvents windyLindyEvents, decimal expectedPrice)
        {
            var events = windyLindyEvents.PutIntoList();

            var result = new WindyLindyEventsPriceCalculation(events)
                .Calculate()
                .Result();

            Assert.AreEqual(expectedPrice, result);
        }

        [Test]
        public void It_should_handle_multiple_events()
        {
            var events = new List<WindyLindyEvents>{ WindyLindyEvents.Heats, WindyLindyEvents.GroupiesGoodbyeJam };

            var result = new WindyLindyEventsPriceCalculation(events)
                .Calculate()
                .Result();

            Assert.AreEqual(35, result);
        }
    }
}
