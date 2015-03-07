using Common.Tests.Builders.MockBuilders;
using Data.Repositories;
using Models;
using Moq;

namespace Data.Tests.Builders
{
    public class MockRepositoryBuilder<T> : MockBuilder<IRepository<T>> where T : IEntity
    {
        public MockRepositoryBuilder<T> WithCreate()
        {
            Mock.Setup(x => x.Create(It.IsAny<T>()))
                .Returns<T>(x => x);

            return this;
        }
    }
}