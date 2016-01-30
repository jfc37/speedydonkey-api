using Actions;
using Models;

namespace Action.StandAloneEvents
{
    public class CreateStandAloneEvent : SystemAction<StandAloneEvent>, ICrudAction<StandAloneEvent>
    {
        public CreateStandAloneEvent(StandAloneEvent standAloneEvent)
        {
            ActionAgainst = standAloneEvent;
        }
    }
}