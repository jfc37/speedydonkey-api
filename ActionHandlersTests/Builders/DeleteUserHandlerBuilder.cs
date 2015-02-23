using ActionHandlers;
using Data.Repositories;

namespace ActionHandlersTests.Builders
{
    public class DeleteUserHandlerBuilder
    {
        private IUserRepository _userRepository;

        public DeleteUserHandler Build()
        {
            return new DeleteUserHandler(_userRepository);
        }

        public DeleteUserHandlerBuilder WithUserRepository(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            return this;
        }
    }
}