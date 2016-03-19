using Contracts.Users;
using Models;

namespace Contracts.MappingExtensions
{
    public static class UserMappingExtensions
    {
        public static User ToEntity(this AuthZeroUserModel instance)
        {
            return new User
            {
                GlobalId = instance.UserId
            };
        }
    }
}
