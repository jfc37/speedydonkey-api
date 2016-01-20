using Action.StandAloneEvents;
using ActionHandlers.CreateHandlers;
using Data.Repositories;
using Models;

namespace ActionHandlers.StandAloneEvents
{
    public class CreateStandAloneEventHandler : CreateEntityHandler<CreateStandAloneEvent, StandAloneEvent>
    {
        public CreateStandAloneEventHandler(
            IRepository<StandAloneEvent> repository) : base(repository)
        {
        }
    }
}