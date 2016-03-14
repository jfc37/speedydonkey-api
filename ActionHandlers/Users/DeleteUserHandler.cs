using System.Linq;
using Action;
using AuthZero.Interfaces;
using Data.Repositories;
using Models;

namespace ActionHandlers.Users
{
    public class DeleteUserHandler : IActionHandler<DeleteUser, User>
    {
        private readonly IRepository<User> _repository;
        private readonly IAuthZeroClientRepository _authZeroClientRepository;

        public DeleteUserHandler(
            IRepository<User> repository,
            IAuthZeroClientRepository authZeroClientRepository)
        {
            _repository = repository;
            _authZeroClientRepository = authZeroClientRepository;
        }

        public User Handle(DeleteUser action)
        {
            DeleteFromAuth0(action);

            _repository.SoftDelete(action.ActionAgainst.Id);

            return action.ActionAgainst;
        }

        private void DeleteFromAuth0(DeleteUser action)
        {
            var user = _repository.Queryable()
                .Single(x => x.Id == action.ActionAgainst.Id);

            _authZeroClientRepository.Delete(user.GlobalId);
        }
    }
}