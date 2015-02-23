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
    public class StudentRepositoryTestFixture
    {
        private StudentRepositoryBuilder _studentRepositoryBuilder;
        private MockDbContextBuilder _contextBuilder;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _studentRepositoryBuilder = new StudentRepositoryBuilder();
            _contextBuilder = new MockDbContextBuilder();
        }

        private StudentRepository BuildPersonRepository()
        {
            return _studentRepositoryBuilder
                .WithContext(_contextBuilder.BuildObject())
                .Build();
        }

        public class Get : StudentRepositoryTestFixture
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
                var savedPerson = new Student {Id = _personId};
                _contextBuilder.WithStudent(savedPerson);

                var personRepository = BuildPersonRepository();
                var person = personRepository.Get(_personId);

                Assert.AreSame(savedPerson, person);
            }
        }

        public class GetAll : StudentRepositoryTestFixture
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
            public void It_should_return_all_students()
            {
                _contextBuilder.WithStudent()
                    .WithStudent()
                    .WithStudent()
                    .WithStudent();

                var personRepository = BuildPersonRepository();
                var results = personRepository.GetAll();

                Assert.AreEqual(_contextBuilder.BuildObject().People.Count(), results.Count());
            }
        }

        public class Create : StudentRepositoryTestFixture
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

        public class Update : StudentRepositoryTestFixture
        {
            [Test]
            public void It_should_not_add_new_person_to_database_on_update()
            {
                Student student = new PersonBuilder()
                    .WithId(1)
                    .BuildStudent();
                _contextBuilder.WithStudent(student);

                var repository = BuildPersonRepository();
                repository.Update(student);

                Assert.AreEqual(1, _contextBuilder.Mock.Object.People.Count());
            }
        }

        public class Delete : StudentRepositoryTestFixture
        {
            [Test]
            public void It_should_delete_person_from_database()
            {
                Student student = new PersonBuilder()
                    .WithId(1)
                    .BuildStudent();
                _contextBuilder.WithStudent(student);

                var repository = BuildPersonRepository();
                repository.Delete(student);

                Assert.IsEmpty(_contextBuilder.Mock.Object.People);
            }
        }
    }
}
