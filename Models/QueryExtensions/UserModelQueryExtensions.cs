using System.Collections.Generic;
using System.Linq;

namespace Models.QueryExtensions
{
    public static class UserModelQueryExtensions
    {
        public static IEnumerable<User> WithFullName(this IEnumerable<User> instance, string name)
        {
            return instance.Where(x => x.FullName == name);
        } 
    }
}