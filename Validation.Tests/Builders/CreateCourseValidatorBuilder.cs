using Data;
using Validation.Validators;

namespace Validation.Tests.Builders
{
    public class CreateCourseValidatorBuilder
    {
        private ISpeedyDonkeyDbContext _context;

        public CreateCourseValidatorBuilder WithContext(ISpeedyDonkeyDbContext context)
        {
            _context = context;
            return this;
        }

        public CreateCourseValidator Build()
        {
            return new CreateCourseValidator(_context);
        }
    }
}