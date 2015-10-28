using Action.Rooms;
using ActionHandlers.UpdateHandlers;
using Data.Repositories;
using Models;

namespace ActionHandlers.Rooms
{
    public class UpdateRoomHandler : IActionHandler<UpdateRoom, Room>
    {
        private readonly IRepository<Room> _repository;

        public UpdateRoomHandler(IRepository<Room> repository)
        {
            _repository = repository;
        }

        public Room Handle(UpdateRoom action)
        {
            var room = _repository.Get(action.ActionAgainst.Id);
            room.Location = action.ActionAgainst.Location;
            room.Name = action.ActionAgainst.Name;

            return _repository.Update(room);
        }
    }
}