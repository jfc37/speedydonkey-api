using Common.Extensions;
using Validation;

namespace ActionHandlers
{
    public class ActionReponse<TResult>
    {
        public ActionReponse()
        {
            ValidationResult = new ValidationResult();
        }

        public ActionReponse(TResult result)
        {
            ActionResult = result;
            ValidationResult = new ValidationResult();
        }

        public ActionReponse(TResult actionResult, ValidationResult validationResult)
        {
            ActionResult = actionResult;
            ValidationResult = validationResult;
        }

        public ValidationResult ValidationResult { get; set; }
        public TResult ActionResult { get; set; }

        public override string ToString()
        {
            return this.ToDebugString(nameof(ActionResult), nameof(ValidationResult));
        }
    }
}
