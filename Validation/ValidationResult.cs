using System.Collections.Generic;
using System.Linq;
using Common.Extensions;

namespace Validation
{
    public class ValidationResult
    {
        public bool IsValid => !ValidationErrors.Any();
        public List<ValidationError> ValidationErrors { get; set; }

        public ValidationResult()
        {
            ValidationErrors = new List<ValidationError>();
        }

        public override string ToString()
        {
            return this.ToDebugString(nameof(IsValid), nameof(ValidationErrors));
        }
    }
}
