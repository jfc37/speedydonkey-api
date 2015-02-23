using System.Collections.Generic;
using System.Linq;

namespace Validation
{
    public class ValidationResult
    {
        public bool IsValid { get { return !ValidationErrors.Any(); } }
        public IList<ValidationError> ValidationErrors { get; set; }

        public ValidationResult()
        {
            ValidationErrors = new List<ValidationError>();
        }
    }
}
