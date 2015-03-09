using Actions;
using Data.Repositories;
using Models;

namespace ActionHandlers.CreateHandlers
{
    public class CreateUserHandler : CreateEntityHandler<CreateUser, User>
    {
        private readonly IPasswordHasher _passwordHasher;

        public CreateUserHandler(
            IRepository<User> repository,
            IPasswordHasher passwordHasher) : base(repository)
        {
            _passwordHasher = passwordHasher;
        }

        protected override void PreHandle(ICreateAction<User> action)
        {
            action.ActionAgainst.Password = _passwordHasher.CreateHash(action.ActionAgainst.Password);
        }
    }
}
