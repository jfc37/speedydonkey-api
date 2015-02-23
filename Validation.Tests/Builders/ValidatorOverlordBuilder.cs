using Autofac;

namespace Validation.Tests.Builders
{
    public class ValidatorOverlordBuilder
    {
        private ILifetimeScope _lifetimeScope;

        public ValidatorOverlordBuilder WithLifetimeScope(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
            return this;
        }

        public ValidatorOverlord Build()
        {
            return new ValidatorOverlord(_lifetimeScope);
        }
    }
}
