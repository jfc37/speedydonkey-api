using System.Collections.Generic;
using System.Linq;
using Common.Tests.Builders.MockBuilders;
using Data.Repositories;
using Models;
using Moq;

namespace Data.Tests.Builders.MockBuilders
{
    public class MockPersonRepositoryBuilder<TPerson> : MockBuilder<IPersonRepository<TPerson>> where TPerson : Person, new()
    {
        private IList<Person> _persons;

        public MockPersonRepositoryBuilder()
        {
            _persons = new List<Person>();
        }

        protected override void BeforeBuild()
        {
            Mock.Setup(x => x.GetAll())
                .Returns(_persons.OfType<TPerson>());
        }

        public MockPersonRepositoryBuilder<TPerson> WithSomeValidPerson()
        {
            _persons.Add(new TPerson());
            return this;
        }

        public MockPersonRepositoryBuilder<TPerson> WithNoPeople()
        {
            _persons = new List<Person>();

            Mock.Setup(x => x.Get(It.IsAny<int>()))
                .Returns((TPerson)null);
            return this;
        }

        public MockPersonRepositoryBuilder<TPerson> WithPerson(TPerson person)
        {
            _persons.Add(person);

            Mock.Setup(x => x.Get(person.Id))
                .Returns(person);

            return this;
        }

        public MockPersonRepositoryBuilder<TPerson> WithSuccessfulCreation()
        {
            Mock.Setup(x => x.Create(It.IsAny<Person>()))
                .Returns<Person>(x => new Student { Id = 543 });

            return this;
        }

        public MockPersonRepositoryBuilder<TPerson> WithSuccessfulUpdate()
        {
            Mock.Setup(x => x.Update(It.IsAny<TPerson>()))
                .Returns<TPerson>(x => x);
            return this;
        }

        public MockPersonRepositoryBuilder<TPerson> WithSuccessfulDelete()
        {
            Mock.Setup(x => x.Delete(It.IsAny<Person>()));
            return this;
        }
    }
}