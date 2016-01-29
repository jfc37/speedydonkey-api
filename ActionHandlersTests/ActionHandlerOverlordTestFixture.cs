using ActionHandlers;
using ActionHandlersTests.Builders;
using Actions;
using Autofac.Core.Registration;
using Common.Tests.Builders;
using Data.Tests.Builders.MockBuilders;
using Models;
using Moq;
using NUnit.Framework;
using Validation.Tests.Builders.MockBuilders;

namespace ActionHandlersTests
{
    [TestFixture]
    public class ActionHandlerOverlordTestFixture
    {
        private MockValidatorOverlordBuilder _validatorOverlordBuilder;
        private ActionHandlerOverlordBuilder _actionHandlerOverlordBuilder;
        private LifetimeScopeBuilder _lifetimeScopeBuilder;

        private TestAction _actionToHandle;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _actionHandlerOverlordBuilder = new ActionHandlerOverlordBuilder();
            _validatorOverlordBuilder = new MockValidatorOverlordBuilder();
            _lifetimeScopeBuilder = new LifetimeScopeBuilder()
                .WithActionHandlersRegistered();

            _actionToHandle = new TestAction();
        }

        private ActionHandlerOverlord BuildOverlord()
        {
            return _actionHandlerOverlordBuilder
                .WithValidatorOverlord(_validatorOverlordBuilder.BuildObject())
                .WithLifetimeScope(_lifetimeScopeBuilder.Build())
                .Build();
        }

        public class Handle : ActionHandlerOverlordTestFixture
        {
            [SetUp]
            public void Setup()
            {
                _lifetimeScopeBuilder = new LifetimeScopeBuilder()
                .WithActionHandlersRegistered();
                _validatorOverlordBuilder.WithValidInput<TestAction, TestObject>();
            }

            [Test]
            public void It_should_call_the_validator()
            {
                var overlord = BuildOverlord();
                overlord.HandleAction<TestAction, TestObject>(_actionToHandle);

                _validatorOverlordBuilder.Mock.Verify(x => x.Validate<TestAction, TestObject>(It.IsAny<TestObject>()), Times.Once);
            }

            [Test]
            public void It_should_include_errors_that_validator_returns()
            {
                _validatorOverlordBuilder.WithInvalidInput<TestAction, TestObject>();

                var overlord = BuildOverlord();
                var actionResult = overlord.HandleAction<TestAction, TestObject>(_actionToHandle);

                Assert.IsFalse(actionResult.ValidationResult.IsValid);
            }

            [Test]
            public void It_should_have_no_errors_if_validator_returns_with_none()
            {
                _validatorOverlordBuilder.WithValidInput<TestAction, TestObject>();

                var overlord = BuildOverlord();
                var actionResult = overlord.HandleAction<TestAction, TestObject>(_actionToHandle);

                Assert.IsTrue(actionResult.ValidationResult.IsValid);
            }

            [Test]
            public void It_should_create_correct_action_handler()
            {
                _lifetimeScopeBuilder.WithActionHandlersRegistered();

                var overlord = BuildOverlord();
                var actionResult = overlord.HandleAction<TestAction, TestObject>(_actionToHandle);

                Assert.AreEqual("test object", actionResult.ActionResult.Message);
            }

            [Test]
            public void It_should_throw_not_registered_exception_when_handler_isnt_found()
            {
                _lifetimeScopeBuilder.WithNothingRegistered();

                var overlord = BuildOverlord();

                Assert.Throws<ComponentNotRegisteredException>(() => overlord.HandleAction<TestAction, TestObject>(_actionToHandle));
            }
        }

        internal class TestActionHandler : IActionHandler<TestAction, TestObject>
        {
            TestObject IActionHandler<TestAction, TestObject>.Handle(TestAction action)
            {
                return new TestObject();
            }
        }

        internal class TestAction : SystemAction<TestObject>
        {
        }

        internal class TestObject
        {
            public string Message
            {
                get { return "test object"; } 
            }
        }
    }
}
