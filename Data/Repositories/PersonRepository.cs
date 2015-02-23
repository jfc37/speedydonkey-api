using System.Collections.Generic;
using System.Linq;
using Models;

namespace Data.Repositories
{
    public interface IPersonRepository<TPerson> where TPerson : Person
    {
        IEnumerable<TPerson> GetAll();
        TPerson Get(int personId);

        Person Create(Person person);

        TPerson Update(TPerson person);

        void Delete(Person person);
    }

    public abstract class PersonRepository<TPerson> : IPersonRepository<TPerson> where TPerson : Person
    {
        protected readonly ISpeedyDonkeyDbContext Context;

        protected PersonRepository(ISpeedyDonkeyDbContext context)
        {
            Context = context;
        }

        public abstract IEnumerable<TPerson> GetAll();

        public abstract TPerson Get(int personId);

        public Person Create(Person person)
        {
            person.User = Context.Users.Single(x => x.Id == person.User.Id);

            Context.People.Add(person);
            Context.SaveChanges();
            return person;
        }

        public TPerson Update(TPerson person)
        {
            Context.SaveChanges();
            return person;
        }

        public void Delete(Person person)
        {
            Context.People.Remove(person);
            Context.SaveChanges();
        }
    }
}
