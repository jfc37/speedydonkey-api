using Data;
using Data.Repositories;
using Validation.Validators;

namespace Validation.Tests.Builders
{
    public class UpdateUserValidatorBuilder
    {
        private IUserRepository _userRepository;

        public UpdateUserValidator Build()
        {
            return new UpdateUserValidator(_userRepository);
        }

        public UpdateUserValidatorBuilder WithUserRepository(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            return this;
        }
    }
}