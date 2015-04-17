using Action;
using Data.Repositories;
using Models;

namespace ActionHandlers.DeleteHandlers
{
    public class DeletePassTemplateHandler : DeleteEntityHandler<DeletePassTemplate, PassTemplate>
    {
        public DeletePassTemplateHandler(IRepository<PassTemplate> repository) : base(repository)
        {
        }
    }
}
