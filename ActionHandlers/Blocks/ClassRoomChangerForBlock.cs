using ActionHandlers.Classes;
using Data.CodeChunks;
using Data.Repositories;
using Models;
using Validation.Rules;

namespace ActionHandlers.Blocks
{
    public class ClassRoomChangerForBlock : ICodeChunk<Block>
    {
        private readonly Room _room;
        private readonly Block _block;
        private readonly IRepository<Class> _classRepository;

        public ClassRoomChangerForBlock(IRepository<Class> classRepository, Block block, Room room)
        {
            _classRepository = classRepository;
            _block = block;
            _room = room;
        }


        public Block Do()
        {
            foreach (var theClass in _block.Classes)
            {
                var isRoomFreeForClass = new IsRoomAvailable(_room, theClass).IsValid();

                if (isRoomFreeForClass)
                {
                    new ClassRoomChanger(_classRepository, theClass, _room)
                        .Do();
                }
            }

            return _block;
        }
    }
}