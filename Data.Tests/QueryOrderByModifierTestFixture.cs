using System;
using System.Collections.Generic;
using System.Linq;
using Data.Searches;
using Models;
using NUnit.Framework;

namespace Data.Tests
{
    [TestFixture]
    public class QueryOrderByModifierTestFixture
    {
        private IList<User> _userCollectionSearchingOver;
        private IList<Block> _blockCollectionSearchingOver; 

        private QueryOrderByModifier GetOrderByDescriptor()
        {
            return new QueryOrderByModifier();
        }

        public class Given_a_valid_order_by : QueryOrderByModifierTestFixture
        {
            [Test]
            public void It_should_handle_ordering_by_a_string()
            {
                _userCollectionSearchingOver = new[]
                {
                    new User {Email = "tim"},
                    new User {Email = "john"},
                    new User {Email = "timmy"},
                    new User {Email = "jess"},
                    new User {Email = "atimmy"},
                };

                var orderByModifier = GetOrderByDescriptor();

                var queryable = _userCollectionSearchingOver.AsQueryable();
                var searchStatement = new SearchStatement
                {
                    Condition = SearchKeyWords.OrderBy,
                    Element = SearchElements.Email,
                    Value = SearchKeyWords.Ascending
                };
                var queryableWithOrderBy = orderByModifier.ApplyStatementToQuery(searchStatement, queryable);

                Assert.AreEqual(_userCollectionSearchingOver.OrderBy(x => x.Email), queryableWithOrderBy.ToList());
            }

            [Test]
            public void It_should_handle_ordering_by_a_number()
            {
                _blockCollectionSearchingOver = new[]
                {
                    new Block{ NumberOfClasses = 20},
                    new Block{ NumberOfClasses = 100},
                    new Block{ NumberOfClasses = 45},
                    new Block{ NumberOfClasses = 87},
                    new Block{ NumberOfClasses = 12},
                };

                var orderByModifier = GetOrderByDescriptor();

                var queryable = _blockCollectionSearchingOver.AsQueryable();
                var searchStatement = new SearchStatement
                {
                    Condition = SearchKeyWords.OrderBy,
                    Element = "numberofclasses"
                };
                var queryableWithOrderBy = orderByModifier.ApplyStatementToQuery(searchStatement, queryable);

                Assert.AreEqual(_blockCollectionSearchingOver.OrderBy(x => x.NumberOfClasses), queryableWithOrderBy.ToList());
            }

            [Test]
            public void It_should_handle_ordering_by_a_date()
            {
                _blockCollectionSearchingOver = new[]
                {
                    new Block{ StartDate = DateTime.Today.AddDays(-2)},
                    new Block{ StartDate = DateTime.Today.AddDays(-25)},
                    new Block{ StartDate = DateTime.Today.AddDays(2)},
                    new Block{ StartDate = DateTime.Today.AddDays(-5)},
                    new Block{ StartDate = DateTime.Today.AddDays(6)},
                };

                var orderByModifier = GetOrderByDescriptor();

                var queryable = _blockCollectionSearchingOver.AsQueryable();
                var searchStatement = new SearchStatement
                {
                    Condition = SearchKeyWords.OrderBy,
                    Element = "startdate"
                };
                var queryableWithOrderBy = orderByModifier.ApplyStatementToQuery(searchStatement, queryable);

                Assert.AreEqual(_blockCollectionSearchingOver.OrderBy(x => x.StartDate), queryableWithOrderBy.ToList());
            }

            [Test]
            public void It_should_handle_ordering_descending()
            {
                _userCollectionSearchingOver = new[]
                {
                    new User {Email = "tim"},
                    new User {Email = "john"},
                    new User {Email = "timmy"},
                    new User {Email = "jess"},
                    new User {Email = "atimmy"},
                };

                var orderByModifier = GetOrderByDescriptor();

                var queryable = _userCollectionSearchingOver.AsQueryable();
                var searchStatement = new SearchStatement
                {
                    Condition = SearchKeyWords.OrderBy,
                    Element = SearchElements.Email,
                    Value = SearchKeyWords.Descending
                };
                var queryableWithOrderBy = orderByModifier.ApplyStatementToQuery(searchStatement, queryable);

                Assert.AreEqual(_userCollectionSearchingOver.OrderByDescending(x => x.Email), queryableWithOrderBy.ToList());
            }
        }

        public class Given_an_invalid_order_by_with_no_matching_property : QueryOrderByModifierTestFixture
        {
            [Test]
            public void It_should_throw_exception()
            {
                _userCollectionSearchingOver = new[]
                {
                    new User {Email = "tim"},
                    new User {Email = "john"},
                    new User {Email = "timmy"},
                    new User {Email = "jess"},
                    new User {Email = "atimmy"},
                };

                var orderByModifier = GetOrderByDescriptor();

                var queryable = _userCollectionSearchingOver.AsQueryable();
                var searchStatement = new SearchStatement
                {
                    Condition = SearchKeyWords.OrderBy,
                    Element = "invalid",
                    Value = SearchKeyWords.Ascending
                };
                Assert.Throws<ArgumentException>(() => orderByModifier.ApplyStatementToQuery(searchStatement, queryable));
            }
        }
    }
}
