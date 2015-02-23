using ActionHandlers;
using Actions;
using Data.Repositories;
using Data.Tests.Builders.MockBuilders;
using Models.Tests.Builders;
using NUnit.Framework;

namespace ActionHandlersTests
{
    [TestFixture]
    public class UpdateCourseWorkGradeHandlerTestFixture
    {
        private UpdateCourseWorkGradeHandlerBuilder _updateCourseWorkGradeHandlerBuilder;
        private MockCourseWorkGradeRepositoryBuilder _courseWorkGradeRepositoryBuilder;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _updateCourseWorkGradeHandlerBuilder = new UpdateCourseWorkGradeHandlerBuilder();
            _courseWorkGradeRepositoryBuilder = new MockCourseWorkGradeRepositoryBuilder()
                .WithSuccessfulUpdate();
        }

        private UpdateCourseWorkGradeHandler BuildUpdateCourseWorkGradeHandler()
        {
            return _updateCourseWorkGradeHandlerBuilder
                .WithCourseWorkGradeRepository(_courseWorkGradeRepositoryBuilder.BuildObject())
                .Build();
        }

        public class Handle : UpdateCourseWorkGradeHandlerTestFixture
        {
            private UpdateCourseWorkGrade _updateCourseWorkGrade;

            [SetUp]
            public void Setup()
            {
                _updateCourseWorkGrade = new UpdateCourseWorkGrade(new CourseWorkGradeBuilder().Build());
                _courseWorkGradeRepositoryBuilder.WithCourseWorkGrade(_updateCourseWorkGrade.ActionAgainst);
            }

            [Test]
            public void It_should_call_update_on_courseWorkGrade_repository()
            {
                var updateCourseWorkGradeHanlder = BuildUpdateCourseWorkGradeHandler();
                updateCourseWorkGradeHanlder.Handle(_updateCourseWorkGrade);

                _courseWorkGradeRepositoryBuilder.Mock.Verify(x => x.Update(_updateCourseWorkGrade.ActionAgainst));
            }
        }
    }

    public class UpdateCourseWorkGradeHandlerBuilder
    {
        private ICourseWorkGradeRepository _courseWorkGradeRepository;

        public UpdateCourseWorkGradeHandler Build()
        {
            return new UpdateCourseWorkGradeHandler(_courseWorkGradeRepository);
        }

        public UpdateCourseWorkGradeHandlerBuilder WithCourseWorkGradeRepository(ICourseWorkGradeRepository courseWorkGradeRepository)
        {
            _courseWorkGradeRepository = courseWorkGradeRepository;
            return this;
        }
    }
}
