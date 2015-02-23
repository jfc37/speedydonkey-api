using System.Collections.Generic;
using Common.Tests.Builders.MockBuilders;
using Data.Searches;
using Moq;

namespace Data.Tests.Builders.MockBuilders
{
    public class MockSearchStatementParseBuilder : MockBuilder<ISearchQueryParser>
    {
        public MockSearchStatementParseBuilder WithValidQuery()
        {
            Mock.Setup(x => x.ParseQuery(It.IsAny<string>()))
                .Returns(new[]
                {
                    new SearchStatement()
                });
            return this;
        }

        public MockSearchStatementParseBuilder WithQueryReturningSearchStatement(IList<SearchStatement> searchStatements)
        {
            Mock.Setup(x => x.ParseQuery(It.IsAny<string>()))
                .Returns(searchStatements);
            return this;
        }
    }
}