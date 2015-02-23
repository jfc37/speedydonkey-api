using Actions;
using Data.Repositories;
using FluentValidation;
using Models;

namespace Validation.Validators
{
    public class UpdateAssignmentValidator : AbstractValidator<Assignment>, IActionValidator<UpdateAssignment, Assignment>
    {
        private readonly IAssignmentRepository _assignmentRepository;

        public UpdateAssignmentValidator(IAssignmentRepository assignmentRepository)
        {
            _assignmentRepository = assignmentRepository;
            RuleFor(x => x.Id).Must(BeExistingAssignment).WithMessage(ValidationMessages.AssignmentDoesntExist);
            RuleFor(x => x.Name).NotEmpty().WithMessage(ValidationMessages.MissingName);
            RuleFor(x => x.EndDate).GreaterThan(x => x.StartDate).WithMessage(ValidationMessages.EndDateBeforeStartDate);
            RuleFor(x => x.FinalMarkPercentage).LessThanOrEqualTo(100).WithMessage(ValidationMessages.FinalMarkPercentageGreaterThan100);
        }

        private bool BeExistingAssignment(int id)
        {
            return _assignmentRepository.Get(id) != null;
        }
    }
}
