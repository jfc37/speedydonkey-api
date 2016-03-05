using System.Collections.Generic;
using System.Linq;
using Common.Tests.Builders.MockBuilders;
using Data.Repositories;
using Data.Searches;
using Data.Tests.Builders;
using Data.Tests.Builders.MockBuilders;
using Models;
using Moq;
using NHibernate;
using NHibernate.Impl;
using NUnit.Framework;

namespace Data.Tests
{
    [TestFixture]
    public class EntitySearcherTestFixture
    {
        private EntitySearcherBuilder<User> _entitySearcherBuilder;
        private Mock<IRepository<User>> _repository;
        private MockSearchStatementParseBuilder _searchStatementParserBuilder;
        private MockQueryModifierFactoryBuilder _queryModifierFactoryBuilder;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _entitySearcherBuilder = new EntitySearcherBuilder<User>();
            _repository = new Mock<IRepository<User>>(MockBehavior.Loose);
            _searchStatementParserBuilder = new MockSearchStatementParseBuilder();
            _queryModifierFactoryBuilder = new MockQueryModifierFactoryBuilder();
        }

        private EntitySearch<User> GetUserSearch()
        {
            return _entitySearcherBuilder
                .WithRepository(_repository.Object)
                .WithSearchStatementParser(_searchStatementParserBuilder.BuildObject())
                .WithQueryModifierFactory(_queryModifierFactoryBuilder.BuildObject())
                .Build();
        }

        public class Search : EntitySearcherTestFixture
        {
            private readonly string _q = "";

            private IList<User> PerformSearch()
            {
                var userSearch = GetUserSearch();
                return userSearch.Search(_q);
            }

            [SetUp]
            public void Setup()
            {
                _searchStatementParserBuilder.WithValidQuery();
                _queryModifierFactoryBuilder.WithValidFilter<User>();
            }

            [Test]
            public void It_should_append_to_query_the_same_number_of_search_statements()
            {
                IList<SearchStatement> singleSearchStatement = new[]
                {
                    new SearchStatement()
                };
                _searchStatementParserBuilder.WithQueryReturningSearchStatement(singleSearchStatement);

                const string usernameSearch = "tim";
                _repository.SetReturnsDefault(new List<User>
                {
                    new User {Email = usernameSearch},
                    new User {Email = "timmy"}
                }.AsQueryable());

                PerformSearch();

                _queryModifierFactoryBuilder.MockModifier.Verify(x => x.ApplyStatementToQuery(It.IsAny<SearchStatement>(), It.IsAny<IQueryable<User>>()), Times.Exactly(singleSearchStatement.Count));
            }
        }
    }

    public class MockSessionBuilder : MockBuilder<ISession>
    {
        public MockSessionBuilder WithEntities<T>(List<T> entities) where T : class
        {
            var criteria = new MockCriteriaBuilder()
                .WithEntities(entities)
                .BuildObject();
            Mock.Setup(x => x.CreateCriteria<T>())
                .Returns(criteria);
            return this;
        }
    }

    public class MockCriteriaBuilder : MockBuilder<ICriteria> {
        public MockCriteriaBuilder WithEntities<T>(List<T> entities)
        {
            Mock.Setup(x => x.List<T>())
                .Returns(entities);
            return this;
        }
    }
}
