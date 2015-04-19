using Action;
using Common;
using Data.Repositories;
using Models;

namespace ActionHandlers.UpdateHandlers
{
    public class UpdateLevelHandler : UpdateEntityHandler<UpdateLevel, Level>
    {
        public UpdateLevelHandler(
            IRepository<Level> repository, ICommonInterfaceCloner cloner)
            : base(repository, cloner)
        {
        }
    }
}
