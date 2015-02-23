using System.Collections.Generic;
using Common.Tests.Builders.MockBuilders;
using Data.Repositories;
using Models;
using Moq;

namespace Data.Tests.Builders.MockBuilders
{
    public class MockCourseWorkRepositoryBuilder<TCourseWork> : MockBuilder<ICourseWorkRepository<TCourseWork>> where TCourseWork : CourseWork
    {
        private readonly IList<TCourseWork> _courseWorks;

        public MockCourseWorkRepositoryBuilder()
        {
            _courseWorks = new List<TCourseWork>();

            Mock.Setup(x => x.Get(It.IsAny<int>()))
                .Returns((TCourseWork) null);
        }

        protected override void BeforeBuild()
        {
            Mock.Setup(x => x.GetAll())
                .Returns(_courseWorks);
        }

        public MockCourseWorkRepositoryBuilder<TCourseWork> WithSuccessfulDelete()
        {
            Mock.Setup(x => x.Delete(It.IsAny<TCourseWork>()));

            return this;
        }

        public MockCourseWorkRepositoryBuilder<TCourseWork> WithCourseWork(TCourseWork courseWork)
        {
            Mock.Setup(x => x.Get(It.IsAny<int>()))
                .Returns(courseWork);
            return this;
        }
    }
}