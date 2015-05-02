using Action;
using Actions;
using Data.Repositories;
using FluentValidation;
using Models;

namespace Validation.Validators
{
    public abstract class PreExistingValidator<TEntity> : AbstractValidator<TEntity> where TEntity : IEntity
    {
        private readonly IRepository<TEntity> _repository;

        protected PreExistingValidator(IRepository<TEntity> repository)
        {
            _repository = repository;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Id)
                .Must(Exist).WithMessage(ValidationMessages.ItemDoesntExist);
        }

        private bool Exist(int id)
        {
            return _repository.Get(id) != null;
        }
    }
    public abstract class OtherPreExistingValidator<TEntity, TOtherEntity> : AbstractValidator<TEntity> where TEntity : IEntity where TOtherEntity : IEntity
    {
        private readonly IRepository<TOtherEntity> _repository;

        protected OtherPreExistingValidator(IRepository<TOtherEntity> repository)
        {
            _repository = repository;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Id)
                .Must(Exist).WithMessage(ValidationMessages.ItemDoesntExist);
        }

        private bool Exist(int id)
        {
            return _repository.Get(id) != null;
        }
    }

    public class DeletePassTemplateValidator : PreExistingValidator<PassTemplate>, IActionValidator<DeletePassTemplate, PassTemplate>
    {
        public DeletePassTemplateValidator(IRepository<PassTemplate> repository) : base(repository)
        {
        }
    }

    public class DeleteLevelValidator : PreExistingValidator<Level>, IActionValidator<DeleteLevel, Level>
    {
        public DeleteLevelValidator(IRepository<Level> repository)
            : base(repository)
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
        }
    }

    public class SetAsTeacherValidator : OtherPreExistingValidator<Teacher, User>, IActionValidator<SetAsTeacher, Teacher>
    {
        public SetAsTeacherValidator(IRepository<User> repository)
            : base(repository)
        {
        }
    }

    public class RemoveAsTeacherValidator : PreExistingValidator<Teacher>, IActionValidator<RemoveAsTeacher, Teacher>
    {
        public RemoveAsTeacherValidator(IRepository<Teacher> repository)
            : base(repository)
        {
        }
    }
}
