using Actions;
using Models;

namespace Action.StandAloneEvents
{
    public class UpdateStandAloneEvent : ICrudAction<StandAloneEvent>
    {
        public UpdateStandAloneEvent(StandAloneEvent standAloneEvent)
        {
            ActionAgainst = standAloneEvent;
        }

        public StandAloneEvent ActionAgainst { get; set; }
    }
}