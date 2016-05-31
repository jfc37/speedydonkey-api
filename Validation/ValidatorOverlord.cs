using Actions;
using Autofac;
using Autofac.Core.Registration;
using FluentValidation;
using FluentValidation.Results;
using Validation.Validators;

namespace Validation
{
    public interface IValidatorOverlord
    {
        /// <summary>
        /// Validates any object
        /// </summary>
        /// <typeparam name="TObject">The type of the object.</typeparam>
        /// <param name="validate">The validate.</param>
        /// <returns></returns>
        ValidationResult Validate<TObject>(TObject validate);

        /// <summary>
        /// Validates an action
        /// </summary>
        /// <typeparam name="TAction">The type of the action.</typeparam>
        /// <typeparam name="TObject">The type of the object.</typeparam>
        /// <param name="validate">The validate.</param>
        /// <returns></returns>
        ValidationResult Validate<TAction, TObject>(TAction validate) where TAction : SystemAction<TObject>;
    }

    public class ValidatorOverlord : IValidatorOverlord
    {
        private readonly ILifetimeScope _container;

        public ValidatorOverlord(ILifetimeScope container)
        {
            _container = container;
        }
        
        /// <summary>
        /// Validates any object
        /// </summary>
        /// <typeparam name="TObject">The type of the object.</typeparam>
        /// <param name="validate">The validate.</param>
        /// <returns></returns>
        public ValidationResult Validate<TObject>(TObject validate)
        {
            FluentValidation.Results.ValidationResult validationResult;
            if (validate == null)
            {
                validationResult = new FluentValidation.Results.ValidationResult();
                validationResult.Errors.Add(new ValidationFailure("ActionObject", "Please provide some input"));
            }
            else
            {
                var validator = GetValidator<TObject>();
                validationResult = validator.Validate(validate);
            }

            return FluentConverter.ToProjectValidationResult(validationResult);
        }

        /// <summary>
        /// Validates an action
        /// </summary>
        /// <typeparam name="TAction">The type of the action.</typeparam>
        /// <typeparam name="TObject">The type of the object.</typeparam>
        /// <param name="validate">The validate.</param>
        /// <returns></returns>
        public ValidationResult Validate<TAction, TObject>(TAction validate) where TAction : SystemAction<TObject>
        {
            FluentValidation.Results.ValidationResult validationResult;
            if (validate.ActionAgainst == null)
            {
                validationResult = new FluentValidation.Results.ValidationResult();
                validationResult.Errors.Add(new ValidationFailure("ActionObject", "Please provide an object"));
            }
            else
            {
                try
                {
                    validationResult = GetValidator<TAction, TObject>()
                        .Validate(validate.ActionAgainst);
                }
                catch (ComponentNotRegisteredException)
                {
                    try
                    {
                        validationResult = GetValidator<TAction>()
                            .Validate(validate);
                    }
                    catch (ComponentNotRegisteredException)
                    {
                        return new ValidationResult();
                    }
                }
            }
            
            return FluentConverter.ToProjectValidationResult(validationResult);
        }

        private IActionValidator<TAction, TObject> GetValidator<TAction, TObject>() where TAction : SystemAction<TObject>
        {
            var actionValidator = _container.Resolve<IActionValidator<TAction, TObject>>();
            return actionValidator;
        }

        private IValidator<TObject> GetValidator<TObject>()
        {
            var actionValidator = _container.Resolve<IValidator<TObject>>();
            return actionValidator;
        }
    }
}
