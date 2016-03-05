using System;
using System.Linq;
using Action;
using Data.Repositories;
using Models;

namespace ActionHandlers
{
    public class ResetPasswordHandler : IActionHandler<ResetPassword, User>
    {
        private readonly IRepository<User> _repository;
        private readonly IPasswordHasher _passwordHasher;

        public ResetPasswordHandler(IRepository<User> repository, IPasswordHasher passwordHasher)
        {
            _repository = repository;
            _passwordHasher = passwordHasher;
        }

        public User Handle(ResetPassword action)
        {
            var user = _repository.Queryable().Single(x => x.ActivationKey == action.ActionAgainst.ActivationKey);
            user.ActivationKey = new Guid();
            user.Password = _passwordHasher.CreateHash(action.ActionAgainst.Password);
            _repository.Update(user);
            
            return user;
        }
    }
}
