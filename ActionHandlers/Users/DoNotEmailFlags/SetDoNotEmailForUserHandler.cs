using Action.Users;
using Models;

namespace ActionHandlers.Users.DoNotEmailFlags
{
    /// <summary>
    /// Sets do not email flag for user
    /// </summary>
    /// <seealso cref="ActionHandlers.IActionHandler{Action.Users.SetDoNotEmailForUser, Models.User}" />
    public class SetDoNotEmailForUserHandler : IActionHandler<SetDoNotEmailForUser, User>
    {
        private readonly IDoNotEmailForUserFlagChanger _changer;

        /// <summary>
        /// Initializes a new instance of the <see cref="SetDoNotEmailForUserHandler"/> class.
        /// </summary>
        /// <param name="changer">The changer.</param>
        public SetDoNotEmailForUserHandler(IDoNotEmailForUserFlagChanger changer)
        {
            _changer = changer;
        }

        /// <summary>
        /// Handles the specified action.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        public User Handle(SetDoNotEmailForUser action)
        {
            return _changer.Change(action.ActionAgainst.Id, true);
        }
    }
}