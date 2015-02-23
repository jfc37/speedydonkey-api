using System.Linq;
using Data.Repositories;
using Data.Tests.Builders.MockBuilders;
using Models;
using Models.Tests.Builders;
using NUnit.Framework;

namespace Data.Tests
{
    [TestFixture]
    public class CourseGradeRepositoryTestFixture
    {
        private CourseGradeRepositoryBuilder _courseGradeRepositoryBuilder;
        private MockDbContextBuilder _contextBuilder;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _courseGradeRepositoryBuilder = new CourseGradeRepositoryBuilder();
            _contextBuilder = new MockDbContextBuilder();
        }

        private CourseGradeRepository BuildCourseGradeRepository()
        {
            return _courseGradeRepositoryBuilder
                .WithContext(_contextBuilder.BuildObject())
                .Build();
        }

        public class Get : CourseGradeRepositoryTestFixture
        {
            private int _courseId;
            private int _studentId;

            [SetUp]
            public void Setup()
            {
                _courseId = 553;
                _studentId = 643;
            }

            [Test]
            public void It_should_return_null_when_no_courseGrade_id_isnt_found()
            {
                _contextBuilder.WithNoCourseGrades();

                var courseGradeRepository = BuildCourseGradeRepository();
                var courseGrade = courseGradeRepository.Get(_studentId, _courseId);

                Assert.IsNull(courseGrade);
            }

            [Test]
            public void It_should_return_all_courseGrades()
            {
                var savedCourseGrade = new CourseGradeBuilder()
                    .WithStudent(new Student{Id = _studentId})
                    .WithCourse(new Course{Id = _courseId})
                    .Build();
                _contextBuilder.WithCourseGrade(savedCourseGrade);

                var courseGradeRepository = BuildCourseGradeRepository();
                var courseGrade = courseGradeRepository.Get(_studentId, _courseId);

                Assert.AreSame(savedCourseGrade, courseGrade);
            }
        }

        public class GetAll : CourseGradeRepositoryTestFixture
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
            public void It_should_return_empty_when_no_courseGrades_exists()
            {
                _contextBuilder.WithNoCourseGrades();

                var courseGradeRepository = BuildCourseGradeRepository();
                var results = courseGradeRepository.GetAll(_studentId);

                Assert.IsEmpty(results);
            }

            [Test]
            public void It_should_return_all_courseGrades()
            {
                var student = new PersonBuilder()
                    .WithId(_studentId)
                    .BuildStudent();

                _contextBuilder
                    .WithCourseGrade(new CourseGradeBuilder().WithStudent(student).Build())
                    .WithCourseGrade(new CourseGradeBuilder().WithStudent(student).Build())
                    .WithCourseGrade(new CourseGradeBuilder().WithStudent(student).Build());

                var courseGradeRepository = BuildCourseGradeRepository();
                var results = courseGradeRepository.GetAll(_courseId);

                Assert.AreEqual(_contextBuilder.BuildObject().CourseWork.Count(), results.Count());
            }
        }
    }

    public class CourseGradeRepositoryBuilder
    {
        private ISpeedyDonkeyDbContext _context;

        public CourseGradeRepositoryBuilder WithContext(ISpeedyDonkeyDbContext context)
        {
            _context = context;
            return this;
        }

        public CourseGradeRepository Build()
        {
            return new CourseGradeRepository(_context);
        }
    }
}
