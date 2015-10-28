using Actions;
using Models;

namespace Action.Rooms
{
    public class CreateRoom : ICrudAction<Room>
    {
        public CreateRoom(Room room)
        {
            ActionAgainst = room;
        }
        public Room ActionAgainst { get; set; }
    }
}