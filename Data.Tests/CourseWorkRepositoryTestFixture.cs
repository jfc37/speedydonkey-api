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
    public class CourseWorkRepositoryTestFixture
    {
        private CourseWorkRepositoryBuilder _courseWorkRepositoryBuilder;
        private MockDbContextBuilder _contextBuilder;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _courseWorkRepositoryBuilder = new CourseWorkRepositoryBuilder();
            _contextBuilder = new MockDbContextBuilder();
        }

        private CourseWorkRepository BuildCourseWorkRepository()
        {
            return _courseWorkRepositoryBuilder
                .WithContext(_contextBuilder.BuildObject())
                .Build();
        }

        public class Get : CourseWorkRepositoryTestFixture
        {
            private int _courseWorkId;

            [SetUp]
            public void Setup()
            {
                _courseWorkId = 553;
            }

            [Test]
            public void It_should_return_null_when_no_course_work_id_isnt_found()
            {
                _contextBuilder
                    .WithNoExams()
                    .WithNoAssignments();

                var courseWorkRepository = BuildCourseWorkRepository();
                var courseWork = courseWorkRepository.Get(_courseWorkId);

                Assert.IsNull(courseWork);
            }

            [Test]
            public void It_should_return_all_course_work()
            {
                var savedCourseWork = new Assignment {Id = _courseWorkId};
                _contextBuilder.WithCourseWork(savedCourseWork);

                var courseWorkRepository = BuildCourseWorkRepository();
                var courseWork = courseWorkRepository.Get(_courseWorkId);

                Assert.AreSame(savedCourseWork, courseWork);
            }
        }

        public class GetAll : CourseWorkRepositoryTestFixture
        {
            private int _courseId;

            [SetUp]
            public void Setup()
            {
                _courseId = 5654;
            }

            [Test]
            public void It_should_return_empty_when_no_course_work_exists()
            {
                _contextBuilder
                    .WithNoExams()
                    .WithNoAssignments();

                var courseWorkRepository = BuildCourseWorkRepository();
                var results = courseWorkRepository.GetAll();

                Assert.IsEmpty(results);
            }

            [Test]
            public void It_should_return_all_course_work()
            {
                var course = new CourseBuilder()
                    .WithId(_courseId)
                    .Build();

                _contextBuilder
                    .WithCourseWork(new AssignmentBuilder().WithCourse(course).Build())
                    .WithCourseWork(new AssignmentBuilder().WithCourse(course).Build())
                    .WithCourseWork(new AssignmentBuilder().WithCourse(course).Build());

                var courseWorkRepository = BuildCourseWorkRepository();
                var results = courseWorkRepository.GetAll();

                Assert.AreEqual(_contextBuilder.BuildObject().CourseWork.Count(), results.Count());
            }
        }

        public class Delete : CourseWorkRepositoryTestFixture
        {
            private CourseWork _courseWork;

            [SetUp]
            public void Setup()
            {
                _courseWork = new AssignmentBuilder().WithId(1).Build();
                _contextBuilder.WithCourseWork(_courseWork);
            }

            [Test]
            public void It_should_delete_courseWork_to_the_database()
            {
                var courseWorkRepository = BuildCourseWorkRepository();
                courseWorkRepository.Delete(_courseWork);

                Assert.IsEmpty(_contextBuilder.BuildObject().CourseWork.ToList());
            }
        }
    }
}
