using Data.Searches;
using NHibernate;

namespace Data.Tests.Builders
{
    public class EntitySearcherBuilder<T> where T : class
    {
        private ISession _context;
        private ISearchQueryParser _searchQueryParser;
        private IQueryModifierFactory _queryModifierFactory;

        public EntitySearch<T> Build()
        {
            return new EntitySearch<T>(_context, _searchQueryParser, _queryModifierFactory);
        }

        public EntitySearcherBuilder<T> WithContext(ISession userRepository)
        {
            _context = userRepository;
            return this;
        }

        public EntitySearcherBuilder<T> WithSearchStatementParser(ISearchQueryParser searchQueryParser)
        {
            _searchQueryParser = searchQueryParser;
            return this;
        }

        public EntitySearcherBuilder<T> WithQueryModifierFactory(IQueryModifierFactory queryModifierFactory)
        {
            _queryModifierFactory = queryModifierFactory;
            return this;
        }
    }
}