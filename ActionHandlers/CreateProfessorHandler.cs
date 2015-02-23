using Actions;
using Data.Repositories;
using Models;

namespace ActionHandlers
{
    public class CreateProfessorHandler : IActionHandler<CreatePerson, Person>
    {
        private readonly IPersonRepository<Professor> _personRepository;

        public CreateProfessorHandler(IPersonRepository<Professor> personRepository)
        {
            _personRepository = personRepository;
        }

        public Person Handle(CreatePerson action)
        {
            return _personRepository.Create(action.ActionAgainst);
        }
    }
}
