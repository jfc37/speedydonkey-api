using Action.Classes;
using Data.Repositories;
using Models;

namespace ActionHandlers.Classes
{
    public class UnassignClassRoomHandler : IActionHandler<UnassignClassRoom, Class>
    {
        private readonly IRepository<Class> _classRepository;

        public UnassignClassRoomHandler(IRepository<Class> classRepository)
        {
            _classRepository = classRepository;
        }

        public Class Handle(UnassignClassRoom action)
        {
            var theClass = _classRepository.Get(action.ActionAgainst.Id);

            return new ClassRoomChanger(_classRepository, theClass, null)
                .Do();
        }
    }
}