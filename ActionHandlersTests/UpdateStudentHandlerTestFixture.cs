using ActionHandlers;
using Actions;
using Data.Repositories;
using Data.Tests.Builders.MockBuilders;
using Models;
using Models.Tests.Builders;
using NUnit.Framework;

namespace ActionHandlersTests
{
    [TestFixture]
    public class UpdateStudentHandlerTestFixture
    {
        private UpdateStudentHandlerBuilder _updateStudentHandlerBuilder;
        private MockPersonRepositoryBuilder<Student> _studentRepositoryBuilder;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _updateStudentHandlerBuilder = new UpdateStudentHandlerBuilder();
            _studentRepositoryBuilder = new MockPersonRepositoryBuilder<Student>()
                .WithSuccessfulUpdate();
        }

        private UpdateStudentHandler BuildUpdateStudentHandler()
        {
            return _updateStudentHandlerBuilder
                .WithStudentRepository(_studentRepositoryBuilder.BuildObject())
                .Build();
        }

        public class Handle : UpdateStudentHandlerTestFixture
        {
            private UpdateStudent _createStudent;

            [SetUp]
            public void Setup()
            {
                _createStudent = new UpdateStudent(new PersonBuilder().BuildStudent());
                _studentRepositoryBuilder.WithPerson(_createStudent.ActionAgainst);
            }

            [Test]
            public void It_should_call_update_on_student_repository()
            {
                var createStudentHanlder = BuildUpdateStudentHandler();
                createStudentHanlder.Handle(_createStudent);

                _studentRepositoryBuilder.Mock.Verify(x => x.Update(_createStudent.ActionAgainst));
            }
        }
    }

    public class UpdateStudentHandlerBuilder
    {
        private IPersonRepository<Student> _studentRepository;

        public UpdateStudentHandler Build()
        {
            return new UpdateStudentHandler(_studentRepository);
        }

        public UpdateStudentHandlerBuilder WithStudentRepository(IPersonRepository<Student> studentRepository)
        {
            _studentRepository = studentRepository;
            return this;
        }
    }
}
