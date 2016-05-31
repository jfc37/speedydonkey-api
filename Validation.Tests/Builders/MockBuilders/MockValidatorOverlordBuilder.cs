using Actions;
using Common.Tests.Builders.MockBuilders;
using Moq;

namespace Validation.Tests.Builders.MockBuilders
{
    public class MockValidatorOverlordBuilder : MockBuilder<IValidatorOverlord>
    {
        public MockValidatorOverlordBuilder WithValidInput<TAction, TObject>() where TAction : SystemAction<TObject>
        {
            Mock.Setup(x => x.Validate<TAction, TObject>(It.IsAny<TAction>()))
                .Returns<TObject>(t => new ValidationResultBuilder().WithNoErrors().Build());
            return this;
        }

        public MockValidatorOverlordBuilder WithInvalidInput<TAction, TObject>() where TAction : SystemAction<TObject>
        {
            Mock.Setup(x => x.Validate<TAction, TObject>(It.IsAny<TAction>()))
                .Returns<TObject>(t => new ValidationResultBuilder().WithError().Build());
            return this;
        }
    }
}
