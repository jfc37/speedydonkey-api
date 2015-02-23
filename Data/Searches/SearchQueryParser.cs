using System;
using System.Collections.Generic;
using System.Linq;

namespace Data.Searches
{
    public interface ISearchQueryParser
    {
        IList<SearchStatement> ParseQuery(string q);
    }

    public class SearchQueryParser : ISearchQueryParser
    {
        public IList<SearchStatement> ParseQuery(string q)
        {
            var searchStatements = new List<SearchStatement>();
            if (!String.IsNullOrWhiteSpace(q))
            {
                var allQueries = q.Split(SearchSyntax.StatementSeperator.ToCharArray());
                foreach (var query in allQueries)
                {
                    var statementParserFactory = new StatementParserFactory();
                    var statementParser = statementParserFactory.GetStatementParser(query);
                    searchStatements.Add(statementParser.ParseStatement(query.Split(SearchSyntax.Seperator.ToCharArray())));
                }  
            }

            return searchStatements;
        }
    }

    public interface IStatementParserFactory
    {
        IStatementParser GetStatementParser(string statement);
    }

    public class StatementParserFactory : IStatementParserFactory
    {
        public IStatementParser GetStatementParser(string statement)
        {
            var splitStatement = statement.Split(SearchSyntax.Seperator.ToCharArray());
            if (IsIncludeStatement(splitStatement))
            {
                return new IncludeStatementParser();
            }
            if (IsTakeOrSkipStatement(splitStatement))
            {
                return new TakeSkipStatementParser();
            }
            if (IsOrderByStatement(splitStatement))
            {
                return new OrderByStatementParser();
            }
            if (IsFilterStatement(splitStatement))
            {
                return new FilterStatementParser();
            }
            throw new ArgumentException(String.Format("Don't know how to parse the query statement: {0}", statement));
        }

        private bool IsTakeOrSkipStatement(string[] splitStatement)
        {
            return splitStatement.Count() == 2 && (
                splitStatement[0].Equals(SearchKeyWords.Take, StringComparison.InvariantCultureIgnoreCase)
                || splitStatement[0].Equals(SearchKeyWords.Skip, StringComparison.InvariantCultureIgnoreCase));
        }

        private bool IsOrderByStatement(string[] splitStatement)
        {
            return splitStatement.Count() > 1 &&
                   splitStatement[0].Equals(SearchKeyWords.OrderBy, StringComparison.InvariantCultureIgnoreCase);
        }

        private bool IsFilterStatement(string[] splitStatement)
        {
            return splitStatement.Count() == 3;
        }

        private bool IsIncludeStatement(string[] splitStatement)
        {
            return splitStatement.Count() == 2 &&
                   splitStatement[0].Equals(SearchKeyWords.Include, StringComparison.InvariantCultureIgnoreCase);
        }
    }

    public interface IStatementParser
    {
        SearchStatement ParseStatement(string[] splitStatement);
    }

    public class FilterStatementParser : IStatementParser
    {
        public SearchStatement ParseStatement(string[] splitStatement)
        {
            return new SearchStatement
            {
                Element = splitStatement[0],
                Condition = splitStatement[1],
                Value = splitStatement[2],
            };
        }
    }

    public class IncludeStatementParser : IStatementParser
    {
        public SearchStatement ParseStatement(string[] splitStatement)
        {
            return new SearchStatement
            {
                Condition = splitStatement[0],
                Element = splitStatement[1]
            };
        }
    }

    public class TakeSkipStatementParser : IStatementParser
    {
        public SearchStatement ParseStatement(string[] splitStatement)
        {
            return new SearchStatement
            {
                Condition = splitStatement[0],
                Value = splitStatement[1]
            };
        }
    }

    public class OrderByStatementParser : IStatementParser
    {
        public SearchStatement ParseStatement(string[] splitStatement)
        {
            var searchStatement = new SearchStatement
            {
                Condition = splitStatement[0],
                Element = splitStatement[1],
            };

            if (splitStatement.Count() == 3)
                searchStatement.Value = splitStatement[2];

            return searchStatement;
        }
    }
}
