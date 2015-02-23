using System.Collections.Generic;
using System.Linq;
using Common.Tests.Builders.MockBuilders;
using Data.Searches;
using Moq;

namespace Data.Tests.Builders.MockBuilders
{
    public class MockQueryModifierFactoryBuilder : MockBuilder<IQueryModifierFactory>
    {
        public Mock<IQueryModifier> MockModifier;

        public MockQueryModifierFactoryBuilder WithValidFilter<T>()
        {
            MockModifier = new Mock<IQueryModifier>();
            MockModifier.Setup(
                x =>
                    x.ApplyStatementToQuery(It.IsAny<SearchStatement>(), It.IsAny<IQueryable<T>>()))
                .Returns(new List<T>().AsQueryable());

            Mock.Setup(x => x.GetModifier(It.IsAny<string>()))
                .Returns(MockModifier.Object);

            return this;
        }
    }
}