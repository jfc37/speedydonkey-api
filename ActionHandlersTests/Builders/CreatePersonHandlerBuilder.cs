using ActionHandlers;
using Data.Repositories;
using Models;

namespace ActionHandlersTests.Builders
{
    public class CreatePersonHandlerBuilder
    {
        private IPersonRepository<Professor> _personRepository;

        public CreateProfessorHandler Build()
        {
            return new CreateProfessorHandler(_personRepository);
        }

        public CreatePersonHandlerBuilder WithPersonRepository(IPersonRepository<Professor> personRepository)
        {
            _personRepository = personRepository;
            return this;
        }
    }
}