using System.Collections.Generic;
using System.Linq;
using Action.StandAloneEvents;
using Data.Repositories;
using Models;

namespace ActionHandlers.StandAloneEvents
{
    public class UpdateStandAloneEventHandler : IActionHandler<UpdateStandAloneEvent, StandAloneEvent>
    {
        private readonly IRepository<StandAloneEvent> _repository;
        private readonly IRepository<Teacher> _teacherRepository;

        public UpdateStandAloneEventHandler(
            IRepository<StandAloneEvent> repository, IRepository<Teacher> teacherRepository)
        {
            _repository = repository;
            _teacherRepository = teacherRepository;
        }

        public StandAloneEvent Handle(UpdateStandAloneEvent action)
        {
            var standAloneEvent = _repository.Get(action.ActionAgainst.Id);
            standAloneEvent.Name = action.ActionAgainst.Name;
            standAloneEvent.StartTime = action.ActionAgainst.StartTime;
            standAloneEvent.EndTime = action.ActionAgainst.EndTime;
            standAloneEvent.IsPrivate = action.ActionAgainst.IsPrivate;
            standAloneEvent.Price = action.ActionAgainst.Price;
            standAloneEvent.ClassCapacity = action.ActionAgainst.ClassCapacity;

            if (HasTeachersChanged(standAloneEvent.Teachers, action.ActionAgainst.Teachers))
            {
                var actualTeachers = action.ActionAgainst.Teachers.Select(teacher => _teacherRepository.Get(teacher.Id)).ToList();
                standAloneEvent.Teachers = actualTeachers;
            }
            _repository.Update(standAloneEvent);
            return standAloneEvent;
        }

        private static bool HasTeachersChanged(IEnumerable<Teacher> orginal, IEnumerable<Teacher> updated)
        {
            var orginalIds = orginal.Select(x => x.Id).ToList();
            var updatedIds = updated.Select(x => x.Id).ToList();
            var hasSameNumber = orginalIds.Count() == updatedIds.Count();
            var areSameIds = orginalIds.All(updatedIds.Contains);

            return !hasSameNumber || !areSameIds;
        }
    }
}