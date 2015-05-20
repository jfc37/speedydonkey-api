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
        private readonly IRepository<Teacher> _teacherRepository;

        public UpdateBlockHandler(
            IRepository<Block> repository,
            IRepository<Teacher> teacherRepository)
        {
            _repository = repository;
            _teacherRepository = teacherRepository;
        }

        public Block Handle(UpdateBlock action)
        {
            var block = _repository.Get(action.ActionAgainst.Id);
            block.Name = action.ActionAgainst.Name;

            var startDateDifference = block.StartDate.Subtract(action.ActionAgainst.StartDate);
            var endDateDifference = block.EndDate.Subtract(action.ActionAgainst.EndDate);
            foreach (var theClass in block.Classes)
            {
                theClass.StartTime = theClass.StartTime.Subtract(startDateDifference);
                theClass.EndTime = theClass.EndTime.Subtract(endDateDifference);
            }
            block.StartDate = action.ActionAgainst.StartDate;
            block.EndDate = action.ActionAgainst.EndDate;

            if (HasTeachersChanged(block.Teachers, action.ActionAgainst.Teachers))
            {
                var actualTeachers = action.ActionAgainst.Teachers.Select(teacher => _teacherRepository.Get(teacher.Id)).Cast<ITeacher>().ToList();
                block.Teachers = actualTeachers;
                foreach (var theClass in block.Classes)
                {
                    theClass.Teachers = block.Teachers;
                }
            }

            _repository.Update(block);
            return block;
        }

        private bool HasTeachersChanged(ICollection<ITeacher> orginal, ICollection<ITeacher> updated)
        {
            var orginalIds = orginal.Select(x => x.Id);
            var updatedIds = updated.Select(x => x.Id);
            var hasSameNumber = orginalIds.Count() == updatedIds.Count();
            var areSameIds = orginalIds.All(x => updatedIds.Contains(x));

            return !hasSameNumber || !areSameIds;
        }
    }
}
