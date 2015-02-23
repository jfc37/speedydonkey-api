using Actions;
using Data.Repositories;
using Models;

namespace ActionHandlers
{
    public class UpdateUserHandler : IActionHandler<UpdateUser, User>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public UpdateUserHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public User Handle(UpdateUser action)
        {
            var originalUser = _userRepository.Get(action.ActionAgainst.Id);
            originalUser.Password = _passwordHasher.CreateHash(action.ActionAgainst.Password);
            originalUser.Username = action.ActionAgainst.Username;

            return _userRepository.Update(action.ActionAgainst);
        }
    }
}
