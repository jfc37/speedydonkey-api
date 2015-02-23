using System.Collections.Generic;
using Common.Tests.Builders.MockBuilders;
using Data.Searches;
using Moq;

namespace Data.Tests.Builders.MockBuilders
{
    public class MockSearchBuilder<T> : MockBuilder<IEntitySearch<T>> where T : class, new()
    {
        public MockSearchBuilder<T> WithQueryMatchings()
        {
            Mock.Setup(x => x.Search(It.IsAny<string>()))
                .Returns(new List<T>{new T()});
            return this;
        }

        public MockSearchBuilder<T> WithNoQueryMatchings()
        {
            Mock.Setup(x => x.Search(It.IsAny<string>()))
                .Returns(new List<T>());
            return this;
        }
    }
}