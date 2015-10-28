using Action.Rooms;
using ActionHandlers.DeleteHandlers;
using Data.Repositories;
using Models;

namespace ActionHandlers.Rooms
{
    public class DeleteRoomHandler : DeleteEntityHandler<DeleteRoom, Room>
    {
        public DeleteRoomHandler(
            IRepository<Room> repository) : base(repository)
        {
        }
    }
}