using ActionHandlers;
using Autofac;
using Validation;

namespace ActionHandlersTests.Builders
{
    public class ActionHandlerOverlordBuilder
    {
        private IValidatorOverlord _validatorOverlord;
        private ILifetimeScope _container;

        public ActionHandlerOverlord Build()
        {
            return new ActionHandlerOverlord(_validatorOverlord, _container);
        }

        public ActionHandlerOverlordBuilder WithValidatorOverlord(IValidatorOverlord validatorOverlord)
        {
            _validatorOverlord = validatorOverlord;
            return this;
        }

        public ActionHandlerOverlordBuilder WithLifetimeScope(ILifetimeScope container)
        {
            _container = container;
            return this;
        }
    }
}
