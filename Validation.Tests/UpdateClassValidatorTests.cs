using System;
using System.Collections.Generic;
using Data.Repositories;
using Data.Tests.Builders;
using Models;
using NUnit.Framework;
using Validation.Validators;

namespace Validation.Tests
{
    [TestFixture]
    public class UpdateClassValidatorTests : ValidatorTests<UpdateClassValidator, Class>
    {
        private MockRepositoryBuilder<Class> _repositoryBuilder;
        private MockRepositoryBuilder<Teacher> _teacherRepositoryBuilder;

        [SetUp]
        public void Setup()
        {
            var teacher = new Teacher();
            _repositoryBuilder = new MockRepositoryBuilder<Class>()
                .WithSuccessfulGet();
            Parameter = new Class
            {
                Name = "name",
                EndTime = DateTime.Now.AddMinutes(60),
                StartTime = DateTime.Now.AddMinutes(40),
                Teachers = new List<ITeacher>
                {
                    teacher
                }
            };
            _teacherRepositoryBuilder = new MockRepositoryBuilder<Teacher>()
                .WithGet(teacher);
        }

        protected override UpdateClassValidator GetValidator()
        {
            return new UpdateClassValidator(_repositoryBuilder.BuildObject(), _teacherRepositoryBuilder.BuildObject());
        }

        public class ThereIsNoValidationErrors : UpdateClassValidatorTests
        {
            [Test]
            public void When_all_inputs_are_correct()
            {
                var result = PerformAction();

                Assert.IsTrue(result.IsValid);
            }
        }

        public class ThereIsAValidationError : UpdateClassValidatorTests
        {
            [Test]
            public void When_name_is_missing()
            {
                Parameter.Name = "";

                var result = PerformAction();

                ExpectValidationError(result, ValidationMessages.MissingName);
            }
            [Test]
            public void When_class_doesnt_exist()
            {
                _repositoryBuilder.WithUnsuccessfulGet();

                var result = PerformAction();

                ExpectValidationError(result, ValidationMessages.InvalidClass);
            }

            [Test]
            public void When_start_time_is_too_far_in_the_past()
            {
                Parameter.StartTime = DateTime.Now.AddYears(-11);

                var result = PerformAction();

                ExpectValidationError(result, ValidationMessages.MissingStartTime);
            }

            [Test]
            public void When_end_time_is_before_start_time()
            {
                Parameter.EndTime = DateTime.Now.AddHours(1);
                Parameter.StartTime = Parameter.EndTime.AddMinutes(1);

                var result = PerformAction();

                ExpectValidationError(result, ValidationMessages.EndTimeGreaterThanStartTime);
            }
        }
    }
}
