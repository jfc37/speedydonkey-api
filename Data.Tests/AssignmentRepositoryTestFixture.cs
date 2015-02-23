using System.Linq;
using Data.Repositories;
using Data.Tests.Builders.MockBuilders;
using Models;
using Models.Tests.Builders;
using NUnit.Framework;

namespace Data.Tests
{
    [TestFixture]
    public class AssignmentRepositoryTestFixture
    {
        private AssignmentRepositoryBuilder _assignmentRepositoryBuilder;
        private MockDbContextBuilder _contextBuilder;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _assignmentRepositoryBuilder = new AssignmentRepositoryBuilder();
            _contextBuilder = new MockDbContextBuilder();
        }

        private AssignmentRepository BuildAssignmentRepository()
        {
            return _assignmentRepositoryBuilder
                .WithContext(_contextBuilder.BuildObject())
                .Build();
        }

        public class Get : AssignmentRepositoryTestFixture
        {
            private int _assignmentId;

            [SetUp]
            public void Setup()
            {
                _assignmentId = 553;
            }

            [Test]
            public void It_should_return_null_when_no_assignment_id_isnt_found()
            {
                _contextBuilder.WithNoAssignments();

                var assignmentRepository = BuildAssignmentRepository();
                var assignment = assignmentRepository.Get(_assignmentId);

                Assert.IsNull(assignment);
            }

            [Test]
            public void It_should_return_all_assignments()
            {
                var savedAssignment = new Assignment {Id = _assignmentId};
                _contextBuilder.WithCourseWork(savedAssignment);

                var assignmentRepository = BuildAssignmentRepository();
                var assignment = assignmentRepository.Get(_assignmentId);

                Assert.AreSame(savedAssignment, assignment);
            }
        }

        public class GetAll : AssignmentRepositoryTestFixture
        {
            private int _courseId;

            [SetUp]
            public void Setup()
            {
                _courseId = 5654;
            }

            [Test]
            public void It_should_return_empty_when_no_assignments_exists()
            {
                _contextBuilder.WithNoAssignments();

                var assignmentRepository = BuildAssignmentRepository();
                var results = assignmentRepository.GetAll(_courseId);

                Assert.IsEmpty(results);
            }

            [Test]
            public void It_should_return_all_assignments()
            {
                var course = new CourseBuilder()
                    .WithId(_courseId)
                    .Build();

                _contextBuilder
                    .WithCourseWork(new AssignmentBuilder().WithCourse(course).Build())
                    .WithCourseWork(new AssignmentBuilder().WithCourse(course).Build())
                    .WithCourseWork(new AssignmentBuilder().WithCourse(course).Build());

                var assignmentRepository = BuildAssignmentRepository();
                var results = assignmentRepository.GetAll(_courseId);

                Assert.AreEqual(_contextBuilder.BuildObject().CourseWork.Count(), results.Count());
            }
        }

        public class Create : AssignmentRepositoryTestFixture
        {
            private Assignment _assignment;

            [SetUp]
            public void Setup()
            {
                var course = new Course {Id = 6555};
                _assignment = new AssignmentBuilder()
                    .WithCourse(course)
                    .Build();
                _contextBuilder.WithNoAssignments()
                    .WithCourse(course);
            }

            [Test]
            public void It_should_add_assignment_to_the_database()
            {
                var assignmentRepository = BuildAssignmentRepository();
                assignmentRepository.Create(_assignment);

                Assert.IsNotEmpty(_contextBuilder.BuildObject().CourseWork.ToList());
            }
        }

        public class Update : AssignmentRepositoryTestFixture
        {
            [Test]
            public void It_should_not_add_new_assignment_to_database_on_update()
            {
                Assignment assignment = new AssignmentBuilder()
                    .WithId(1)
                    .Build();
                _contextBuilder.WithCourseWork(assignment);

                var repository = BuildAssignmentRepository();
                repository.Update(assignment);

                Assert.AreEqual(1, _contextBuilder.Mock.Object.CourseWork.Count());
            }
        }
    }

    public class AssignmentRepositoryBuilder
    {
        private ISpeedyDonkeyDbContext _context;

        public AssignmentRepositoryBuilder WithContext(ISpeedyDonkeyDbContext context)
        {
            _context = context;
            return this;
        }

        public AssignmentRepository Build()
        {
            return new AssignmentRepository(_context);
        }
    }
}
