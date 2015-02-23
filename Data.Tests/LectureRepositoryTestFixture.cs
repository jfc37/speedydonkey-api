using System.Linq;
using Data.Repositories;
using Data.Tests.Builders.MockBuilders;
using Models;
using Models.Tests.Builders;
using NUnit.Framework;

namespace Data.Tests
{
    [TestFixture]
    public class LectureRepositoryTestFixture
    {
        private LectureRepositoryBuilder _lectureRepositoryBuilder;
        private MockDbContextBuilder _contextBuilder;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _lectureRepositoryBuilder = new LectureRepositoryBuilder();
            _contextBuilder = new MockDbContextBuilder();
        }

        private LectureRepository BuildLectureRepository()
        {
            return _lectureRepositoryBuilder
                .WithContext(_contextBuilder.BuildObject())
                .Build();
        }

        public class Get : LectureRepositoryTestFixture
        {
            private int _lectureId;

            [SetUp]
            public void Setup()
            {
                _lectureId = 553;
            }

            [Test]
            public void It_should_return_null_when_no_lecture_id_isnt_found()
            {
                _contextBuilder.WithNoLectures();

                var lectureRepository = BuildLectureRepository();
                var lecture = lectureRepository.Get(_lectureId);

                Assert.IsNull(lecture);
            }

            [Test]
            public void It_should_return_all_lectures()
            {
                var savedLecture = new Lecture {Id = _lectureId};
                _contextBuilder.WithLecture(savedLecture);

                var lectureRepository = BuildLectureRepository();
                var lecture = lectureRepository.Get(_lectureId);

                Assert.AreSame(savedLecture, lecture);
            }
        }

        public class GetAll : LectureRepositoryTestFixture
        {
            private int _courseId;

            [SetUp]
            public void Setup()
            {
                _courseId = 5654;
            }

            [Test]
            public void It_should_return_empty_when_no_lectures_exists()
            {
                _contextBuilder.WithNoLectures();

                var lectureRepository = BuildLectureRepository();
                var results = lectureRepository.GetAll(_courseId);

                Assert.IsEmpty(results);
            }

            [Test]
            public void It_should_return_all_lectures()
            {
                var course = new CourseBuilder()
                    .WithId(_courseId)
                    .Build();

                _contextBuilder
                    .WithLecture(new LectureBuilder().WithCourse(course).Build())
                    .WithLecture(new LectureBuilder().WithCourse(course).Build())
                    .WithLecture(new LectureBuilder().WithCourse(course).Build());

                var lectureRepository = BuildLectureRepository();
                var results = lectureRepository.GetAll(_courseId);

                Assert.AreEqual(_contextBuilder.BuildObject().Lectures.Count(), results.Count());
            }
        }

        public class Create : LectureRepositoryTestFixture
        {
            private Lecture _lecture;

            [SetUp]
            public void Setup()
            {
                var course = new Course {Id = 6555};
                _lecture = new LectureBuilder()
                    .WithCourse(course)
                    .Build();
                _contextBuilder.WithNoLectures()
                    .WithCourse(course);
            }

            [Test]
            public void It_should_add_lecture_to_the_database()
            {
                var lectureRepository = BuildLectureRepository();
                lectureRepository.Create(_lecture);

                Assert.IsNotEmpty(_contextBuilder.BuildObject().Lectures.ToList());
            }
        }

        public class Update : LectureRepositoryTestFixture
        {
            [Test]
            public void It_should_not_add_new_lecture_to_database_on_update()
            {
                Lecture lecture = new LectureBuilder()
                    .WithId(1)
                    .Build();
                _contextBuilder.WithLecture(lecture);

                var repository = BuildLectureRepository();
                repository.Update(lecture);

                Assert.AreEqual(1, _contextBuilder.Mock.Object.Lectures.Count());
            }
        }

        public class Delete : LectureRepositoryTestFixture
        {
            private Lecture _lecture;

            [SetUp]
            public void Setup()
            {
                _lecture = new LectureBuilder().WithId(1).Build();
                _contextBuilder.WithLecture(_lecture);
            }

            [Test]
            public void It_should_delete_lecture_to_the_database()
            {
                var lectureRepository = BuildLectureRepository();
                lectureRepository.Delete(_lecture);

                Assert.IsEmpty(_contextBuilder.BuildObject().Lectures.ToList());
            }
        }
    }

    public class LectureRepositoryBuilder
    {
        private ISpeedyDonkeyDbContext _context;

        public LectureRepositoryBuilder WithContext(ISpeedyDonkeyDbContext context)
        {
            _context = context;
            return this;
        }

        public LectureRepository Build()
        {
            return new LectureRepository(_context);
        }
    }
}
