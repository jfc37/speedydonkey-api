using System;
using Action;
using Data.Repositories;
using FluentValidation;
using Models;

namespace Validation.Validators
{
    public class UpdateBlockValidator : AbstractValidator<Block>, IActionValidator<UpdateBlock, Block>
    {
        private readonly IRepository<Block> _repository;

        public UpdateBlockValidator(IRepository<Block> repository)
        {
            _repository = repository;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(ValidationMessages.MissingName);

            RuleFor(x => x.Id)
                .Must(Exist).WithMessage(ValidationMessages.InvalidBlock);
        }

        private bool Exist(int id)
        {
            return _repository.Get(id) != null;
        }
    }
}
