using System.Collections.Generic;
using System.Linq;
using Common;

namespace Models.QueryExtensions
{
    public static class EntityQueryExtensions
    {
        public static T SingleWithId<T>(this IEnumerable<T> instance, int id)
            where T : IEntity
        {
            return instance.Single(x => x.Id == id);
        } 
    }
}
