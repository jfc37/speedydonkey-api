using System.Collections.Generic;
using System.Linq;
using Common;
using Data.Repositories;
using Models;

namespace Data.Searches
{
    public class EntitySearch<T> : IEntitySearch<T> where T : class, IDatabaseEntity, IEntity
    {
        private readonly IRepository<T> _repository;
        private readonly ISearchQueryParser _searchQueryParser;
        private readonly IQueryModifierFactory _queryModifierFactory;

        public EntitySearch(IRepository<T> repository, ISearchQueryParser searchQueryParser, IQueryModifierFactory queryModifierFactory)
        {
            _repository = repository;
            _searchQueryParser = searchQueryParser;
            _queryModifierFactory = queryModifierFactory;
        }

        public IList<T> Search(string q)
        {
            var searchStatements = _searchQueryParser.ParseQuery(q);
            var query = _repository.Queryable();

            foreach (var searchStatement in searchStatements)
            {
                var queryModifier = _queryModifierFactory.GetModifier(searchStatement.Condition);
                query = queryModifier.ApplyStatementToQuery(searchStatement, query);
            }

            return query.ToList();
        }
    }
}
