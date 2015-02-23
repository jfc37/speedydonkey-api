using ActionHandlers;
using Data.Repositories;
using Models;

namespace ActionHandlersTests.Builders
{
    public class DeletePersonHandlerBuilder
    {
        private IPersonRepository<Student> _personRepository;

        public DeletePersonHandlerBuilder WithPersonRepository(IPersonRepository<Student> personRepository)
        {
            _personRepository = personRepository;
            return this;
        }

        public DeletePersonHandler Build()
        {
            return new DeletePersonHandler(_personRepository);
        }
    }
}