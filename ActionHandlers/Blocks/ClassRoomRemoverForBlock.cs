using ActionHandlers.Classes;
using Data.CodeChunks;
using Data.Repositories;
using Models;

namespace ActionHandlers.Blocks
{
    public class ClassRoomRemoverForBlock : ICodeChunk<Block>
    {
        private readonly Block _block;
        private readonly IRepository<Class> _classRepository;

        public ClassRoomRemoverForBlock(IRepository<Class> classRepository, Block block)
        {
            _classRepository = classRepository;
            _block = block;
        }

        public Block Do()
        {
            foreach (var theClass in _block.Classes)
            {
                new ClassRoomChanger(_classRepository, theClass, null)
                    .Do();
            }

            return _block;
        }
    }
}