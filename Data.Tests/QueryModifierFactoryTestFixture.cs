using System;
using Data.Searches;
using NUnit.Framework;

namespace Data.Tests
{
    [TestFixture]
    public class QueryModifierFactoryTestFixture
    {
        private string _condition;

        private IQueryModifier PerformAction()
        {
            return new QueryModifierFactory().GetModifier(_condition);
        }
        public class GetModifier : QueryModifierFactoryTestFixture
        {
            [Test]
            public void It_should_return_correct_modifier_for_equals()
            {
                _condition = SearchKeyWords.Equals;

                var modifier = PerformAction();

                Assert.AreEqual(typeof(QueryFilterModifier), modifier.GetType());
            }

            [Test]
            public void It_should_return_correct_modifier_for_contains()
            {
                _condition = SearchKeyWords.Contains;

                var modifier = PerformAction();

                Assert.AreEqual(typeof(QueryFilterModifier), modifier.GetType());
            }

            [Test]
            public void It_should_return_correct_modifier_for_greater_than()
            {
                _condition = SearchKeyWords.GreaterThan;

                var modifier = PerformAction();

                Assert.AreEqual(typeof(QueryFilterModifier), modifier.GetType());
            }

            [Test]
            public void It_should_return_correct_modifier_for_less_than()
            {
                _condition = SearchKeyWords.LessThan;

                var modifier = PerformAction();

                Assert.AreEqual(typeof(QueryFilterModifier), modifier.GetType());
            }

            [Test]
            public void It_should_return_correct_modifier_for_order_by()
            {
                _condition = SearchKeyWords.OrderBy;

                var modifier = PerformAction();

                Assert.AreEqual(typeof(QueryOrderByModifier), modifier.GetType());
            }

            [Test]
            public void It_should_return_correct_modifier_for_take()
            {
                _condition = SearchKeyWords.Take;

                var modifier = PerformAction();

                Assert.AreEqual(typeof(QueryTakeModifier), modifier.GetType());
            }

            [Test]
            public void It_should_return_correct_modifier_for_skip()
            {
                _condition = SearchKeyWords.Skip;

                var modifier = PerformAction();

                Assert.AreEqual(typeof(QuerySkipModifier), modifier.GetType());
            }

            [Test]
            public void It_should_throw_exception_when_condition_is_unknown()
            {
                _condition = "something weird";

                Assert.Throws<ArgumentException>(() => PerformAction());
            }
        }
    }
}
