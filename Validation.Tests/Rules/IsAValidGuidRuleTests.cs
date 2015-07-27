using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Repositories;
using FizzWare.NBuilder;
using Models;
using Moq;
using NUnit.Framework;
using Validation.Rules;

namespace Validation.Tests.Rules
{
    [TestFixture]
    public class IsStringAValidGuidRuleTests
    {
        [Test]
        public void True_when_string_is_a_guid()
        {
            var result = new IsStringAValidGuidRule(Guid.NewGuid().ToString())
                .IsValid();

            Assert.IsTrue(result);
        }
        [Test]
        public void False_when_string_is_empty()
        {
            var result = new IsStringAValidGuidRule("")
                .IsValid();

            Assert.IsFalse(result);
        }
        [Test]
        public void False_when_string_is_null()
        {
            var result = new IsStringAValidGuidRule(null)
                .IsValid();

            Assert.IsFalse(result);
        }
        [Test]
        public void False_when_string_is_not_a_guid()
        {
            var result = new IsStringAValidGuidRule("random43")
                .IsValid();

            Assert.IsFalse(result);
        }
    }

    [TestFixture]
    public class IsThereARegistrationWithMatchingRegistrationNumberTests
    {
        private IEnumerable<Registration> _registrations;
        private string _input;

        [SetUp]
        public void Setup()
        {
            _registrations = new List<Registration>();
        }

        private bool PerformAction()
        {
            var repository = new Mock<IRepository<Registration>>(MockBehavior.Loose);
            repository.SetReturnsDefault(_registrations);

            return new IsThereARegistrationWithMatchingRegistrationNumber(repository.Object, _input)
                .IsValid();
        }

        [Test]
        public void False_when_input_is_not_a_guid()
        {
            _input = "invalid";

            bool result = PerformAction();

            Assert.IsFalse(result);
        }

        [Test]
        public void False_when_no_registrations_match_input()
        {
            _registrations = Builder<Registration>.CreateListOfSize(1)
                .All().With(x => x.RegistationId = Guid.NewGuid())
                .Build();
            _input = Guid.NewGuid().ToString();

            bool result = PerformAction();

            Assert.IsFalse(result);
        }

        [Test]
        public void True_when_registration_matches_input()
        {
            var registrationNumber = Guid.NewGuid();
            _registrations = Builder<Registration>.CreateListOfSize(1)
                .All().With(x => x.RegistationId = registrationNumber)
                .Build();
            _input = registrationNumber.ToString();

            var result = PerformAction();

            Assert.IsTrue(result);
        }
    }
}
