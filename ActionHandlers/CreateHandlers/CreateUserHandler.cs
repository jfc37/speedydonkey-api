using System;
using System.Linq;
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

        protected override void PreHandle(ICrudAction<User> action)
        {
            action.ActionAgainst.Password = _passwordHasher.CreateHash(action.ActionAgainst.Password);
            if (action.ActionAgainst.Email.EndsWith("fullswing.co.nz"))
            {
                var allClaims = Enum.GetValues(typeof(Claim)).Cast<Claim>().ToList();
                allClaims.Remove(Claim.Invalid);
                action.ActionAgainst.Claims = String.Join(",", allClaims);   
            }
        }
    }
}
