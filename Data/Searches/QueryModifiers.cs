using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Common;

namespace Data.Searches
{

    public interface IQueryModifier
    {
        IQueryable<T> ApplyStatementToQuery<T>(SearchStatement statement, IQueryable<T> queryable);
    }

    public class QueryOrderByModifier : IQueryModifier
    {
        public IQueryable<T> ApplyStatementToQuery<T>(SearchStatement statement, IQueryable<T> queryable)
        {
            if (statement.Value == SearchKeyWords.Descending)
                return queryable.OrderByDescending(statement.Element);

            return queryable.OrderBy(statement.Element);
        }
    }

    public class QueryTakeModifier : IQueryModifier
    {
        public IQueryable<T> ApplyStatementToQuery<T>(SearchStatement statement, IQueryable<T> queryable)
        {
            int numberToTake;
            return int.TryParse(statement.Value, out numberToTake) ? queryable.Take(numberToTake) : queryable;
        }
    }

    public class QuerySkipModifier : IQueryModifier
    {
        public IQueryable<T> ApplyStatementToQuery<T>(SearchStatement statement, IQueryable<T> queryable)
        {
            int numberToSkip;
            return int.TryParse(statement.Value, out numberToSkip) ? queryable.Skip(numberToSkip) : queryable;
        }
    }

    public class QueryIncludeModifier : IQueryModifier
    {
        public IQueryable<T> ApplyStatementToQuery<T>(SearchStatement statement, IQueryable<T> queryable)
        {
            return queryable.Include(statement.Element);
        }
    }

    public class QueryFilterModifier : IQueryModifier
    {
        private readonly IConditionExpressionHandlerFactory _conditionExpressionHandlerFactory;

        public QueryFilterModifier(IConditionExpressionHandlerFactory conditionExpressionHandlerFactory)
        {
            _conditionExpressionHandlerFactory = conditionExpressionHandlerFactory;
        }

        public IQueryable<T> ApplyStatementToQuery<T>(SearchStatement statement, IQueryable<T> queryable)
        {
            var parameterExpression = Expression.Parameter(typeof(T));
            var propertyOrField = Expression.PropertyOrField(parameterExpression, statement.Element);
            var binaryExpression = GetConditionExpression(statement.Value, statement.Condition, propertyOrField);
            
            var predicate = Expression.Lambda<Func<T, bool>>(binaryExpression, parameterExpression);
            return queryable.Where(predicate);
        }

        private Expression GetConditionExpression(string value, string searchCondition, MemberExpression propertyOrField)
        {
            var conditionExpressionHandler = _conditionExpressionHandlerFactory.GetConditionExpressionHandler(searchCondition);
            return conditionExpressionHandler.GetExpression(value, propertyOrField);
        }
    }
}