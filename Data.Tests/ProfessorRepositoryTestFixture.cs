using System.Linq;
using Data.Repositories;
using Data.Tests.Builders.MockBuilders;
using Models;
using Models.Tests.Builders;
using NUnit.Framework;

namespace Data.Tests
{
    [TestFixture]
    public class ProfessorRepositoryTestFixture
    {
        private ProfessorRepositoryBuilder _professorRepositoryBuilder;
        private MockDbContextBuilder _contextBuilder;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _professorRepositoryBuilder = new ProfessorRepositoryBuilder();
            _contextBuilder = new MockDbContextBuilder();
        }

        private ProfessorRepository BuildPersonRepository()
        {
            return _professorRepositoryBuilder
                .WithContext(_contextBuilder.BuildObject())
                .Build();
        }

        public class Get : ProfessorRepositoryTestFixture
        {
            private int _personId;

            [SetUp]
            public void Setup()
            {
                _personId = 553;
            }

            [Test]
            public void It_should_return_null_when_no_person_id_isnt_found()
            {
                _contextBuilder.WithNoPeople();

                var personRepository = BuildPersonRepository();
                var person = personRepository.Get(_personId);

                Assert.IsNull(person);
            }

            [Test]
            public void It_should_return_all_persons()
            {
                var savedPerson = new Professor {Id = _personId};
                _contextBuilder.WithProfessor(savedPerson);

                var personRepository = BuildPersonRepository();
                var person = personRepository.Get(_personId);

                Assert.AreSame(savedPerson, person);
            }
        }

        public class GetAll : ProfessorRepositoryTestFixture
        {
            [Test]
            public void It_should_return_empty_when_no_persons_exists()
            {
                _contextBuilder.WithNoPeople();

                var personRepository = BuildPersonRepository();
                var results = personRepository.GetAll();

                Assert.IsEmpty(results);
            }

            [Test]
            public void It_should_return_all_persons()
            {
                _contextBuilder.WithProfessor()
                    .WithProfessor()
                    .WithProfessor()
                    .WithProfessor();

                var personRepository = BuildPersonRepository();
                var results = personRepository.GetAll();

                Assert.AreEqual(_contextBuilder.BuildObject().People.Count(), results.Count());
            }
        }

        public class Create : ProfessorRepositoryTestFixture
        {
            private Person _person;

            [SetUp]
            public void Setup()
            {
                var user = new UserBuilder()
                    .WithId(44)
                    .Build();
                _person = new PersonBuilder()
                    .WithUser(user)
                    .BuildStudent();
                _contextBuilder
                    .WithNoPeople()
                    .WithUser(user);
            }

            [Test]
            public void It_should_add_person_to_the_database()
            {
                var personRepository = BuildPersonRepository();
                personRepository.Create(_person);

                Assert.IsNotEmpty(_contextBuilder.BuildObject().People.ToList());
            }
        }
    }

    public class ProfessorRepositoryBuilder
    {
        private ISpeedyDonkeyDbContext _context;

        public ProfessorRepositoryBuilder WithContext(ISpeedyDonkeyDbContext context)
        {
            _context = context;
            return this;
        }

        public ProfessorRepository Build()
        {
            return new ProfessorRepository(_context);
        }
    }
}
