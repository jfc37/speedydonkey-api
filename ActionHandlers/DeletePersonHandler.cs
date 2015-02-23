using Actions;
using Data.Repositories;
using Models;

namespace ActionHandlers
{
    public class DeletePersonHandler : IActionHandler<DeletePerson, Person>
    {
        private readonly IPersonRepository<Student> _userRepository;

        public DeletePersonHandler(IPersonRepository<Student> userRepository)
        {
            _userRepository = userRepository;
        }

        public Person Handle(DeletePerson action)
        {
            _userRepository.Delete(action.ActionAgainst);
            return null;
        }
    }
}
