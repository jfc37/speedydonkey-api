using System.Collections.Generic;
using System.Linq;

namespace Models.QueryExtensions
{
    public static class UserModelQueryExtensions
    {
        public static IEnumerable<IUser> WithFullName(this IEnumerable<IUser> instance, string name)
        {
            return instance.Where(x => x.FullName == name);
        } 
    }
}