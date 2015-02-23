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
    public class ExamRepositoryTestFixture
    {
        private ExamRepositoryBuilder _examRepositoryBuilder;
        private MockDbContextBuilder _contextBuilder;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _examRepositoryBuilder = new ExamRepositoryBuilder();
            _contextBuilder = new MockDbContextBuilder();
        }

        private ExamRepository BuildExamRepository()
        {
            return _examRepositoryBuilder
                .WithContext(_contextBuilder.BuildObject())
                .Build();
        }

        public class Get : ExamRepositoryTestFixture
        {
            private int _examId;

            [SetUp]
            public void Setup()
            {
                _examId = 553;
            }

            [Test]
            public void It_should_return_null_when_no_exam_id_isnt_found()
            {
                _contextBuilder.WithNoExams();

                var examRepository = BuildExamRepository();
                var exam = examRepository.Get(_examId);

                Assert.IsNull(exam);
            }

            [Test]
            public void It_should_return_all_exams()
            {
                var savedExam = new Exam {Id = _examId};
                _contextBuilder.WithCourseWork(savedExam);

                var examRepository = BuildExamRepository();
                var exam = examRepository.Get(_examId);

                Assert.AreSame(savedExam, exam);
            }
        }

        public class GetAll : ExamRepositoryTestFixture
        {
            private int _courseId;

            [SetUp]
            public void Setup()
            {
                _courseId = 5654;
            }

            [Test]
            public void It_should_return_empty_when_no_exams_exists()
            {
                _contextBuilder.WithNoExams();

                var examRepository = BuildExamRepository();
                var results = examRepository.GetAll(_courseId);

                Assert.IsEmpty(results);
            }

            [Test]
            public void It_should_return_all_exams()
            {
                var course = new CourseBuilder()
                    .WithId(_courseId)
                    .Build();

                _contextBuilder
                    .WithCourseWork(new ExamBuilder().WithCourse(course).Build())
                    .WithCourseWork(new ExamBuilder().WithCourse(course).Build())
                    .WithCourseWork(new ExamBuilder().WithCourse(course).Build());

                var examRepository = BuildExamRepository();
                var results = examRepository.GetAll(_courseId);

                Assert.AreEqual(_contextBuilder.BuildObject().CourseWork.Count(), results.Count());
            }
        }

        public class Create : ExamRepositoryTestFixture
        {
            private Exam _exam;

            [SetUp]
            public void Setup()
            {
                var course = new Course {Id = 6555};
                _exam = new ExamBuilder()
                    .WithCourse(course)
                    .Build();
                _contextBuilder.WithNoExams()
                    .WithCourse(course);
            }

            [Test]
            public void It_should_add_exam_to_the_database()
            {
                var examRepository = BuildExamRepository();
                examRepository.Create(_exam);

                Assert.IsNotEmpty(_contextBuilder.BuildObject().CourseWork.ToList());
            }
        }

        public class Update : ExamRepositoryTestFixture
        {
            [Test]
            public void It_should_not_add_new_exam_to_database_on_update()
            {
                Exam exam = new ExamBuilder()
                    .WithId(1)
                    .Build();
                _contextBuilder.WithCourseWork(exam);

                var repository = BuildExamRepository();
                repository.Update(exam);

                Assert.AreEqual(1, _contextBuilder.Mock.Object.CourseWork.Count());
            }
        }
    }
}
