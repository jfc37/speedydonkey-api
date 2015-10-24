using System.Linq;
using Common.Extensions;
using Data.Repositories;
using Models;

namespace Data.CodeChunks
{
    /// <summary>
    /// Calculates how much money an unlimited pass puts towards each pass.
    /// Divides the pass cost by all the classes that are run while the pass is valid
    /// </summary>
    public class GetCostPerClassForUnlimitedPass : ICodeChunk<decimal>
    {
        private readonly IRepository<Class> _repository;
        private readonly Pass _pass;

        public GetCostPerClassForUnlimitedPass(IRepository<Class> repository, Pass pass)
        {
            _repository = repository;
            _pass = pass;
        }

        public decimal Do()
        {
            var numberOfClassesAvailableForPass = _repository
                .GetAll()
                .Count(x => _pass.StartDate.IsOnOrBefore(x.StartTime) && x.StartTime.IsOnOrBefore(_pass.EndDate));
            if (numberOfClassesAvailableForPass == 0)
                return 0;

            return _pass.Cost / numberOfClassesAvailableForPass;
        }
    }
}