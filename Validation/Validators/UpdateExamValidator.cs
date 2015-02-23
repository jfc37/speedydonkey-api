using System;
using Actions;
using Data.Repositories;
using FluentValidation;
using Models;

namespace Validation.Validators
{
    public class UpdateExamValidator : AbstractValidator<Exam>, IActionValidator<UpdateExam, Exam>
    {
        private readonly IExamRepository _examRepository;

        public UpdateExamValidator(IExamRepository examRepository)
        {
            _examRepository = examRepository;
            RuleFor(x => x.Id).Must(BeExistingExam).WithMessage(ValidationMessages.ExamDoesntExist);
            RuleFor(x => x.Name).NotEmpty().WithMessage(ValidationMessages.MissingName);
            RuleFor(x => x.FinalMarkPercentage).LessThanOrEqualTo(100).WithMessage(ValidationMessages.FinalMarkPercentageGreaterThan100);
            RuleFor(x => x.StartTime).GreaterThan(DateTime.Today.AddYears(-1)).WithMessage(ValidationMessages.MissingStartTime);
        }

        private bool BeExistingExam(int id)
        {
            return _examRepository.Get(id) != null;
        }
    }
}
