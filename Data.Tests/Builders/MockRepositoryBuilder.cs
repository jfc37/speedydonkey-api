using System.Collections.Generic;
using System.Linq;
using Common;
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
                Mock.Setup(x => x.Queryable())
                .Returns(new List<T>().AsQueryable());
            }
            else
            {
                Mock.Setup(x => x.Queryable())
                .Returns(new List<T>
                {
                    response
                }.AsQueryable());
            }
            
            Mock.Setup(x => x.Get(It.IsAny<int>()))
                .Returns(response);
        }

        public MockRepositoryBuilder<T> WithGet(T entity)
        {
            SetupGet(entity);
            return this;
        }

        public MockRepositoryBuilder<T> WithGetAll()
        {
            Mock.Setup(x => x.Queryable())
                .Returns(new List<T>().AsQueryable());
            return this;
        }

        public MockRepositoryBuilder<T> WithGetAll(IEnumerable<T> entities)
        {
            Mock.Setup(x => x.Queryable())
                .Returns(entities.AsQueryable());
            return this;
        }
    }
}