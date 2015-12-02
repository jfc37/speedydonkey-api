using Action.Classes;
using Data.Repositories;
using Models;

namespace ActionHandlers.Classes
{
    public class ChangeClassRoomHandler : IActionHandler<ChangeClassRoom, Class>
    {
        private readonly IRepository<Class> _classRepository;
        private readonly IRepository<Room> _roomRepository;

        public ChangeClassRoomHandler(IRepository<Class> classRepository, IRepository<Room> roomRepository)
        {
            _classRepository = classRepository;
            _roomRepository = roomRepository;
        }

        public Class Handle(ChangeClassRoom action)
        {
            var theClass = _classRepository.Get(action.ActionAgainst.Id);
            var room = _roomRepository.Get(action.ActionAgainst.Room.Id);

            return new ClassRoomChanger(_classRepository, theClass, room)
                .Do();
        }
    }
}