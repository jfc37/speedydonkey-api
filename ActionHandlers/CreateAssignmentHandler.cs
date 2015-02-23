using Actions;
using Data.Repositories;
using Models;

namespace ActionHandlers
{
    public class CreateAssignmentHandler : IActionHandler<CreateAssignment, Assignment>
    {
        private readonly IAssignmentRepository _assignmentRepository;

        public CreateAssignmentHandler(IAssignmentRepository assignmentRepository)
        {
            _assignmentRepository = assignmentRepository;
        }

        public Assignment Handle(CreateAssignment action)
        {
            return _assignmentRepository.Create(action.ActionAgainst);
        }
    }
}
