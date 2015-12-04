using Action.Users;
using Data.Repositories;
using Models;

namespace ActionHandlers.Users
{
    /// <summary>
    /// Performs the updating of a user's name
    /// </summary>
    /// <seealso />
    public class UpdateUserNamesHandler : IActionHandler<UpdateUserNames, User>
    {
        private readonly IRepository<User> _repository;

        public UpdateUserNamesHandler(IRepository<User> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Performs the updating of a user's name (both first and surname)
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        public User Handle(UpdateUserNames action)
        {
            var user = _repository.Get(action.ActionAgainst.Id);
            user.FirstName = action.ActionAgainst.FirstName;
            user.Surname = action.ActionAgainst.Surname;

            return _repository.Update(user);
        }
    }
}