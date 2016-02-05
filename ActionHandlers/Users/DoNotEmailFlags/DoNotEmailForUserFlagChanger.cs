using Data.Repositories;
using Models;

namespace ActionHandlers.Users.DoNotEmailFlags
{
    /// <summary>
    /// Changes do not email flag for user
    /// </summary>
    public class DoNotEmailForUserFlagChanger : IDoNotEmailForUserFlagChanger
    {
        private readonly IRepository<User> _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="DoNotEmailForUserFlagChanger"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public DoNotEmailForUserFlagChanger(IRepository<User> repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Changes the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="doNotEmail">if set to <c>true</c> [do not email].</param>
        /// <returns></returns>
        public User Change(int userId, bool doNotEmail)
        {
            var user = _repository.Get(userId);
            user.DoNotEmail = doNotEmail;

            return _repository.Update(user);
        }
    }
}
