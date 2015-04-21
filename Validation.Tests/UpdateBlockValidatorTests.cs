using System;
using Data.Repositories;
using Data.Tests.Builders;
using Models;
using NUnit.Framework;
using Validation.Validators;

namespace Validation.Tests
{
    [TestFixture]
    public class UpdateBlockValidatorTests : ValidatorTests<UpdateBlockValidator, Block>
    {
        private MockRepositoryBuilder<Block> _repositoryBuilder;

        [SetUp]
        public void Setup()
        {
            _repositoryBuilder = new MockRepositoryBuilder<Block>()
                .WithSuccessfulGet();
            Parameter = new Block
            {
                Name = "name"
            };
        }

        protected override UpdateBlockValidator GetValidator()
        {
            return new UpdateBlockValidator(_repositoryBuilder.BuildObject());
        }

        public class ThereIsNoValidationErrors : UpdateBlockValidatorTests
        {
            [Test]
            public void When_all_inputs_are_correct()
            {
                var result = PerformAction();

                Assert.IsTrue(result.IsValid);
            }
        }

        public class ThereIsAValidationError : UpdateBlockValidatorTests
        {
            [Test]
            public void When_name_is_missing()
            {
                Parameter.Name = "";

                var result = PerformAction();

                ExpectValidationError(result, ValidationMessages.MissingName);
            }
            [Test]
            public void When_block_doesnt_exist()
            {
                _repositoryBuilder.WithUnsuccessfulGet();

                var result = PerformAction();

                ExpectValidationError(result, ValidationMessages.InvalidBlock);
            }
        }
    }
}
