using Validation;

namespace ActionHandlers
{
    public class ActionReponse<TResult>
    {
        public ValidationResult ValidationResult { get; set; }
        public TResult ActionResult { get; set; }
    }
}
