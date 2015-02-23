using Actions;
using Data.Repositories;
using Models;

namespace ActionHandlers
{
    public class CreateUserHandler : IActionHandler<CreateUser, User>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public CreateUserHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public User Handle(CreateUser action)
        {
            action.ActionAgainst.Password = _passwordHasher.CreateHash(action.ActionAgainst.Password);
            return _userRepository.Create(action.ActionAgainst);
        }
    }
}
