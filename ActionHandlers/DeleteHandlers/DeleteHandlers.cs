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
    public class DeleteLevelHandler : DeleteEntityHandler<DeleteLevel, Level>
    {
        public DeleteLevelHandler(IRepository<Level> repository)
            : base(repository)
        {
        }
    }
    public class DeleteBlockHandler : DeleteEntityHandler<DeleteBlock, Block>
    {
        public DeleteBlockHandler(IRepository<Block> repository)
            : base(repository)
        {
        }
    }
}
