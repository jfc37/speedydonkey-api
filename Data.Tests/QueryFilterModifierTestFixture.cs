using System;
using System.Collections.Generic;
using System.Linq;
using Data.Searches;
using Models;
using NUnit.Framework;

namespace Data.Tests
{
    [TestFixture]
    public class QueryFilterModifierTestFixture
    {
        private IList<User> _userCollectionSearchingOver;
        private IList<Level> _levelCollectionSearchingOver; 

        private QueryFilterModifier GetFilterDescriptor()
        {
            return new QueryFilterModifier(new ConditionExpressionHandlerFactory());
        }

        public class Given_a_valid_contains_search : QueryFilterModifierTestFixture
        {
            [Test]
            public void It_should_handle_a_contains_query()
            {
                _userCollectionSearchingOver = new[]
                {
                    new User {Email = "tim"},
                    new User {Email = "john"},
                    new User {Email = "timmy"},
                    new User {Email = "jess"},
                    new User {Email = "atimmy"},
                };

                var filterDescriptor = GetFilterDescriptor();

                var queryable = _userCollectionSearchingOver.AsQueryable();
                var searchStatement = new SearchStatement
                {
                    Condition = SearchKeyWords.Contains,
                    Element = SearchElements.Email,
                    Value = "tim"
                };
                var queryableWithFilter = filterDescriptor.ApplyStatementToQuery(searchStatement, queryable);

                var filteredResults = queryableWithFilter.ToList();
                foreach (var result in filteredResults)
                {
                    Assert.IsTrue(result.Email.Contains("tim"));
                }
            }

            [Test]
            public void It_should_return_no_results_when_nothing_matches()
            {
                _userCollectionSearchingOver = new[]
                {
                    new User {Email = "tim"},
                    new User {Email = "john"},
                    new User {Email = "timmy"},
                    new User {Email = "jess"},
                    new User {Email = "atimmy"},
                };

                var filterDescriptor = GetFilterDescriptor();

                var queryable = _userCollectionSearchingOver.AsQueryable();
                var searchStatement = new SearchStatement
                {
                    Condition = SearchKeyWords.Contains,
                    Element = SearchElements.Email,
                    Value = "sim"
                };
                var queryableWithFilter = filterDescriptor.ApplyStatementToQuery(searchStatement, queryable);

                var filteredResults = queryableWithFilter.ToList();
                Assert.IsEmpty(filteredResults);
            }
            [Test]
            public void It_should_be_case_insensitive()
            {
                _userCollectionSearchingOver = new[]
                {
                    new User {Email = "Tim"},
                    new User {Email = "john"},
                    new User {Email = "TIMMY"},
                    new User {Email = "jess"}
                };

                var filterDescriptor = GetFilterDescriptor();

                var queryable = _userCollectionSearchingOver.AsQueryable();
                var searchStatement = new SearchStatement
                {
                    Condition = SearchKeyWords.Contains,
                    Element = SearchElements.Email,
                    Value = "tIm"
                };
                var queryableWithFilter = filterDescriptor.ApplyStatementToQuery(searchStatement, queryable);

                var filteredResults = queryableWithFilter.ToList();
                Assert.AreEqual(2, filteredResults.Count);
                foreach (var result in filteredResults)
                {
                    Assert.IsTrue(result.Email.ToLower().Contains("tim"));
                }
            }
        }
        public class Given_a_valid_equals_search : QueryFilterModifierTestFixture
        {
            [Test]
            public void It_should_handle_an_equals_query_with_a_string()
            {
                _userCollectionSearchingOver = new[]
                {
                    new User {Email = "tim"},
                    new User {Email = "timmy"}
                };

                var filterDescriptor = GetFilterDescriptor();

                var queryable = _userCollectionSearchingOver.AsQueryable();
                var searchStatement = new SearchStatement
                {
                    Condition = SearchKeyWords.Equals,
                    Element = SearchElements.Email,
                    Value = "tim"
                };
                var queryableWithFilter = filterDescriptor.ApplyStatementToQuery(searchStatement, queryable);

                var filteredResults = queryableWithFilter.ToList();
                var result = filteredResults.Single();
                Assert.AreEqual("tim", result.Email);
            }

