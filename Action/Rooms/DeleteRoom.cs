using Actions;
using Models;

namespace Action.Rooms
{
    public class DeleteRoom : SystemAction<Room>, ICrudAction<Room>
    {
        public DeleteRoom(Room room)
        {
            ActionAgainst = room;
        }
    }
}