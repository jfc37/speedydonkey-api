using Actions;
using Models;

namespace Action.Rooms
{
    public class CreateRoom : SystemAction<Room>, ICrudAction<Room>
    {
        public CreateRoom(Room room)
        {
            ActionAgainst = room;
        }
    }
}