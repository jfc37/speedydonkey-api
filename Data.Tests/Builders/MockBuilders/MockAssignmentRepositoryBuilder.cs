using System.Collections.Generic;
using Common.Tests.Builders.MockBuilders;
using Data.Repositories;
using Models;
using Moq;

namespace Data.Tests.Builders.MockBuilders
{
    public class MockAssignmentRepositoryBuilder : MockBuilder<IAssignmentRepository>
    {
        private readonly IList<Assignment> _assignments;

        public MockAssignmentRepositoryBuilder()
        {
            _assignments = new List<Assignment>();

            Mock.Setup(x => x.Get(It.IsAny<int>()))
                .Returns((Assignment) null);
        }

        protected override void BeforeBuild()
        {
            Mock.Setup(x => x.GetAll(It.IsAny<int>()))
                .Returns(_assignments);
        }

        public MockAssignmentRepositoryBuilder WithSomeValidAssignment()
        {
            _assignments.Add(new Assignment());
            return this;
        }

        public MockAssignmentRepositoryBuilder WithNoAssignments()
        {
            _assignments.Clear();
            return this;
        }

        public MockAssignmentRepositoryBuilder WithAssignment(Assignment assignment)
        {
            _assignments.Add(assignment);

            Mock.Setup(x => x.Get(assignment.Id))
                .Returns(assignment);

            return this;
        }

        public MockAssignmentRepositoryBuilder WithSuccessfulCreation()
        {
            Mock.Setup(x => x.Create(It.IsAny<Assignment>()))
                .Returns<Assignment>(x => new Assignment { Id = 543 });

            return this;
        }

        public MockAssignmentRepositoryBuilder WithSuccessfulUpdate()
        {
            Mock.Setup(x => x.Update(It.IsAny<Assignment>()))
                .Returns<Assignment>(x => x);

            return this;
        }
    }
}