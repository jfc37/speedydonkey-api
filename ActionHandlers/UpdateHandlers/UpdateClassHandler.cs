using Action;
using Common;
using Data.Repositories;
using Models;

namespace ActionHandlers.UpdateHandlers
{
    public class UpdateClassHandler : IActionHandler<UpdateClass, Class>
    {
        private readonly IRepository<Class> _repository;

        public UpdateClassHandler(
            IRepository<Class> repository)
        {
            _repository = repository;
        }

        public Class Handle(UpdateClass action)
        {
            var theClass = _repository.Get(action.ActionAgainst.Id);
            theClass.Name = action.ActionAgainst.Name;
            theClass.StartTime = action.ActionAgainst.StartTime;
            theClass.EndTime= action.ActionAgainst.EndTime;
            _repository.Update(theClass);
            return theClass;
        }
    }
}
