using System.Linq.Expressions;
using System.Reflection;
using Common;

namespace Data.Searches
{
    public interface IConditionExpressionHandler
    {
        Expression GetExpression(string value, MemberExpression propertyOrField);
    }

    public class EqualsConditionExpressionHandler : IConditionExpressionHandler
    {
        public Expression GetExpression(string value, MemberExpression propertyOrField)
        {
            var valueInCorrectType = value.ConvertToType(propertyOrField.Type);
            return Expression.Equal(propertyOrField, Expression.Constant(valueInCorrectType));
        }
    }

    public class ContainsConditionExpressionHandler : IConditionExpressionHandler
    {
        public Expression GetExpression(string value, MemberExpression propertyOrField)
        {
            MethodInfo method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            var someValue = Expression.Constant(value, typeof(string));
            var containsMethodExp = Expression.Call(propertyOrField, method, someValue);
            return containsMethodExp;
        }
    }

    public class GreaterThanConditionExpressionHandler : IConditionExpressionHandler
    {
        public Expression GetExpression(string value, MemberExpression propertyOrField)
        {
            var valueInCorrectType = value.ConvertToType(propertyOrField.Type);
            return Expression.GreaterThan(propertyOrField, Expression.Constant(valueInCorrectType));
        }
    }

    public class LessThanConditionExpressionHandler : IConditionExpressionHandler
    {
        public Expression GetExpression(string value, MemberExpression propertyOrField)
        {
            var valueInCorrectType = value.ConvertToType(propertyOrField.Type);
            return Expression.LessThan(propertyOrField, Expression.Constant(valueInCorrectType));
        }
    }
}