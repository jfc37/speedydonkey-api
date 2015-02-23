using Actions;
using Data.Repositories;
using Models;

namespace ActionHandlers
{
    public class UpdateProfessorHandler : IActionHandler<UpdateProfessor, Professor>
    {
        private readonly IPersonRepository<Professor> _professorRepository;

        public UpdateProfessorHandler(IPersonRepository<Professor> professorRepository)
        {
            _professorRepository = professorRepository;
        }

        public Professor Handle(UpdateProfessor action)
        {
            var originalProfessor = _professorRepository.Get(action.ActionAgainst.Id);
            originalProfessor.FirstName = action.ActionAgainst.FirstName;
            originalProfessor.Surname = action.ActionAgainst.Surname;

            return _professorRepository.Update(action.ActionAgainst);
        }
    }
}
