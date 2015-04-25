using System.Collections.Generic;
using System.Linq;
using Action;
using Data.Repositories;
using Models;

namespace ActionHandlers.UpdateHandlers
{
    public class UpdateBlockHandler : IActionHandler<UpdateBlock, Block>
    {
        private readonly IRepository<Block> _repository;
        private readonly IRepository<User> _userRepository;

        public UpdateBlockHandler(
            IRepository<Block> repository,
            IRepository<User> userRepository)
        {
            _repository = repository;
            _userRepository = userRepository;
        }

        public Block Handle(UpdateBlock action)
        {
            var block = _repository.Get(action.ActionAgainst.Id);
            block.Name = action.ActionAgainst.Name;

            if (HasTeachersChanged(block.Teachers, action.ActionAgainst.Teachers))
            {
                var actualTeachers = action.ActionAgainst.Teachers.Select(teacher => _userRepository.Get(teacher.Id)).Cast<IUser>().ToList();
                block.Teachers = actualTeachers;
            }
            _repository.Update(block);
            return block;
        }

        private bool HasTeachersChanged(IList<IUser> orginal, IList<IUser> updated)
        {
            var orginalIds = orginal.Select(x => x.Id);
            var updatedIds = updated.Select(x => x.Id);
            var hasSameNumber = orginalIds.Count() == updatedIds.Count();
            var areSameIds = orginalIds.All(x => updatedIds.Contains(x));

            return !hasSameNumber || !areSameIds;
        }
    }
}
