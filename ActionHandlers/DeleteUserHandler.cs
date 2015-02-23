using Actions;
using Data.Repositories;
using Models;

namespace ActionHandlers
{
    public class DeleteUserHandler : IActionHandler<DeleteUser, User>
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User Handle(DeleteUser action)
        {
            _userRepository.Delete(action.ActionAgainst);
            return null;
        }
    }
}
