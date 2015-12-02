using Actions;
using Models;

namespace Action.Rooms
{
    public class UpdateRoom : ICrudAction<Room>
    {
        public UpdateRoom(Room room)
        {
            ActionAgainst = room;
        }
        public Room ActionAgainst { get; set; }
    }
    public class DeleteRoom : ICrudAction<Room>
    {
        public DeleteRoom(Room room)
        {
            ActionAgainst = room;
        }
        public Room ActionAgainst { get; set; }
    }
}