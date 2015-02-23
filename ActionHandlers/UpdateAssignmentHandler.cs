using Actions;
using Data.Repositories;
using Models;

namespace ActionHandlers
{
    public class UpdateAssignmentHandler : IActionHandler<UpdateAssignment, Assignment>
    {
        private readonly IAssignmentRepository _assignmentRepository;

        public UpdateAssignmentHandler(IAssignmentRepository assignmentRepository)
        {
            _assignmentRepository = assignmentRepository;
        }

        public Assignment Handle(UpdateAssignment action)
        {
            var originalAssignment = _assignmentRepository.Get(action.ActionAgainst.Id);
            originalAssignment.Description = action.ActionAgainst.Description;
            originalAssignment.EndDate = action.ActionAgainst.EndDate;
            originalAssignment.FinalMarkPercentage = action.ActionAgainst.FinalMarkPercentage;
            originalAssignment.Name = action.ActionAgainst.Name;
            originalAssignment.StartDate = action.ActionAgainst.StartDate;

            return _assignmentRepository.Update(action.ActionAgainst);
        }
    }
}
