using Actions;
using Autofac;
using Autofac.Core.Registration;
using FluentValidation.Results;
using Validation.Validators;

namespace Validation
{
    public interface IValidatorOverlord
    {
        ValidationResult Validate<TAction, TObject>(TObject validate) where TAction : IAction<TObject>;
    }

    public class ValidatorOverlord : IValidatorOverlord
    {
        private readonly ILifetimeScope _container;

        public ValidatorOverlord(ILifetimeScope container)
        {
            _container = container;
        }

        public ValidationResult Validate<TAction, TObject>(TObject validate) where TAction : IAction<TObject>
        {
            IActionValidator<TAction, TObject> validator;
            try
            {
                validator = GetValidator<TAction, TObject>();
            }
            catch (ComponentNotRegisteredException)
            {
                return new ValidationResult();
            }
            FluentValidation.Results.ValidationResult validationResult;
            if (validate == null)
            {
                validationResult = new FluentValidation.Results.ValidationResult();
                validationResult.Errors.Add(new ValidationFailure("ActionObject", "Please provide an object"));
            }
            else
            {
                validationResult = validator.Validate(validate);   
            }
            return FluentConverter.ToProjectValidationResult(validationResult);
        }

        private IActionValidator<TAction, TObject> GetValidator<TAction, TObject>() where TAction : IAction<TObject>
        {
            var actionValidator = _container.Resolve<IActionValidator<TAction, TObject>>();
            return actionValidator;
        }
    }
}
