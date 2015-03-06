using Actions;
using Data.Repositories;
using Models;

namespace ActionHandlers
{
    public class CreateAccountHandler : IActionHandler<CreateAccount, Account>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public CreateAccountHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public Account Handle(CreateAccount action)
        {
            return action.ActionAgainst;
        }
    }
}
