using System;

namespace GenericSearch
{
    public interface IQueryModifierFactory
    {
        IQueryModifier GetModifier(string condition);
    }
    public class QueryModifierFactory : IQueryModifierFactory
    {
        public IQueryModifier GetModifier(string condition)
        {
            switch (condition)
            {
                case SearchKeyWords.Skip:
                    return new QuerySkipModifier();

                case SearchKeyWords.Take:
                    return new QueryTakeModifier();

                case SearchKeyWords.OrderBy:
                    return new QueryOrderByModifier();

                case SearchKeyWords.Include:
                    return new QueryIncludeModifier();

                case SearchKeyWords.Contains:
                case SearchKeyWords.Equals:
                case SearchKeyWords.GreaterThan:
                case SearchKeyWords.LessThan:
                    return new QueryFilterModifier(new ConditionExpressionHandlerFactory());

                default:
                    throw new ArgumentException(String.Format("Don't know how to modify a query based on {0}", condition));
            }
        }
    }
}