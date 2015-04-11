using System.Linq;

namespace Common
{
    public interface ICommonInterfaceCloner
    {
        TCloneTo Clone<TCloneFrom, TCloneTo>(TCloneFrom cloneFrom) where TCloneTo : new();
        TCopyTo Copy<TCopyFrom, TCopyTo>(TCopyFrom copyFrom, TCopyTo copyTo);
    }
    public class CommonInterfaceCloner : ICommonInterfaceCloner
    {
        public TCloneTo Clone<TCloneFrom, TCloneTo>(TCloneFrom cloneFrom) where TCloneTo : new()
        {
            var cloneFromInterfaces = typeof (TCloneFrom).GetInterfaces();
            var cloneToInterfaces = typeof (TCloneTo).GetInterfaces();

            var commonInterfaces = cloneFromInterfaces.Intersect(cloneToInterfaces);
            var clone = new TCloneTo();
            foreach (var commonInterface in commonInterfaces)
            {
                var propertiesToCopy = commonInterface.GetProperties();
                foreach (var propertyInfo in propertiesToCopy)
                {
                    if (!propertyInfo.PropertyType.IsInterface && propertyInfo.CanWrite)
                        propertyInfo.SetValue(clone, propertyInfo.GetValue(cloneFrom));
                }
            }
            return clone;
        }

        public TCopyTo Copy<TCopyFrom, TCopyTo>(TCopyFrom copyFrom, TCopyTo copyTo)
        {
            var cloneFromInterfaces = typeof(TCopyFrom).GetInterfaces();
            var cloneToInterfaces = typeof(TCopyTo).GetInterfaces();

            var commonInterfaces = cloneFromInterfaces.Intersect(cloneToInterfaces);
            foreach (var commonInterface in commonInterfaces)
            {
                var propertiesToCopy = commonInterface.GetProperties();
                foreach (var propertyInfo in propertiesToCopy)
                {
                    if (!propertyInfo.PropertyType.IsInterface && propertyInfo.CanWrite)
                        propertyInfo.SetValue(copyTo, propertyInfo.GetValue(copyFrom));
                }
            }
            return copyTo;
        }
    }
}
