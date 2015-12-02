using Data.CodeChunks;
using Data.Repositories;
using Models;

namespace ActionHandlers.Classes
{
    public class ClassRoomChanger : ICodeChunk<Class>
    {
        private readonly IRepository<Class> _repository;
        private readonly Class _classToChange;
        private readonly Room _room;

        public ClassRoomChanger(IRepository<Class> repository, Class classToChange, Room room)
        {
            _repository = repository;
            _classToChange = classToChange;
            _room = room;
        }

        public Class Do()
        {
            _classToChange.Room = _room;

            return _repository.Update(_classToChange);
        }
    }
}