namespace AuthZero.Interfaces
{
    /// <summary>
    /// Repository for auth0 email verfication
    /// </summary>
    public interface IAuthZeroEmailVerificationRepository
    {
        /// <summary>
        /// Creates an email verification ticket for 
        /// </summary>
        /// <param name="globalId">The global identifier.</param>
        /// <returns></returns>
        string Create(string globalId);
    }
}