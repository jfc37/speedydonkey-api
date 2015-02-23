using System.Linq;
using Data.Repositories;
using Data.Tests.Builders;
using Data.Tests.Builders.MockBuilders;
using Models;
using Models.Tests.Builders;
using NUnit.Framework;

namespace Data.Tests
{
    [TestFixture]
    public class CourseRepositoryTestFixture
    {
        private CourseRepositoryBuilder _courseRepositoryBuilder;
        private MockDbContextBuilder _contextBuilder;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _courseRepositoryBuilder = new CourseRepositoryBuilder();
            _contextBuilder = new MockDbContextBuilder();
        }

        private CourseRepository BuildCourseRepository()
        {
            return _courseRepositoryBuilder
                .WithContext(_contextBuilder.BuildObject())
                .Build();
        }

        public class Get : CourseRepositoryTestFixture
        {
            private int _courseId;

            [SetUp]
            public void Setup()
            {
                _courseId = 553;
            }

            [Test]
            public void It_should_return_null_when_no_course_id_isnt_found()
            {
                _contextBuilder.WithNoCourses();

                var courseRepository = BuildCourseRepository();
                var course = courseRepository.Get(_courseId);

                Assert.IsNull(course);
            }

            [Test]
            public void It_should_return_all_courses()
            {
                var savedCourse = new Course {Id = _courseId};
                _contextBuilder.WithCourse(savedCourse);

                var courseRepository = BuildCourseRepository();
                var course = courseRepository.Get(_courseId);

                Assert.AreSame(savedCourse, course);
            }
        }

        public class GetAll : CourseRepositoryTestFixture
        {
            [Test]
            public void It_should_return_empty_when_no_courses_exists()
            {
                _contextBuilder.WithNoCourses();

                var courseRepository = BuildCourseRepository();
                var results = courseRepository.GetAll();

                Assert.IsEmpty(results);
            }

            [Test]
            public void It_should_return_all_courses()
            {
                _contextBuilder.WithCourse()
                    .WithCourse()
                    .WithCourse()
                    .WithCourse();

                var courseRepository = BuildCourseRepository();
                var results = courseRepository.GetAll();

                Assert.AreEqual(_contextBuilder.BuildObject().Courses.Count(), results.Count());
            }
        }

        public class Create : CourseRepositoryTestFixture
        {
            private Course _course;

            [SetUp]
            public void Setup()
            {
                var professor = new Professor {Id = 6555};
                _course = new CourseBuilder()
                    .WithProfessor(professor)
                    .Build();
                _contextBuilder.WithNoCourses()
                    .WithProfessor(professor);
            }

            [Test]
            public void It_should_add_course_to_the_database()
            {
                var courseRepository = BuildCourseRepository();
                courseRepository.Create(_course);

                Assert.IsNotEmpty(_contextBuilder.BuildObject().Courses.ToList());
            }
        }

        public class Update : CourseRepositoryTestFixture
        {
            [Test]
            public void It_should_not_add_new_course_to_database_on_update()
            {
                Course course = new CourseBuilder()
                    .WithId(1)
                    .Build();
                _contextBuilder.WithCourse(course);

                var repository = BuildCourseRepository();
                repository.Update(course);

                Assert.AreEqual(1, _contextBuilder.Mock.Object.Courses.Count());
            }
        }

        public class Delete : CourseRepositoryTestFixture
        {
            private Course _course;

            [SetUp]
            public void Setup()
            {
                _course = new CourseBuilder().WithId(1).Build();
                _contextBuilder.WithCourse(_course);
            }

            [Test]
            public void It_should_delete_course_to_the_database()
            {
                var courseRepository = BuildCourseRepository();
                courseRepository.Delete(_course);

                Assert.IsEmpty(_contextBuilder.BuildObject().Courses.ToList());
            }
        }
    }
}
