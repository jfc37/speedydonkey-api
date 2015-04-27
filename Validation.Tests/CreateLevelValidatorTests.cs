using System;
using System.Collections.Generic;
using Data.Tests.Builders;
using Models;
using NUnit.Framework;
using Validation.Validators;

namespace Validation.Tests
{
    [TestFixture]
    public class CreateLevelValidatorTests : ValidatorTests<CreateLevelValidator, Level>
    {
        protected MockRepositoryBuilder<User> UserRepositoryBuilder;
        
        [SetUp]
        public void Setup()
        {
            var teacher = new Teacher{Id = 1, Claims = Claim.Teacher.ToString(), TeachingConcerns = new TeachingConcerns()};
            UserRepositoryBuilder = new MockRepositoryBuilder<User>()
                .WithGet(teacher);

            Parameter = new Level
            {
                Name = "name",
                StartTime = DateTime.Now.AddYears(-1),
                EndTime = DateTime.Now.AddYears(1),
                ClassMinutes = 60,
                ClassesInBlock = 6,
                Teachers = new ITeacher[]{ teacher}
            };
        }

        protected override CreateLevelValidator GetValidator()
        {
            return new CreateLevelValidator(UserRepositoryBuilder.BuildObject());
        }

        public class ThereIsNoValidationErrors : CreateLevelValidatorTests
        {
            [Test]
            public void When_all_inputs_are_correct()
            {
                var result = PerformAction();

                Assert.IsTrue(result.IsValid);
            }
        }

        public class ThereIsAValidationError : CreateLevelValidatorTests
        {
            [Test]
            public void When_name_is_missing()
            {
                Parameter.Name = "";

                var result = PerformAction();

                ExpectValidationError(result, ValidationMessages.MissingName);
            }

            [Test]
            public void When_start_time_is_missing()
            {
                Parameter.StartTime = DateTime.MinValue;

                var result = PerformAction();

                ExpectValidationError(result, ValidationMessages.MissingStartTime);
            }

            [Test]
            public void When_end_time_is_missing()
            {
                Parameter.EndTime = DateTime.MinValue;

                var result = PerformAction();

                ExpectValidationError(result, ValidationMessages.MissingEndTime);
            }

            [Test]
            public void When_end_time_is_before_start_time()
            {
                Parameter.StartTime = DateTime.Now;
                Parameter.EndTime = Parameter.StartTime.AddMinutes(-10);

                var result = PerformAction();

                ExpectValidationError(result, ValidationMessages.EndTimeGreaterThanStartTime);
            }

            [Test]
            public void When_class_minutes_is_not_positive()
            {
                Parameter.ClassMinutes = 0;

                var result = PerformAction();

                ExpectValidationError(result, ValidationMessages.InvalidClassMinutes);
            }

            [Test]
            public void When_number_of_classes_is_not_positive()
            {
                Parameter.ClassesInBlock = 0;

                var result = PerformAction();

                ExpectValidationError(result, ValidationMessages.InvalidClassesInBlock);
            }

            [Test]
            public void When_no_teachers_are_included()
            {
                Parameter.Teachers = null;
                UserRepositoryBuilder.WithUnsuccessfulGet();

                var result = PerformAction();

                ExpectValidationError(result, ValidationMessages.TeachersRequired);
            }

            [Test]
            public void When_teachers_dont_exist()
            {
                Parameter.Teachers = new List<ITeacher> { new Teacher(){ Id = 1 } };
                UserRepositoryBuilder.WithUnsuccessfulGet();

                var result = PerformAction();

                ExpectValidationError(result, ValidationMessages.InvalidTeachers);
            }

            [Test]
            public void When_proposed_teacher_isnt_set_up_as_a_teacher()
            {
                var user = new Teacher{Claims = ""};
                Parameter.Teachers = new List<ITeacher> { user };
                UserRepositoryBuilder.WithGet(user);

                var result = PerformAction();

                ExpectValidationError(result, ValidationMessages.InvalidTeachers);
            }
        }
    }
}
