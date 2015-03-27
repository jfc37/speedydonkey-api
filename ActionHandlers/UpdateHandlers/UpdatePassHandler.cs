using Action;
using ActionHandlers.CreateHandlers;
using Data.Repositories;
using Models;

namespace ActionHandlers.UpdateHandlers
{
    public class UpdatePassHandler : UpdateEntityHandler<UpdatePass, Pass>
    {
        public UpdatePassHandler(IRepository<Pass> passRepository)
            : base(passRepository)
        {
        }
    }
}
