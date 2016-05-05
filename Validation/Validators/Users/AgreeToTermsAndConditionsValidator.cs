using Action.Users;
using Data.Repositories;
using FluentValidation;
using Validation.Rules;

namespace Validation.Validators.Users
{
    /// <summary>
    /// Validator for user agreeing to terms and conditions
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator{Models.User}" />
    /// <seealso cref="Validation.Validators.IActionValidator{Action.Users.AgreeToTermsAndConditions, Models.User}" />
    public class AgreeToTermsAndConditionsValidator : AbstractValidator<Models.User>, IActionValidator<AgreeToTermsAndConditions, Models.User>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AgreeToTermsAndConditionsValidator"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        public AgreeToTermsAndConditionsValidator(IRepository<Models.User> userRepository)
        {
            RuleFor(x => x.Id)
                .Must(x => new DoesIdExist<Models.User>(userRepository, x).IsValid())
                .WithMessage(ValidationMessages.InvalidUser);
        }
    }
}