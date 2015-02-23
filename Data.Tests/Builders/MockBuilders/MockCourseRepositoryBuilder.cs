using System.Collections.Generic;
using Common.Tests.Builders.MockBuilders;
using Data.Repositories;
using Models;
using Moq;

namespace Data.Tests.Builders.MockBuilders
{
    public class MockCourseRepositoryBuilder : MockBuilder<ICourseRepository>
    {
        private readonly IList<Course> _courses;

        public MockCourseRepositoryBuilder()
        {
            _courses = new List<Course>();

            Mock.Setup(x => x.Get(It.IsAny<int>()))
                .Returns((Course) null);
        }

        protected override void BeforeBuild()
        {
            Mock.Setup(x => x.GetAll())
                .Returns(_courses);
        }

        public MockCourseRepositoryBuilder WithSomeValidCourse()
        {
            _courses.Add(new Course());
            return this;
        }

        public MockCourseRepositoryBuilder WithNoCourses()
        {
            _courses.Clear();

            Mock.Setup(x => x.Get(It.IsAny<int>()))
                .Returns((Course)null);
            return this;
        }

        public MockCourseRepositoryBuilder WithCourse(Course course)
        {
            _courses.Add(course);

            Mock.Setup(x => x.Get(course.Id))
                .Returns(course);

            return this;
        }

        public MockCourseRepositoryBuilder WithSuccessfulCreation()
        {
            Mock.Setup(x => x.Create(It.IsAny<Course>()))
                .Returns<Course>(x => new Course { Id = 543 });

            return this;
        }

        public MockCourseRepositoryBuilder WithSuccessfulUpdate()
        {
            Mock.Setup(x => x.Update(It.IsAny<Course>()))
                .Returns<Course>(x => x);

            return this;
        }

        public MockCourseRepositoryBuilder WithSuccessfulDelete()
        {
            Mock.Setup(x => x.Delete(It.IsAny<Course>()));

            return this;
        }
    }
}