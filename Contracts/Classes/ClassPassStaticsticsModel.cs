using System.Collections.Generic;
using Contracts.Passes;
using Models;

namespace Contracts.Classes
{
    public class ClassPassStaticsticsModel : IEntityView<Class, PassStatisticModel>
    {
        public IEnumerable<PassStatisticModel> ConvertFromEntity(Class theClass)
        {
            var passStatistics = new List<PassStatisticModel>();
            if (theClass.PassStatistics == null)
                return passStatistics;

            foreach (var passStatistic in theClass.PassStatistics)
            {
                var passStatisticModel = passStatistic.ToModel();
                passStatistics.Add(passStatisticModel);
            }
            return passStatistics;
        }
    }
}