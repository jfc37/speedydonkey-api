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
    public class CourseWorkGradeRepositoryTestFixture
    {
        private CourseWorkGradeRepositoryBuilder _courseWorkGradeRepositoryBuilder;
        private MockDbContextBuilder _contextBuilder;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _courseWorkGradeRepositoryBuilder = new CourseWorkGradeRepositoryBuilder();
            _contextBuilder = new MockDbContextBuilder();
        }

        private CourseWorkGradeRepository BuildCourseWorkGradeRepository()
        {
            return _courseWorkGradeRepositoryBuilder
                .WithContext(_contextBuilder.BuildObject())
                .Build();
        }

        public class Get : CourseWorkGradeRepositoryTestFixture
        {
            private int _courseId;
            private int _studentId;
            private int _courseWorkId;

            [SetUp]
            public void Setup()
            {
                _courseId = 1;
                _studentId = 2;
                _courseWorkId = 3;
            }

            [Test]
            public void It_should_return_null_when_no_course_work_grade_id_isnt_found()
            {
                _contextBuilder.WithNoCourseWorkGrades();

                var courseWorkGradeRepository = BuildCourseWorkGradeRepository();
                var courseWorkGrade = courseWorkGradeRepository.Get(_studentId, _courseId, _courseWorkId);

                Assert.IsNull(courseWorkGrade);
            }

            [Test]
            public void It_should_return_all_courseWorkGrades()
            {
                var savedCourseWorkGrade = new CourseWorkGradeBuilder()
                    .WithStudentId(_studentId)
                    .WithCourseId(_courseId)
                    .WithCourseWorkId(_courseWorkId)
                    .Build();
                _contextBuilder.WithCourseWorkGrade(savedCourseWorkGrade);

                var courseWorkGradeRepository = BuildCourseWorkGradeRepository();
                var courseWorkGrade = courseWorkGradeRepository.Get(_studentId, _courseId, _courseWorkId);

                Assert.AreSame(savedCourseWorkGrade, courseWorkGrade);
            }
        }

        public class GetAll : CourseWorkGradeRepositoryTestFixture
        {
            private int _courseId;
            private int _studentId;

            [SetUp]
            public void Setup()
            {
                _courseId = 5654;
                _studentId = 454;
            }

            [Test]
            public void It_should_return_empty_when_no_courseWorkGrades_exists()
            {
                _contextBuilder.WithNoCourseWorkGrades();

                var courseWorkGradeRepository = BuildCourseWorkGradeRepository();
                var results = courseWorkGradeRepository.GetAll(_studentId, _courseId);

                Assert.IsEmpty(results);
            }

            [Test]
            public void It_should_return_all_courseWorkGrades()
            {
                _contextBuilder
                    .WithNoCourseWorkGrades()
                    .WithCourseWorkGrade(
                        new CourseWorkGradeBuilder().WithStudentId(_studentId).WithCourseId(_courseId).WithCourseWorkId(1).Build())
                    .WithCourseWorkGrade(
                        new CourseWorkGradeBuilder().WithStudentId(_studentId).WithCourseId(_courseId).WithCourseWorkId(2).Build())
                    .WithCourseWorkGrade(
                        new CourseWorkGradeBuilder().WithStudentId(_studentId).WithCourseId(_courseId).WithCourseWorkId(3).Build());

                var courseWorkGradeRepository = BuildCourseWorkGradeRepository();
                var results = courseWorkGradeRepository.GetAll(_studentId, _courseId);

                Assert.AreEqual(_contextBuilder.BuildObject().CourseWorkGrades.Count(), results.Count());
            }
        }

        public class Create : CourseWorkGradeRepositoryTestFixture
        {
            private CourseWorkGrade _courseWorkGrade;
            private int _courseGradeId;

            [SetUp]
            public void Setup()
            {
                _courseGradeId = 1;
                _courseWorkGrade = new CourseWorkGradeBuilder()
                    .WithCourseGradeId(_courseGradeId)
                    .WithStudentId(1)
                    .WithCourseId(1)
                    .WithCourseWorkId(1)
                    .Build();
                CourseGrade courseGrade = new CourseGrade{Id = _courseGradeId, Student = new Student{Id = 1}, Course = new Course{Id = 1}};
                _contextBuilder
                    .WithNoCourseGrades()
                    .WithNoCourses()
                    .WithCourseGrade(courseGrade)
                    .WithCourse(new Course{Id = 1})
                    .WithCourseWork(new Assignment{Id = 1});
            }

            [Test]
            public void It_should_add_courseWorkGrade_to_the_database()
            {
                var courseWorkGradeRepository = BuildCourseWorkGradeRepository();
                courseWorkGradeRepository.Create(_courseWorkGrade);

                Assert.IsNotEmpty(_contextBuilder.BuildObject().CourseWorkGrades.ToList());
            }
        }

        public class Update : CourseWorkGradeRepositoryTestFixture
        {
            [Test]
            public void It_should_not_add_new_course_work_grade_to_database_on_update()
            {
                CourseWorkGrade courseWorkGrade = new CourseWorkGradeBuilder()
                    .WithId(1)
                    .Build();
                _contextBuilder.WithCourseWorkGrade(courseWorkGrade);

                var repository = BuildCourseWorkGradeRepository();
                repository.Update(courseWorkGrade);

                Assert.AreEqual(1, _contextBuilder.Mock.Object.CourseWorkGrades.Count());
            }
        }

        public class Delete : CourseWorkGradeRepositoryTestFixture
        {
            private CourseWorkGrade _courseWorkGrade;

            [SetUp]
            public void Setup()
            {
                _courseWorkGrade = new CourseWorkGradeBuilder().WithId(1).Build();
                _contextBuilder.WithCourseWorkGrade(_courseWorkGrade);
            }

            [Test]
            public void It_should_delete_courseWorkGrade_to_the_database()
            {
                var courseWorkGradeRepository = BuildCourseWorkGradeRepository();
                courseWorkGradeRepository.Delete(_courseWorkGrade);

                Assert.IsEmpty(_contextBuilder.BuildObject().CourseWorkGrades.ToList());
            }
        }
    }
}