            [Test]
            public void It_should_handle_an_equals_query_with_a_number()
            {
                _levelCollectionSearchingOver = new[]
                {
                    new Level{ClassesInBlock = 50}, 
                    new Level{ClassesInBlock = 45}, 
                };

                var filterDescriptor = GetFilterDescriptor();

                var queryable = _levelCollectionSearchingOver.AsQueryable();
                var searchStatement = new SearchStatement
                {
                    Condition = SearchKeyWords.Equals,
                    Element = "ClassesInBlock",
                    Value = "45"
                };
                var queryableWithFilter = filterDescriptor.ApplyStatementToQuery(searchStatement, queryable);

                var filteredResults = queryableWithFilter.ToList();
                var result = filteredResults.Single();
                Assert.AreEqual(45, result.ClassesInBlock);
            }

            [Test]
            public void It_should_return_no_results_when_nothing_matches()
            {
                _userCollectionSearchingOver = new[]
                {
                    new User {Email = "tim"},
                    new User {Email = "timmy"}
                };

                var filterDescriptor = GetFilterDescriptor();

                var queryable = _userCollectionSearchingOver.AsQueryable();
                var searchStatement = new SearchStatement
                {
                    Condition = SearchKeyWords.Contains,
                    Element = SearchElements.Email,
                    Value = "sim"
                };
                var queryableWithFilter = filterDescriptor.ApplyStatementToQuery(searchStatement, queryable);

                var filteredResults = queryableWithFilter.ToList();
                Assert.IsEmpty(filteredResults);
            }
        }
        //public class Given_a_valid_greater_than_search : QueryFilterModifierTestFixture
        //{
        //    [Test]
        //    public void It_should_handle_query_on_a_number()
        //    {
        //        _levelCollectionSearchingOver = new[]
        //        {
        //            new Assignment{ FinalMarkPercentage = 80 },
        //            new Assignment{ FinalMarkPercentage = 50 }
        //        };

        //        var filterDescriptor = GetFilterDescriptor();

        //        var queryable = _levelCollectionSearchingOver.AsQueryable();
        //        var searchStatement = new SearchStatement
        //        {
        //            Condition = SearchKeyWords.GreaterThan,
        //            Element = "FinalMarkPercentage",
        //            Value = "55"
        //        };
        //        var queryableWithFilter = filterDescriptor.ApplyStatementToQuery(searchStatement, queryable);

        //        var filteredResults = queryableWithFilter.ToList();
        //        var matchingAssignment = filteredResults.Single();
        //        Assert.Greater(matchingAssignment.FinalMarkPercentage, 55);
        //    }

        //    [Test]
        //    public void It_should_handle_query_on_a_date()
        //    {
        //        _levelCollectionSearchingOver = new[]
        //        {
        //            new Assignment{ StartDate = DateTime.Today.AddMonths(-1) },
        //            new Assignment{ StartDate = DateTime.Today.AddMonths(1)}
        //        };

        //        var filterDescriptor = GetFilterDescriptor();

        //        var queryable = _levelCollectionSearchingOver.AsQueryable();
        //        var searchStatement = new SearchStatement
        //        {
        //            Condition = SearchKeyWords.GreaterThan,
        //            Element = "StartDate",
        //            Value = DateTime.Today.ToString("yyyy-MM-dd")
        //        };
        //        var queryableWithFilter = filterDescriptor.ApplyStatementToQuery(searchStatement, queryable);

        //        var filteredResults = queryableWithFilter.ToList();
        //        var matchingAssignment = filteredResults.Single();
        //        Assert.Greater(matchingAssignment.StartDate, DateTime.Today);
        //    }

        //    [Test]
        //    public void It_should_return_no_results_when_nothing_matches()
        //    {
        //        _levelCollectionSearchingOver = new[]
        //        {
        //            new Assignment{ FinalMarkPercentage = 80 },
        //            new Assignment{ FinalMarkPercentage = 50 }
        //        };

        //        var filterDescriptor = GetFilterDescriptor();

