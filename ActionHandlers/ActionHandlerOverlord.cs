using Actions;
using Autofac;
using Validation;
using PostSharp.Patterns.Diagnostics;

namespace ActionHandlers
{
    public interface IActionHandlerOverlord
    {
        ActionReponse<TObject> HandleAction<TAction, TObject>(TAction action) where TAction : IAction<TObject>;
    }

    public class ActionHandlerOverlord : IActionHandlerOverlord
    {
        private readonly IValidatorOverlord _validatorOverlord;
        private readonly ILifetimeScope _container;

        public ActionHandlerOverlord(IValidatorOverlord validatorOverlord, ILifetimeScope container)
        {
            _validatorOverlord = validatorOverlord;
            _container = container;
        }

        [Log]
        public ActionReponse<TObject> HandleAction<TAction, TObject>(TAction action) where TAction : IAction<TObject>
        {
            var validationResult = _validatorOverlord.Validate<TAction, TObject>(action.ActionAgainst);

            if (validationResult.IsValid)
            {
                var actionHandler = GetActionHandler<TAction, TObject>();
                action.ActionAgainst = actionHandler.Handle(action);
            }

            return new ActionReponse<TObject>
            {
                ActionResult = action.ActionAgainst,
                ValidationResult = validationResult
            };
        }

        private IActionHandler<TAction, TObject> GetActionHandler<TAction, TObject>() where TAction : IAction<TObject>
        {
            var actionValidator = _container.Resolve<IActionHandler<TAction, TObject>>();
            return actionValidator;
        }
    }
}
