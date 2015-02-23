using System.Collections.Generic;
using Common.Tests.Builders.MockBuilders;
using Data.Repositories;
using Models;
using Moq;

namespace Data.Tests.Builders.MockBuilders
{
    public class MockExamRepositoryBuilder : MockBuilder<IExamRepository>
    {
        private readonly IList<Exam> _exams;

        public MockExamRepositoryBuilder()
        {
            _exams = new List<Exam>();

            Mock.Setup(x => x.Get(It.IsAny<int>()))
                .Returns((Exam) null);
        }

        protected override void BeforeBuild()
        {
            Mock.Setup(x => x.GetAll(It.IsAny<int>()))
                .Returns(_exams);
        }

        public MockExamRepositoryBuilder WithSomeValidExam()
        {
            _exams.Add(new Exam());
            return this;
        }

        public MockExamRepositoryBuilder WithNoExams()
        {
            _exams.Clear();
            return this;
        }

        public MockExamRepositoryBuilder WithExam(Exam exam)
        {
            _exams.Add(exam);

            Mock.Setup(x => x.Get(exam.Id))
                .Returns(exam);

            return this;
        }

        public MockExamRepositoryBuilder WithSuccessfulCreation()
        {
            Mock.Setup(x => x.Create(It.IsAny<Exam>()))
                .Returns<Exam>(x => new Exam { Id = 543 });

            return this;
        }

        public MockExamRepositoryBuilder WithSuccessfulUpdate()
        {
            Mock.Setup(x => x.Update(It.IsAny<Exam>()))
                .Returns<Exam>(x => x);

            return this;
        }
    }
}