using Models;

namespace ActionHandlers.Users.DoNotEmailFlags
{
    /// <summary>
    /// Changes do not email flag for user
    /// </summary>
    public interface IDoNotEmailForUserFlagChanger
    {
        /// <summary>
        /// Changes do not email flag for user
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="doNotEmail">if set to <c>true</c> [do not email].</param>
        /// <returns></returns>
        User Change(int userId, bool doNotEmail);
    }
}