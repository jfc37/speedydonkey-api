using System.Collections.Generic;
using System.Linq;
using Action.OpeningHours;
using ActionHandlers.PrivateLessons.SetOpeningHours;
using Common.Extensions;
using Data.Repositories;
using Models.PrivateLessons;
using Moq;
using NodaTime;
using NUnit.Framework;

namespace ActionHandlersTests.PrivateLessons
{
    [TestFixture]
    public class SetOpeningHoursHandlerTests
    {
        private SetOpeningHours _setOpeningHours;
        private Mock<IRepository<OpeningHours>> _repository;

        [SetUp]
        public void Setup()
        {
            _setOpeningHours = new SetOpeningHours(new OpeningHours(IsoDayOfWeek.Friday, LocalTime.Midnight, LocalTime.Noon));
            _repository = new Mock<IRepository<OpeningHours>>(MockBehavior.Loose);

            _repository.SetReturnsDefault(_setOpeningHours.ActionAgainst.PutIntoList().AsQueryable());
        }

        [Test]
        public void It_should_update_if_opening_hour_already_exist_for_day()
        {
            _repository.SetReturnsDefault(_setOpeningHours.ActionAgainst.PutIntoList().AsEnumerable());

            PerformAction();

            _repository.Verify(x => x.Update(It.IsAny<OpeningHours>()));
        }

        [Test]
        public void It_should_create_if_opening_hour_doesnt_exist_for_day()
        {
            _repository.SetReturnsDefault(new List<OpeningHours>());

            PerformAction();

            _repository.Verify(x => x.Create(_setOpeningHours.ActionAgainst));
        }

        private void PerformAction()
        {
            var factory = new OpeningHourManagerFactory(_repository.Object);

            new SetOpeningHoursHandler(factory)
                .Handle(_setOpeningHours);
        }
    }
}
