using System.Collections.Generic;
using Common.Tests.Builders.MockBuilders;
using Data.Repositories;
using Models;
using Moq;

namespace Data.Tests.Builders
{
    public class MockRepositoryBuilder<T> : MockBuilder<IRepository<T>> where T : class, IEntity, new()
    {
        public T CreatedEntity { get; set; }
        public T UpdatedEntity { get; set; }

        public MockRepositoryBuilder<T> WithCreate()
        {
            Mock.Setup(x => x.Create(It.IsAny<T>()))
                .Returns<T>(x => x)
                .Callback<T>(x => CreatedEntity = x);

            return this;
        }

        public MockRepositoryBuilder<T> WithUpdate()
        {
            Mock.Setup(x => x.Update(It.IsAny<T>()))
                .Returns<T>(x => x)
                .Callback<T>(x => UpdatedEntity = x);

            return this;
        }

        public MockRepositoryBuilder<T> WithSuccessfulGet()
        {
            SetupGet(new T());
            return this;
        }

        public MockRepositoryBuilder<T> WithUnsuccessfulGet()
        {
            SetupGet(null);
            return this;
        }

        private void SetupGet(T response)
        {
            if (response == null)
            {
                Mock.Setup(x => x.GetAll())
                .Returns(new List<T>());
                Mock.Setup(x => x.GetAllWithChildren(It.IsAny<IList<string>>()))
                    .Returns(new List<T>());
            }
            else
            {
                Mock.Setup(x => x.GetAll())
                .Returns(new List<T>
                {
                    response
                });
                Mock.Setup(x => x.GetAllWithChildren(It.IsAny<IList<string>>()))
                    .Returns(new[]
                {
                    response
                });
            }

            
            Mock.Setup(x => x.Get(It.IsAny<int>()))
                .Returns(response);
            Mock.Setup(x => x.GetWithChildren(It.IsAny<int>(), It.IsAny<IList<string>>()))
                .Returns(response);
        }

        public MockRepositoryBuilder<T> WithGet(T entity)
        {
            SetupGet(entity);
            return this;
        }

        public MockRepositoryBuilder<T> WithGetAll()
        {
            Mock.Setup(x => x.GetAll())
                .Returns(new List<T>());
            Mock.Setup(x => x.GetAllWithChildren(It.IsAny<IList<string>>()))
                .Returns(new List<T>());
            return this;
        }

        public MockRepositoryBuilder<T> WithGetAll(IEnumerable<T> entities)
        {
            Mock.Setup(x => x.GetAll())
                .Returns(entities);
            Mock.Setup(x => x.GetAllWithChildren(It.IsAny<IList<string>>()))
                .Returns(entities);
            return this;
        }
    }
}