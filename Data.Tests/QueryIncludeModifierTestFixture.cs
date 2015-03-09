//using System;
//using System.Collections.Generic;
//using System.Linq;
//using Data.Searches;
//using Models;
//using NUnit.Framework;

//namespace Data.Tests
//{
//    [TestFixture]
//    public class QueryIncludeModifierTestFixture
//    {
//        public class Given_an_invalid_search_statement_element : QueryIncludeModifierTestFixture
//        {
//            [Test]
//            public void It_should_throw_exception()
//            {
//                var modifier = new QueryIncludeModifier();
//                Assert.Throws<ArgumentException>(() => modifier.ApplyStatementToQuery(new SearchStatement(), new List<Account>().AsQueryable()));

//            }
//        }

//        public class Given_an_valid_search_statement_element : QueryIncludeModifierTestFixture
//        {
//            [Test]
//            public void It_should_not_throw_exception()
//            {
//                var modifier = new QueryIncludeModifier();
//                Assert.DoesNotThrow(() => modifier.ApplyStatementToQuery(new SearchStatement { Element = "something" }, new List<Account>().AsQueryable()));

//            }
//        }
//    }
//}
