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

        public ValidationResult ValidationResult { get; set; }
        public TResult ActionResult { get; set; }
    }
}