        //        var queryable = _levelCollectionSearchingOver.AsQueryable();
        //        var searchStatement = new SearchStatement
        //        {
        //            Condition = SearchKeyWords.GreaterThan,
        //            Element = "FinalMarkPercentage",
        //            Value = "85"
        //        };
        //        var queryableWithFilter = filterDescriptor.ApplyStatementToQuery(searchStatement, queryable);

        //        var filteredResults = queryableWithFilter.ToList();
        //        Assert.IsEmpty(filteredResults);
        //    }
        //}

        //public class Given_a_valid_less_than_search : QueryFilterModifierTestFixture
        //{
        //    [Test]
        //    public void It_should_handle_query_on_a_number()
        //    {
        //        _levelCollectionSearchingOver = new[]
        //        {
        //            new Assignment{ FinalMarkPercentage = 80 },
        //            new Assignment{ FinalMarkPercentage = 50 }
        //        };

        //        var filterDescriptor = GetFilterDescriptor();

        //        var queryable = _levelCollectionSearchingOver.AsQueryable();
        //        var searchStatement = new SearchStatement
        //        {
        //            Condition = SearchKeyWords.LessThan,
        //            Element = "FinalMarkPercentage",
        //            Value = "55"
        //        };
        //        var queryableWithFilter = filterDescriptor.ApplyStatementToQuery(searchStatement, queryable);

        //        var filteredResults = queryableWithFilter.ToList();
        //        var matchingAssignment = filteredResults.Single();
        //        Assert.Less(matchingAssignment.FinalMarkPercentage, 55);
        //    }

        //    [Test]
        //    public void It_should_handle_query_on_a_date()
        //    {
        //        _levelCollectionSearchingOver = new[]
        //        {
        //            new Assignment{ StartDate = DateTime.Today.AddMonths(-1) },
        //            new Assignment{ StartDate = DateTime.Today.AddMonths(1)}
        //        };

        //        var filterDescriptor = GetFilterDescriptor();

        //        var queryable = _levelCollectionSearchingOver.AsQueryable();
        //        var searchStatement = new SearchStatement
        //        {
        //            Condition = SearchKeyWords.LessThan,
        //            Element = "StartDate",
        //            Value = DateTime.Today.ToString("yyyy-MM-dd")
        //        };
        //        var queryableWithFilter = filterDescriptor.ApplyStatementToQuery(searchStatement, queryable);

        //        var filteredResults = queryableWithFilter.ToList();
        //        var matchingAssignment = filteredResults.Single();
        //        Assert.Less(matchingAssignment.StartDate, DateTime.Today);
        //    }

        //    [Test]
        //    public void It_should_return_no_results_when_nothing_matches()
        //    {
        //        _levelCollectionSearchingOver = new[]
        //        {
        //            new Assignment{ FinalMarkPercentage = 80 },
        //            new Assignment{ FinalMarkPercentage = 50 }
        //        };

        //        var filterDescriptor = GetFilterDescriptor();

        //        var queryable = _levelCollectionSearchingOver.AsQueryable();
        //        var searchStatement = new SearchStatement
        //        {
        //            Condition = SearchKeyWords.LessThan,
        //            Element = "FinalMarkPercentage",
        //            Value = "45"
        //        };
        //        var queryableWithFilter = filterDescriptor.ApplyStatementToQuery(searchStatement, queryable);

        //        var filteredResults = queryableWithFilter.ToList();
        //        Assert.IsEmpty(filteredResults);
        //    }
        //}
        public class Given_an_invalid_search : QueryFilterModifierTestFixture
        {
            [Test]
            public void It_should_throw_invalid_operation_exception_when_selector_doesnt_match_any_field()
            {
                _userCollectionSearchingOver = new[]
                {
                    new User {Email = "tim"},
                    new User {Email = "timmy"}
                };
                var queryable = _userCollectionSearchingOver.AsQueryable();
                var searchStatement = new SearchStatement
                {
                    Condition = "unknown",
                    Element = SearchElements.Email,
                    Value = "tim"
                };

                var filterDescriptor = GetFilterDescriptor();

                Assert.Throws<InvalidOperationException>(() => filterDescriptor.ApplyStatementToQuery(searchStatement, queryable));
            }
        }
    }
}
