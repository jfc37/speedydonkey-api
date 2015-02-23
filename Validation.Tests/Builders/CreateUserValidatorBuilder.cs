using Data;
using Validation.Validators;

namespace Validation.Tests.Builders
{
    public class CreateUserValidatorBuilder
    {
        private ISpeedyDonkeyDbContext _context;

        public CreateUserValidator Build()
        {
            return new CreateUserValidator(_context);
        }

        public CreateUserValidatorBuilder WithContext(ISpeedyDonkeyDbContext context)
        {
            _context = context;
            return this;
        }
    }
}