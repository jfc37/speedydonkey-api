using Action.Users;
using Models;

namespace ActionHandlers.Users.DoNotEmailFlags
{
    /// <summary>
    /// Removes the do not email flag for user
    /// </summary>
    /// <seealso cref="ActionHandlers.IActionHandler{Action.Users.RemoveDoNotEmailForUser, Models.User}" />
    public class RemoveDoNotEmailForUserHandler : IActionHandler<RemoveDoNotEmailForUser, User>
    {
        private readonly IDoNotEmailForUserFlagChanger _changer;

        /// <summary>
        /// Initializes a new instance of the <see cref="RemoveDoNotEmailForUserHandler"/> class.
        /// </summary>
        /// <param name="changer">The changer.</param>
        public RemoveDoNotEmailForUserHandler(IDoNotEmailForUserFlagChanger changer)
        {
            _changer = changer;
        }

        /// <summary>
        /// Handles the specified action.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        public User Handle(RemoveDoNotEmailForUser action)
        {
            return _changer.Change(action.ActionAgainst.Id, false);
        }
    }
}