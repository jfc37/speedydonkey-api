using System.Collections.Generic;
using System.Linq;
using Action;
using Data.Repositories;
using Models;

namespace ActionHandlers
{
    public class PurchasePassHandler : IActionHandler<PurchasePass, User>
    {
        private readonly IRepository<User> _userRepository;

        public PurchasePassHandler(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public User Handle(PurchasePass action)
        {
            var user = _userRepository.Get(action.ActionAgainst.Id);
            if (user.Passes == null)
                user.Passes = new List<IPass>();

            user.Passes.Add(action.ActionAgainst.Passes.Single());
            return _userRepository.Update(user);
        }
    }
}
