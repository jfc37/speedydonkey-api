using Actions;
using Models;

namespace Action.Rooms
{
    public class UpdateRoom : SystemAction<Room>, ICrudAction<Room>
    {
        public UpdateRoom(Room room)
        {
            ActionAgainst = room;
        }
    }
}