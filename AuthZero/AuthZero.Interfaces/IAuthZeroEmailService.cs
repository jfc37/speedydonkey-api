namespace AuthZero.Interfaces
{
    /// <summary>
    /// Email services for auth0
    /// </summary>
    public interface IAuthZeroEmailService
    {
        /// <summary>
        /// Sends an verification email for a given auth0 user.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        void SendVerification(string userId);
    }
}
