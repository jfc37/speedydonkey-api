//using System;
//using System.Collections.Generic;
//using System.Linq;
//using Data.Searches;
//using Models;
//using NUnit.Framework;

//namespace Data.Tests
//{
//    [TestFixture]
//    public class QueryOrderByModifierTestFixture
//    {
//        private IList<Account> _userCollectionSearchingOver; 
//        //private IList<Assignment> _assignmentCollectionSearchingOver; 

//        private QueryOrderByModifier GetOrderByDescriptor()
//        {
//            return new QueryOrderByModifier();
//        }

//        public class Given_a_valid_order_by : QueryOrderByModifierTestFixture
//        {
//            [Test]
//            public void It_should_handle_ordering_by_a_string()
//            {
//                _userCollectionSearchingOver = new[]
//                {
//                    new Account {Email = "tim"},
//                    new Account {Email = "john"},
//                    new Account {Email = "timmy"},
//                    new Account {Email = "jess"},
//                    new Account {Email = "atimmy"},
//                };

//                var orderByModifier = GetOrderByDescriptor();

//                var queryable = _userCollectionSearchingOver.AsQueryable();
//                var searchStatement = new SearchStatement
//                {
//                    Condition = SearchKeyWords.OrderBy,
//                    Element = SearchElements.Email,
//                    Value = SearchKeyWords.Ascending
//                };
//                var queryableWithOrderBy = orderByModifier.ApplyStatementToQuery(searchStatement, queryable);

//                Assert.AreEqual(_userCollectionSearchingOver.OrderBy(x => x.Email), queryableWithOrderBy.ToList());
//            }

//            //[Test]
//            //public void It_should_handle_ordering_by_a_number()
//            //{
//            //    _assignmentCollectionSearchingOver = new[]
//            //    {
//            //        new Assignment{ FinalMarkPercentage = 20},
//            //        new Assignment{ FinalMarkPercentage = 100},
//            //        new Assignment{ FinalMarkPercentage = 45},
//            //        new Assignment{ FinalMarkPercentage = 87},
//            //        new Assignment{ FinalMarkPercentage = 12},
//            //    };

//            //    var orderByModifier = GetOrderByDescriptor();

//            //    var queryable = _assignmentCollectionSearchingOver.AsQueryable();
//            //    var searchStatement = new SearchStatement
//            //    {
//            //        Condition = SearchKeyWords.OrderBy,
//            //        Element = "finalmarkpercentage"
//            //    };
//            //    var queryableWithOrderBy = orderByModifier.ApplyStatementToQuery(searchStatement, queryable);

//            //    Assert.AreEqual(_assignmentCollectionSearchingOver.OrderBy(x => x.FinalMarkPercentage), queryableWithOrderBy.ToList());
//            //}

//            //[Test]
//            //public void It_should_handle_ordering_by_a_date()
//            //{
//            //    _assignmentCollectionSearchingOver = new[]
//            //    {
//            //        new Assignment{ StartDate = DateTime.Today.AddDays(-2)},
//            //        new Assignment{ StartDate = DateTime.Today.AddDays(-25)},
//            //        new Assignment{ StartDate = DateTime.Today.AddDays(2)},
//            //        new Assignment{ StartDate = DateTime.Today.AddDays(-5)},
//            //        new Assignment{ StartDate = DateTime.Today.AddDays(6)},
//            //    };

//            //    var orderByModifier = GetOrderByDescriptor();

//            //    var queryable = _assignmentCollectionSearchingOver.AsQueryable();
//            //    var searchStatement = new SearchStatement
//            //    {
//            //        Condition = SearchKeyWords.OrderBy,
//            //        Element = "startdate"
//            //    };
//            //    var queryableWithOrderBy = orderByModifier.ApplyStatementToQuery(searchStatement, queryable);

//            //    Assert.AreEqual(_assignmentCollectionSearchingOver.OrderBy(x => x.StartDate), queryableWithOrderBy.ToList());
//            //}

//            [Test]
//            public void It_should_handle_ordering_descending()
//            {
//                _userCollectionSearchingOver = new[]
//                {
//                    new Account {Email = "tim"},
//                    new Account {Email = "john"},
//                    new Account {Email = "timmy"},
//                    new Account {Email = "jess"},
//                    new Account {Email = "atimmy"},
//                };

//                var orderByModifier = GetOrderByDescriptor();

//                var queryable = _userCollectionSearchingOver.AsQueryable();
//                var searchStatement = new SearchStatement
//                {
//                    Condition = SearchKeyWords.OrderBy,
//                    Element = SearchElements.Email,
//                    Value = SearchKeyWords.Descending
//                };
//                var queryableWithOrderBy = orderByModifier.ApplyStatementToQuery(searchStatement, queryable);

//                Assert.AreEqual(_userCollectionSearchingOver.OrderByDescending(x => x.Email), queryableWithOrderBy.ToList());
//            }
//        }

//        public class Given_an_invalid_order_by_with_no_matching_property : QueryOrderByModifierTestFixture
//        {
//            [Test]
//            public void It_should_throw_exception()
//            {
//                _userCollectionSearchingOver = new[]
//                {
//                    new Account {Email = "tim"},
//                    new Account {Email = "john"},
//                    new Account {Email = "timmy"},
//                    new Account {Email = "jess"},
//                    new Account {Email = "atimmy"},
//                };

//                var orderByModifier = GetOrderByDescriptor();

//                var queryable = _userCollectionSearchingOver.AsQueryable();
//                var searchStatement = new SearchStatement
//                {
//                    Condition = SearchKeyWords.OrderBy,
//                    Element = "invalid",
//                    Value = SearchKeyWords.Ascending
//                };
//                Assert.Throws<ArgumentException>(() => orderByModifier.ApplyStatementToQuery(searchStatement, queryable));
//            }
//        }
//    }
//}
