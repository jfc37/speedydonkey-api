using System.Collections.Generic;
using System.Linq;
using Data;

namespace GenericSearch
{
    public interface IEntitySearch<T> where T : class
    {
        IList<T> Search(string q);
    }

    public class EntitySearch<T> : IEntitySearch<T> where T : class
    {
        private readonly IStudyBuddyDbContext _context;
        private readonly ISearchQueryParser _searchQueryParser;
        private readonly IQueryModifierFactory _queryModifierFactory;

        public EntitySearch(IStudyBuddyDbContext context, ISearchQueryParser searchQueryParser, IQueryModifierFactory queryModifierFactory)
        {
            _context = context;
            _searchQueryParser = searchQueryParser;
            _queryModifierFactory = queryModifierFactory;
        }

        public IList<T> Search(string q)
        {
            var searchStatements = _searchQueryParser.ParseQuery(q);
            var query = Queryable.AsQueryable<T>(_context.GetSetOfType<T>());
           
            foreach (var searchStatement in searchStatements)
            {
                var queryModifier = _queryModifierFactory.GetModifier(searchStatement.Condition);
                query = queryModifier.ApplyStatementToQuery(searchStatement, query);
            }

            return query.ToList();
        }
    }
}
