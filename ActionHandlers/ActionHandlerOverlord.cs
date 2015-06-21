using System;
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
        private readonly IActivityLogger _activityLogger;

        public ActionHandlerOverlord(IValidatorOverlord validatorOverlord, ILifetimeScope container, IActivityLogger activityLogger)
        {
            _validatorOverlord = validatorOverlord;
            _container = container;
            _activityLogger = activityLogger;
        }

        public ActionReponse<TObject> HandleAction<TAction, TObject>(TAction action) where TAction : IAction<TObject>
        {
            LogActivity<TAction, TObject>(action, ActivityType.Beginning);
            var validationResult = _validatorOverlord.Validate<TAction, TObject>(action.ActionAgainst);

            if (validationResult.IsValid)
            {
                var actionHandler = GetActionHandler<TAction, TObject>();
                action.ActionAgainst = actionHandler.Handle(action);
                LogActivity<TAction, TObject>(action, ActivityType.Successful);
            }
            else
            {
                _activityLogger.Log(ActivityGroup.PerformAction, ActivityType.FailedValidation, action.LogText);
            }

            return new ActionReponse<TObject>
            {
                ActionResult = action.ActionAgainst,
                ValidationResult = validationResult
            };
        }

        public ActionReponse<TResult> HandleAction<TAction, TObject, TResult>(TAction action) where TAction : IAction<TObject>
        {
            LogActivity<TAction, TObject>(action, ActivityType.Beginning);
            var validationResult = _validatorOverlord.Validate<TAction, TObject>(action.ActionAgainst);

            if (validationResult.IsValid)
            {
                var actionHandler = GetActionHandler<TAction, TObject, TResult>();
                var result = actionHandler.Handle(action);
                LogActivity<TAction, TObject>(action, ActivityType.Successful);
                return new ActionReponse<TResult>(result);
            }
            else
            {
                _activityLogger.Log(ActivityGroup.PerformAction, ActivityType.FailedValidation, action.LogText);

                return new ActionReponse<TResult>
                {
                    ValidationResult = validationResult
                };
            }
        }

        private void LogActivity<TAction, TObject>(TAction action, ActivityType activityType) where TAction : IAction<TObject>
        {
            var text = String.Format("Action: {0}", typeof (TAction).Name);
            try
            {
                text = String.Format("{0}, Text: {1}", text, action.LogText);
            }
            catch (Exception)
            {
                
            }
            _activityLogger.Log(ActivityGroup.PerformAction, activityType, text);
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
