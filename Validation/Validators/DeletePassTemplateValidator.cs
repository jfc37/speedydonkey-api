using Action;
using Data.Repositories;
using FluentValidation;
using Models;

namespace Validation.Validators
{
    public class DeletePassTemplateValidator : AbstractValidator<PassTemplate>, IActionValidator<DeletePassTemplate, PassTemplate>
    {
        private readonly IRepository<PassTemplate> _repository;

        public DeletePassTemplateValidator(IRepository<PassTemplate> repository)
        {
            _repository = repository;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Id)
                .Must(Exist).WithMessage(ValidationMessages.InvalidPassTemplate);
        }

        private bool Exist(int id)
        {
            return _repository.Get(id) != null;
        }
    }
}
