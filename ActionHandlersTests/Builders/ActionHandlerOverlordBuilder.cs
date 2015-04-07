using ActionHandlers;
using Autofac;
using Data;
using Validation;

namespace ActionHandlersTests.Builders
{
    public class ActionHandlerOverlordBuilder
    {
        private IValidatorOverlord _validatorOverlord;
        private ILifetimeScope _container;
        private IActivityLogger _activityLogger;

        public ActionHandlerOverlord Build()
        {
            return new ActionHandlerOverlord(_validatorOverlord, _container, _activityLogger);
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

        public ActionHandlerOverlordBuilder WithActivityLogger(IActivityLogger activityLogger)
        {
            _activityLogger = activityLogger;
            return this;
        }
    }
}
