using Models.PrivateLessons;
using NodaTime;
using NUnit.Framework;
using Validation.Validators.PrivateLessons;

namespace Validation.Tests.Validators.PrivateLessons
{
    [TestFixture]
    public class SetOpeningHoursValidatorTests
    {
        private IsoDayOfWeek _dayOfWeek;
        private LocalTime _openingTime;
        private LocalTime _closingTime;

        [SetUp]
        public void Setup()
        {
            _dayOfWeek = IsoDayOfWeek.Friday;
            _openingTime = new LocalTime(9, 0);
            _closingTime = _openingTime.PlusHours(10).PlusMinutes(30);
        }

        [Test]
        public void Passes_when_valid()
        {
            var result = PerformAction();

            Assert.IsTrue(result.IsValid);
        }

        [Test]
        public void Fails_when_no_day()
        {
            _dayOfWeek = IsoDayOfWeek.None;

            var result = PerformAction();

            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void Fails_when_closing_hour_is_before_opening_hour()
        {
            _closingTime = _openingTime.PlusHours(-1);

            var result = PerformAction();

            Assert.IsFalse(result.IsValid);
        }

        private FluentValidation.Results.ValidationResult PerformAction()
        {
            return new SetOpeningHoursValidator()
                .Validate(new OpeningHours(_dayOfWeek, _openingTime, _closingTime));
        }
    }
}
