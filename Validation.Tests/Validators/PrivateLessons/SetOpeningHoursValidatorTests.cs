using System.Collections.Generic;
using System.Linq;
using Common.Extensions;
using Data.Repositories;
using FizzWare.NBuilder;
using Models;
using Models.PrivateLessons;
using Moq;
using NodaTime;
using NUnit.Framework;
using Validation.Validators.PrivateLessons;

namespace Validation.Tests.Validators.PrivateLessons
{
    [TestFixture]
    public class SetTeacherAvailabilityValidatorTests
    {
        private IOperable<TimeSlot> _availabilities;
        private Teacher _teacher;

        private Mock<IRepository<Teacher>> _repository;

        [SetUp]
        public void Setup()
        {
            _availabilities = Builder<TimeSlot>.CreateListOfSize(1)
                .All()
                .With(x => x.Day = IsoDayOfWeek.Friday)
                .With(x => x.OpeningTime = LocalTime.Noon)
                .With(x => x.ClosingTime = LocalTime.Noon.PlusHours(8));

            _teacher = new Teacher(1);

            _repository = new Mock<IRepository<Teacher>>(MockBehavior.Loose);
            _repository.SetReturnsDefault(_teacher);
        }

        [Test]
        public void Passes_when_valid()
        {
            var result = PerformAction();

            Assert.IsTrue(result.IsValid);
        }

        [Test]
        public void Fails_when_teacher_doesnt_exist()
        {
            _repository.SetReturnsDefault((Teacher) null);

            var result = PerformAction();

            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void Fails_when_closing_hour_is_before_opening_hour()
        {
            _availabilities = _availabilities.With(x => x.OpeningTime = LocalTime.Noon)
                .With(x => x.ClosingTime = LocalTime.Noon.PlusHours(-2));

            var result = PerformAction();

            Assert.IsFalse(result.IsValid);
        }

        private FluentValidation.Results.ValidationResult PerformAction()
        {
            return new SetTeacherAvailabilityValidator(_repository.Object)
                .Validate(new TeacherAvailability(_availabilities.Build(), _teacher));
        }
    }

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
                .Validate(new TimeSlot(_dayOfWeek, _openingTime, _closingTime));
        }
    }
}
