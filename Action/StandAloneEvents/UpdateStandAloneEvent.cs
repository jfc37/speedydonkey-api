using Actions;
using Models;

namespace Action.StandAloneEvents
{
    public class UpdateStandAloneEvent : SystemAction<StandAloneEvent>, ICrudAction<StandAloneEvent>
    {
        public UpdateStandAloneEvent(StandAloneEvent standAloneEvent)
        {
            ActionAgainst = standAloneEvent;
        }
    }
}