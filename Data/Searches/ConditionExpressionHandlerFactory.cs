using System;

namespace Data.Searches
{
    public interface IConditionExpressionHandlerFactory
    {
        IConditionExpressionHandler GetConditionExpressionHandler(string searchCondition);
    }

    public class ConditionExpressionHandlerFactory : IConditionExpressionHandlerFactory
    {
        public IConditionExpressionHandler GetConditionExpressionHandler(string searchCondition)
        {
            switch (searchCondition)
            {
                case SearchKeyWords.Equals:
                    return new EqualsConditionExpressionHandler();

                case SearchKeyWords.Contains:
                    return new ContainsConditionExpressionHandler();

                case SearchKeyWords.GreaterThan:
                    return new GreaterThanConditionExpressionHandler();

                case SearchKeyWords.LessThan:
                    return new LessThanConditionExpressionHandler();

                default:
                    throw new InvalidOperationException(String.Format("Don't know how to convert condition {0} into SQL", searchCondition));
            }
        }
    }
}