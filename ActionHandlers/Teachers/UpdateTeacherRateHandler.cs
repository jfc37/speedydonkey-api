using Action.Teachers;
using Data.Repositories;
using Models;

namespace ActionHandlers.Teachers
{
    public class UpdateTeacherRateHandler : IActionHandler<UpdateTeacherRate, Teacher>
    {
        private readonly IRepository<Teacher> _repository;

        public UpdateTeacherRateHandler(IRepository<Teacher> repository)
        {
            _repository = repository;
        }

        public Teacher Handle(UpdateTeacherRate action)
        {
            var teacher = _repository.Get(action.ActionAgainst.Id);

            teacher.Rate.PartnerRate = action.ActionAgainst.Rate.PartnerRate;
            teacher.Rate.SoloRate = action.ActionAgainst.Rate.SoloRate;

            return _repository.Update(teacher);
        }
    }
}