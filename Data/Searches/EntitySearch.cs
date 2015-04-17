using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using NHibernate;

namespace Data.Searches
{
    public class EntitySearch<T> : IEntitySearch<T> where T : class, IDatabaseEntity
    {
        private readonly ISession _context;
        private readonly ISearchQueryParser _searchQueryParser;
        private readonly IQueryModifierFactory _queryModifierFactory;

        public EntitySearch(ISession context, ISearchQueryParser searchQueryParser, IQueryModifierFactory queryModifierFactory)
        {
            _context = context;
            _searchQueryParser = searchQueryParser;
            _queryModifierFactory = queryModifierFactory;
        }

        public IList<T> Search(string q)
        {
            var searchStatements = _searchQueryParser.ParseQuery(q);
            var query = GetQueryable();

            foreach (var searchStatement in searchStatements)
            {
                var queryModifier = _queryModifierFactory.GetModifier(searchStatement.Condition);
                query = queryModifier.ApplyStatementToQuery(searchStatement, query);
            }

            query = SanatiseResults(query);

            return GetListFromQueryable(query);
        }

        private IQueryable<T> SanatiseResults(IQueryable<T> query)
        {
            return query.Where(x => !x.Deleted);
        }

        private static IList<T> GetListFromQueryable(IQueryable<T> query)
        {
            return query.ToList();
        }

        private IQueryable<T> GetQueryable()
        {
            var query = _context.CreateCriteria<T>().List<T>().AsQueryable();
            return query;
        }
    }
}
