using System.Collections.Generic;
using Common.Tests.Builders.MockBuilders;
using Data.Repositories;
using Models;
using Moq;

namespace Data.Tests.Builders.MockBuilders
{
    public class MockCourseGradeRepositoryBuilder : MockBuilder<ICourseGradeRepository>
    {
        private readonly IList<CourseGrade> _courseGrades;

        public MockCourseGradeRepositoryBuilder()
        {
            _courseGrades = new List<CourseGrade>();

            Mock.Setup(x => x.Get(It.IsAny<int>(), It.IsAny<int>()))
                .Returns((CourseGrade) null);
        }

        protected override void BeforeBuild()
        {
            Mock.Setup(x => x.GetAll(It.IsAny<int>()))
                .Returns(_courseGrades);
        }

        public MockCourseGradeRepositoryBuilder WithSomeValidCourseGrade()
        {
            _courseGrades.Add(new CourseGrade());
            return this;
        }

        public MockCourseGradeRepositoryBuilder WithNoCourseGrades()
        {
            _courseGrades.Clear();
            return this;
        }

        public MockCourseGradeRepositoryBuilder WithCourseGrade(CourseGrade courseGrade)
        {
            _courseGrades.Add(courseGrade);

            Mock.Setup(x => x.Get(It.IsAny<int>(), courseGrade.Id))
                .Returns(courseGrade);

            return this;
        }
    }
}