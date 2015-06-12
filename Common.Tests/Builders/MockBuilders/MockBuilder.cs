using Moq;

namespace Common.Tests.Builders.MockBuilders
{
    public abstract class MockBuilder<T> where T : class
    {
        public Mock<T> Mock { get; private set; }

        protected MockBuilder()
        {
            Mock = new Mock<T>(MockBehavior.Loose);
        }

        public T BuildObject()
        {
            BeforeBuild();
            return Mock.Object;
        }

        protected virtual void BeforeBuild() { }
    }
}
