using Action;
using ActionHandlers.CreateHandlers;
using Common;
using Data.Repositories;
using Models;

namespace ActionHandlers.UpdateHandlers
{
    public class UpdatePassTemplateHandler : UpdateEntityHandler<UpdatePassTemplate, PassTemplate>
    {
        public UpdatePassTemplateHandler(
            IRepository<PassTemplate> repository)
            : base(repository)
        {
        }
    }
}
