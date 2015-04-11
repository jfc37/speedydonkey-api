using ActionHandlers.CreateHandlers;
using Actions;
using Common;
using Data.Repositories;
using Models;
using Notification;

namespace ActionHandlers.UpdateHandlers
{
    public class UpdateUserHandler : UpdateEntityHandler<UpdateUser, User>
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IPostOffice _postOffice;
        private readonly IAppSettings _appSettings;

        public UpdateUserHandler(IRepository<User> repository, IPasswordHasher passwordHasher, ICommonInterfaceCloner cloner)
            : base(repository, cloner)
        {
            _passwordHasher = passwordHasher;
        }

        protected override void PreHandle(ICrudAction<User> action)
        {
            action.ActionAgainst.Password = _passwordHasher.CreateHash(action.ActionAgainst.Password);

        }
    }
}
