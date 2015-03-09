using Action;
using Data.Repositories;
using Models;

namespace ActionHandlers.CreateHandlers
{
    public class CreateLevelHandler : CreateEntityHandler<CreateLevel, Level>
    {
        public CreateLevelHandler(IRepository<Level> repository) : base(repository)
        {
        }
    }
}
