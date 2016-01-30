using Actions;
using Models;

namespace Action.StandAloneEvents
{
    public class CheckStudentIntoEvent : SystemAction<Event>
    {
        public CheckStudentIntoEvent(Event theEvent)
        {
            ActionAgainst = theEvent;
        }
    }
}