using Action.Rooms;
using ActionHandlers.CreateHandlers;
using Data.Repositories;
using Models;

namespace ActionHandlers.Rooms
{
    public class CreateRoomHandler : CreateEntityHandler<CreateRoom, Room>
    {
        public CreateRoomHandler(
            IRepository<Room> repository) : base(repository)
        {
        }
    }
}
