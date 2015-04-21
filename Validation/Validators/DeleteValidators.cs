using Action;
using Data.Repositories;
using FluentValidation;
using Models;

namespace Validation.Validators
{
    public abstract class DeleteValidator<TEntity> : AbstractValidator<TEntity> where TEntity : IEntity
    {
        private readonly IRepository<TEntity> _repository;

        protected DeleteValidator(IRepository<TEntity> repository)
        {
            _repository = repository;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Id)
                .Must(Exist).WithMessage(ValidationMessages.InvalidItemToDelete);
        }

        private bool Exist(int id)
        {
            return _repository.Get(id) != null;
        }
    }

    public class DeletePassTemplateValidator : DeleteValidator<PassTemplate>, IActionValidator<DeletePassTemplate, PassTemplate>
    {
        public DeletePassTemplateValidator(IRepository<PassTemplate> repository) : base(repository)
        {
        }
    }

    public class DeleteLevelValidator : DeleteValidator<Level>, IActionValidator<DeleteLevel, Level>
    {
        public DeleteLevelValidator(IRepository<Level> repository)
            : base(repository)
        {
        }
    }

    public class DeleteBlockValidator : DeleteValidator<Block>, IActionValidator<DeleteBlock, Block>
    {
        public DeleteBlockValidator(IRepository<Block> repository)
            : base(repository)
        {
        }
    }
}
