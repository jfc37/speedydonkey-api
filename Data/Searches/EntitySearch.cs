using System.Collections.Generic;
using System.Linq;

namespace Data.Searches
{
    public class EntitySearch<T> : IEntitySearch<T> where T : class
    {
        private readonly ISpeedyDonkeyDbContext _context;
        private readonly ISearchQueryParser _searchQueryParser;
        private readonly IQueryModifierFactory _queryModifierFactory;

        public EntitySearch(ISpeedyDonkeyDbContext context, ISearchQueryParser searchQueryParser, IQueryModifierFactory queryModifierFactory)
        {
            _context = context;
            _searchQueryParser = searchQueryParser;
            _queryModifierFactory = queryModifierFactory;
        }

        public IList<T> Search(string q)
        {
            var searchStatements = _searchQueryParser.ParseQuery(q);
            var query = _context.GetSetOfType<T>().AsQueryable();
           
            foreach (var searchStatement in searchStatements)
            {
                var queryModifier = _queryModifierFactory.GetModifier(searchStatement.Condition);
                query = queryModifier.ApplyStatementToQuery(searchStatement, query);
            }

            return query.ToList();
        }
    }
}
