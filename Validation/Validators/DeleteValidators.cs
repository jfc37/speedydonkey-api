using System.Linq;
using Action;
using Common;
using Data.Repositories;
using FluentValidation;
using Models;
using Validation.Rules;

namespace Validation.Validators
{
    public abstract class PreExistingValidator<TEntity> : AbstractValidator<TEntity> where TEntity : IEntity
    {
        protected PreExistingValidator(IRepository<TEntity> repository)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Id)
                .Must(x => new DoesIdExist<TEntity>(repository, x).IsValid()).WithMessage(ValidationMessages.ItemDoesntExist);
        }
    }
    public abstract class OtherPreExistingValidator<TEntity, TOtherEntity> : AbstractValidator<TEntity> where TEntity : IEntity where TOtherEntity : IEntity
    {
        protected OtherPreExistingValidator(IRepository<TOtherEntity> repository)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Id)
                .Must(x => new DoesIdExist<TOtherEntity>(repository, x).IsValid()).WithMessage(ValidationMessages.ItemDoesntExist);
        }
    }

    public class DeletePassTemplateValidator : PreExistingValidator<PassTemplate>, IActionValidator<DeletePassTemplate, PassTemplate>
    {
        public DeletePassTemplateValidator(IRepository<PassTemplate> repository) : base(repository)
        {
        }
    }

    public class DeleteBlockValidator : PreExistingValidator<Block>, IActionValidator<DeleteBlock, Block>
    {
        public DeleteBlockValidator(IRepository<Block> repository)
            : base(repository)
        {
        }
    }

    public class DeleteClassValidator : PreExistingValidator<Class>, IActionValidator<DeleteClass, Class>
    {
        public DeleteClassValidator(IRepository<Class> repository)
            : base(repository)
        {
            RuleFor(x => x.ActualStudents)
                .Must((x, y) => new HasNoClassAttandanceRule(repository, x.Id).IsValid())
                .WithMessage(ValidationMessages.CannotDeleteClassWithAttendance);
        }
    }
}
