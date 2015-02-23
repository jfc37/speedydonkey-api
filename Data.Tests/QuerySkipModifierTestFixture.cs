using System.Collections.Generic;
using System.Linq;
using Data.Searches;
using Models;
using NUnit.Framework;

namespace Data.Tests
{
    [TestFixture]
    public class QuerySkipModifierTestFixture
    {
        private IList<User> _userCollectionSearchingOver; 

        private QuerySkipModifier GetSkipDescriptor()
        {
            return new QuerySkipModifier();
        }

        public class Given_a_valid_skip : QuerySkipModifierTestFixture
        {
            [Test]
            public void It_should_limit_list_to_correct_number()
            {
                _userCollectionSearchingOver = new[]
                {
                    new User {Username = "tim"},
                    new User {Username = "john"},
                    new User {Username = "timmy"},
                    new User {Username = "jess"},
                    new User {Username = "atimmy"},
                };

                var skipModifier = GetSkipDescriptor();

                var queryable = _userCollectionSearchingOver.AsQueryable();
                var searchStatement = new SearchStatement
                {
                    Condition = SearchKeyWords.Skip,
                    Value = "3"
                };
                var queryableWithSkip = skipModifier.ApplyStatementToQuery(searchStatement, queryable);

                Assert.AreEqual(2, queryableWithSkip.Count());
            }
        }

        public class Given_an_invalid_skip : QuerySkipModifierTestFixture
        {
            [Test]
            public void It_should_not_affect_queryable()
            {
                _userCollectionSearchingOver = new[]
                {
                    new User {Username = "tim"},
                    new User {Username = "john"},
                    new User {Username = "timmy"},
                    new User {Username = "jess"},
                    new User {Username = "atimmy"},
                };

                var skipModifier = GetSkipDescriptor();

                var queryable = _userCollectionSearchingOver.AsQueryable();
                var searchStatement = new SearchStatement
                {
                    Condition = SearchKeyWords.Skip,
                    Element = "invalid"
                };

                var result = skipModifier.ApplyStatementToQuery(searchStatement, queryable);

                Assert.AreEqual(queryable.ToList(), result.ToList());
            }
        }
    }
}
