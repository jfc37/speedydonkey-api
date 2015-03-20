using System.Collections.Generic;
using System.Linq;
using Data.Searches;
using Models;
using NUnit.Framework;

namespace Data.Tests
{
    [TestFixture]
    public class QueryTakeModifierTestFixture
    {
        private IList<User> _userCollectionSearchingOver;

        private QueryTakeModifier GetTakeDescriptor()
        {
            return new QueryTakeModifier();
        }

        public class Given_a_valid_take : QueryTakeModifierTestFixture
        {
            [Test]
            public void It_should_limit_list_to_correct_number()
            {
                _userCollectionSearchingOver = new[]
                {
                    new User {Email = "tim"},
                    new User {Email = "john"},
                    new User {Email = "timmy"},
                    new User {Email = "jess"},
                    new User {Email = "atimmy"},
                };

                var takeModifier = GetTakeDescriptor();

                var queryable = _userCollectionSearchingOver.AsQueryable();
                var searchStatement = new SearchStatement
                {
                    Condition = SearchKeyWords.Take,
                    Value = "3"
                };
                var queryableWithTake = takeModifier.ApplyStatementToQuery(searchStatement, queryable);

                Assert.AreEqual(3, queryableWithTake.Count());
            }
        }

        public class Given_an_invalid_take : QueryTakeModifierTestFixture
        {
            [Test]
            public void It_should_not_affect_queryable()
            {
                _userCollectionSearchingOver = new[]
                {
                    new User {Email = "tim"},
                    new User {Email = "john"},
                    new User {Email = "timmy"},
                    new User {Email = "jess"},
                    new User {Email = "atimmy"},
                };

                var takeModifier = GetTakeDescriptor();

                var queryable = _userCollectionSearchingOver.AsQueryable();
                var searchStatement = new SearchStatement
                {
                    Condition = SearchKeyWords.Take,
                    Element = "invalid"
                };

                var result = takeModifier.ApplyStatementToQuery(searchStatement, queryable);

                Assert.AreEqual(queryable.ToList(), result.ToList());
            }
        }
    }
}
