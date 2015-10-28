using Actions;
using Autofac;
using Data;
using Models;
using Validation;

namespace ActionHandlers
{
    public interface IActionHandlerOverlord
    {
        ActionReponse<TObject> HandleAction<TAction, TObject>(TAction action) where TAction : IAction<TObject>;
        ActionReponse<TResult> HandleAction<TAction, TObject, TResult>(TAction action) where TAction : IAction<TObject>;
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

        public ActionReponse<TResult> HandleAction<TAction, TObject, TResult>(TAction action) where TAction : IAction<TObject>
        {
            var validationResult = _validatorOverlord.Validate<TAction, TObject>(action.ActionAgainst);

            if (validationResult.IsValid)
            {
                var actionHandler = GetActionHandler<TAction, TObject, TResult>();
                var result = actionHandler.Handle(action);
                return new ActionReponse<TResult>(result);
            }
            else
            {
                return new ActionReponse<TResult>
                {
                    ValidationResult = validationResult
                };
            }
        }

        private IActionHandler<TAction, TObject> GetActionHandler<TAction, TObject>() where TAction : IAction<TObject>
        {
            var actionValidator = _container.Resolve<IActionHandler<TAction, TObject>>();
            return actionValidator;
        }

        private IActionHandlerWithResult<TAction, TObject, TResult> GetActionHandler<TAction, TObject, TResult>() where TAction : IAction<TObject>
        {
            var actionValidator = _container.Resolve<IActionHandlerWithResult<TAction, TObject, TResult>>();
            return actionValidator;
        }
    }
}
