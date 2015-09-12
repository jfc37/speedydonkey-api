using Actions;
using Common;
using Data.Repositories;
using Models;

namespace ActionHandlers.UpdateHandlers
{
    public class UpdateUserHandler : UpdateEntityHandler<UpdateUser, User>
    {
        private readonly PasswordHasher _passwordHasher;

        public UpdateUserHandler(IRepository<User> repository, PasswordHasher passwordHasher)
            : base(repository)
        {
            _passwordHasher = passwordHasher;
        }

        protected override void PreHandle(ICrudAction<User> action)
        {
            action.ActionAgainst.Password = _passwordHasher.CreateHash(action.ActionAgainst.Password);

        }
    }
}
