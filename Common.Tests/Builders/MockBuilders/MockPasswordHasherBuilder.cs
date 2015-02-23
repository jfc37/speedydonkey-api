using ActionHandlers;
using Moq;

namespace Common.Tests.Builders.MockBuilders
{
    public class MockPasswordHasherBuilder : MockBuilder<IPasswordHasher>
    {
        public MockPasswordHasherBuilder WithHashCreation()
        {
            Mock.Setup(x => x.CreateHash(It.IsAny<string>()))
                .Returns("some hash");
            return this;
        }

        public MockPasswordHasherBuilder WithFailedPasswordValidation()
        {
            Mock.Setup(x => x.ValidatePassword(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(false);
            return this;
        }

        public MockPasswordHasherBuilder WithSuccessfulPasswordValidation()
        {
            Mock.Setup(x => x.ValidatePassword(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(true);
            return this;
        }
    }
}
