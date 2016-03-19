using Common;
using Data.Repositories;
using Data.Searches;
using Models;

namespace Data.Tests.Builders
{
    public class EntitySearcherBuilder<T> where T : class, IDatabaseEntity, IEntity
    {
        private IRepository<T> _repository;
        private ISearchQueryParser _searchQueryParser;
        private IQueryModifierFactory _queryModifierFactory;

        public EntitySearch<T> Build()
        {
            return new EntitySearch<T>(_repository, _searchQueryParser, _queryModifierFactory);
        }

        public EntitySearcherBuilder<T> WithRepository(IRepository<T> userRepository)
        {
            _repository = userRepository;
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