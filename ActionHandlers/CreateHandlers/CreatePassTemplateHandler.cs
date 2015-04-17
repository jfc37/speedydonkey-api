using Action;
using Data.Repositories;
using Models;

namespace ActionHandlers.CreateHandlers
{
    public class CreatePassTemplateHandler : CreateEntityHandler<CreatePassTemplate, PassTemplate>
    {
        public CreatePassTemplateHandler(
            IRepository<PassTemplate> repository) : base(repository)
        {
        }
    }
}
