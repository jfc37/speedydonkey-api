using System.Collections.Generic;
using Common.Tests.Builders.MockBuilders;
using Data.Repositories;
using Models;
using Moq;

namespace Data.Tests.Builders.MockBuilders
{
    public class MockCourseWorkGradeRepositoryBuilder : MockBuilder<ICourseWorkGradeRepository>
    {
        private readonly IList<CourseWorkGrade> _courseWorkGrades;

        public MockCourseWorkGradeRepositoryBuilder()
        {
            _courseWorkGrades = new List<CourseWorkGrade>();

            Mock.Setup(x => x.Get(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns((CourseWorkGrade) null);
        }

        protected override void BeforeBuild()
        {
            Mock.Setup(x => x.GetAll(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(_courseWorkGrades);
        }

        public MockCourseWorkGradeRepositoryBuilder WithSomeValidCourseWorkGrade()
        {
            _courseWorkGrades.Add(new CourseWorkGrade());
            return this;
        }

        public MockCourseWorkGradeRepositoryBuilder WithNoCourseWorkGrades()
        {
            _courseWorkGrades.Clear();
            return this;
        }

        public MockCourseWorkGradeRepositoryBuilder WithCourseWorkGrade(CourseWorkGrade courseWorkGrade)
        {
            _courseWorkGrades.Add(courseWorkGrade);

            Mock.Setup(x => x.Get(courseWorkGrade.CourseGrade.Student.Id, courseWorkGrade.CourseGrade.Course.Id, courseWorkGrade.CourseWork.Id))
                .Returns(courseWorkGrade);

            return this;
        }

        public MockCourseWorkGradeRepositoryBuilder WithSuccessfulCreation()
        {
            Mock.Setup(x => x.Create(It.IsAny<CourseWorkGrade>()))
                .Returns(new CourseWorkGrade());
            return this;
        }

        public MockCourseWorkGradeRepositoryBuilder WithSuccessfulDelete()
        {
            Mock.Setup(x => x.Delete(It.IsAny<CourseWorkGrade>()));
            return this;
        }

        public MockCourseWorkGradeRepositoryBuilder WithSuccessfulUpdate()
        {
            Mock.Setup(x => x.Update(It.IsAny<CourseWorkGrade>()))
                .Returns<CourseWorkGrade>(x => x);
            return this;
        }
    }
}