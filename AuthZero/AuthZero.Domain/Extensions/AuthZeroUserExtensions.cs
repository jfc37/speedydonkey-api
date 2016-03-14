using Models;

namespace AuthZero.Domain.Extensions
{
    public static class AuthZeroUserExtensions
    {
        public static User ToUser(this Auth0.Core.User instance)
        {
            return new User
            {
                Email = instance.Email,
                FirstName = instance.FirstName,
                Surname = instance.LastName,
                GlobalId = instance.UserId
            };
        }
    }
}