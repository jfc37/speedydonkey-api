using System.Collections.Generic;
using System.Linq;

namespace Common
{
    public static class EnumerableExtensions
    {
        public static bool DoesNotHaveSameItems<T>(this IEnumerable<T> instance, IEnumerable<T> toCompare) where T : IEntity
        {
            var orginalIds = instance.Select(x => x.Id);
            var updatedIds = toCompare.Select(x => x.Id);
            var hasSameNumber = instance.Count() == toCompare.Count();
            var areSameIds = orginalIds.All(updatedIds.Contains);

            return !hasSameNumber || !areSameIds; 
        }
        public static bool HasSameItems<T>(this IEnumerable<T> instance, IEnumerable<T> toCompare) where T : IEntity
        {
            return !instance.DoesNotHaveSameItems(toCompare);  
        }

        public static bool NotAny<T>(this IEnumerable<T> instatnce)
        {
            return !instatnce.Any();
        }
    }
}
