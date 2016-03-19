using System;
using System.Collections.Generic;
using System.Linq;
using Data.Searches;
using NUnit.Framework;

namespace Data.Tests
{
    [TestFixture]
    public class SearchQueryParserTestFixture
    {
        private SearchQueryParserBuilder _parserBuilder;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _parserBuilder = new SearchQueryParserBuilder();
        }

        private SearchQueryParser GetParser()
        {
            return _parserBuilder.Build();
        }

        public class Given_an_invalid_query : SearchQueryParserTestFixture
        {
            [Test]
            public void It_should_throw_argument_exception()
            {
                Assert.Throws<ArgumentException>(() => GetParser().ParseQuery("invalid"));
            }
        }

        public class Given_a_filter_query : SearchQueryParserTestFixture
        {
            private string _q;

            private IList<SearchStatement> PerformAction()
            {
                return GetParser().ParseQuery(_q);
            }

            [Test]
            public void It_should_split_query_into_correct_number_of_search_statements()
            {
                const int expectedNumberOfSearchStatements = 6;
                var searchStatment = String.Format("blah{0}{1}{2}1", SearchSyntax.Seperator, SearchKeyWords.Equals, SearchSyntax.Seperator);
                _q = searchStatment;
                for (int i = 1; i < expectedNumberOfSearchStatements; i++)
                {
                    _q = String.Format("{0}{1}{2}", _q, SearchSyntax.StatementSeperator, searchStatment);
                }

                var result = PerformAction();

                Assert.AreEqual(expectedNumberOfSearchStatements, result.Count);
            }

            [Test]
            public void It_should_split_query_sections_into_correct_search_statements()
            {
                string searchElement = "blah";
                string searchValue = "1234";
                _q = String.Format("{0}{1}{2}{3}{4}", searchElement, SearchSyntax.Seperator, SearchKeyWords.Equals, SearchSyntax.Seperator, searchValue);

                var result = PerformAction();

                var searchStatement = result.Single();
                Assert.AreEqual(searchElement, searchStatement.Element);
                Assert.AreEqual(searchValue, searchStatement.Value);
            }
        }

        public class Given_a_take_or_skip_query : SearchQueryParserTestFixture
        {
            private string _q;

            private IList<SearchStatement> PerformAction()
            {
                return GetParser().ParseQuery(_q);
            }

            [Test]
            public void It_should_split_query_into_correct_number_of_statements()
            {
                const int expectedNumberOfSearchStatements = 6;
                var searchStatment = String.Format("{0}{1}something", SearchKeyWords.Take, SearchSyntax.Seperator);
                _q = searchStatment;
                for (int i = 1; i < expectedNumberOfSearchStatements; i++)
                {
                    _q = String.Format("{0}{1}{2}", _q, SearchSyntax.StatementSeperator, searchStatment);
                }

                var result = PerformAction();

                Assert.AreEqual(expectedNumberOfSearchStatements, result.Count);
            }
        }

        public class Given_an_order_by_query : SearchQueryParserTestFixture
        {
            private string _q;

            private IList<SearchStatement> PerformAction()
            {
                return GetParser().ParseQuery(_q);
            }

            [Test]
            public void It_should_split_query_into_correct_number_of_include_statements()
            {
                const int expectedNumberOfSearchStatements = 6;
                var searchStatment = String.Format("{0}{1}something{2}asc", SearchKeyWords.OrderBy, SearchSyntax.Seperator, SearchSyntax.Seperator);
                _q = searchStatment;
                for (int i = 1; i < expectedNumberOfSearchStatements; i++)
                {
                    _q = String.Format("{0}{1}{2}", _q, SearchSyntax.StatementSeperator, searchStatment);
                }

                var result = PerformAction();

                Assert.AreEqual(expectedNumberOfSearchStatements, result.Count);
            }
        }
    }

    public class SearchQueryParserBuilder
    {
        public SearchQueryParser Build()
        {
            return new SearchQueryParser();
        }
    }
}
