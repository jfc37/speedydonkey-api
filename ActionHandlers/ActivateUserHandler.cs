using System.Linq;
using Action;
using Data.Repositories;
using Models;

namespace ActionHandlers
{
    public class ActivateUserHandler : IActionHandler<ActivateUser, User>
    {
        private readonly IRepository<User> _repository;

        public ActivateUserHandler(IRepository<User> repository)
        {
            _repository = repository;
        }

        public User Handle(ActivateUser action)
        {
            var userToActivate = _repository.Queryable().Single(x => x.ActivationKey == action.ActionAgainst.ActivationKey);
            userToActivate.Status = UserStatus.Active;
            _repository.Update(userToActivate);

            return userToActivate;
        }
    }
}
