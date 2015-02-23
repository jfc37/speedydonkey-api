using System.Collections.Generic;
using System.Linq;
using Data.Searches;
using Data.Tests.Builders;
using Data.Tests.Builders.MockBuilders;
using Models;
using Moq;
using NUnit.Framework;

namespace Data.Tests
{
    [TestFixture]
    public class EntitySearcherTestFixture
    {
        private EntitySearcherBuilder<User> _entitySearcherBuilder;
        private MockDbContextBuilder _contextBuilder;
        private MockSearchStatementParseBuilder _searchStatementParserBuilder;
        private MockQueryModifierFactoryBuilder _queryModifierFactoryBuilder;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _entitySearcherBuilder = new EntitySearcherBuilder<User>();
            _contextBuilder = new MockDbContextBuilder();
            _searchStatementParserBuilder = new MockSearchStatementParseBuilder();
            _queryModifierFactoryBuilder = new MockQueryModifierFactoryBuilder();
        }

        private EntitySearch<User> GetUserSearch()
        {
            return _entitySearcherBuilder
                .WithContext(_contextBuilder.BuildObject())
                .WithSearchStatementParser(_searchStatementParserBuilder.BuildObject())
                .WithQueryModifierFactory(_queryModifierFactoryBuilder.BuildObject())
                .Build();
        }

        public class Search : EntitySearcherTestFixture
        {
            private string _q;

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
                IList<SearchStatement> singleSearchStatement = new []
                {
                    new SearchStatement()
                };
                _searchStatementParserBuilder.WithQueryReturningSearchStatement(singleSearchStatement);

                const string usernameSearch = "tim";
                _contextBuilder
                    .WithUser(new User {Username = usernameSearch})
                    .WithUser(new User {Username = "timmy"});

                PerformSearch();

                _queryModifierFactoryBuilder.MockModifier.Verify(x => x.ApplyStatementToQuery(It.IsAny<SearchStatement>(), It.IsAny<IQueryable<User>>()), Times.Exactly(singleSearchStatement.Count));
            }
        }
    }
}
