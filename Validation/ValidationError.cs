namespace Validation
{
    public class ValidationError
    {
        public string PropertyName { get; set; }
        public string ErrorMessage { get; set; }

        public ValidationError(string property, string error)
        {
            PropertyName = property;
            ErrorMessage = error;
        }
    }
}
