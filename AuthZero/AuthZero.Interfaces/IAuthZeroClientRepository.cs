using Models;

namespace AuthZero.Interfaces
{
    /// <summary>
    /// Repository for users from Auth0
    /// </summary>
    public interface IAuthZeroClientRepository
    {
        /// <summary>
        /// Creates the user in auth0, returning the auth0 user id
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        string Create(User user);

        /// <summary>
        /// Gets a user
        /// </summary>
        /// <param name="globalId">The global identifier.</param>
        /// <returns></returns>
        User Get(string globalId);

        /// <summary>
        /// Deletes a user
        /// </summary>
        /// <param name="globalId">The global identifier.</param>
        void Delete(string globalId);
    }
}
