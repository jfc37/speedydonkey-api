using Action;
using Common;
using Data.Repositories;
using Models;

namespace ActionHandlers.UpdateHandlers
{
    public class UpdateBlockHandler : IActionHandler<UpdateBlock, Block>
    {
        private readonly IRepository<Block> _repository;

        public UpdateBlockHandler(
            IRepository<Block> repository)
        {
            _repository = repository;
        }

        public Block Handle(UpdateBlock action)
        {
            var block = _repository.Get(action.ActionAgainst.Id);
            block.Name = action.ActionAgainst.Name;
            _repository.Update(block);
            return block;
        }
    }
}
