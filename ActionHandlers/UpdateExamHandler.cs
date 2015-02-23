using Actions;
using Data.Repositories;
using Models;

namespace ActionHandlers
{
    public class UpdateExamHandler : IActionHandler<UpdateExam, Exam>
    {
        private readonly IExamRepository _examRepository;

        public UpdateExamHandler(IExamRepository examRepository)
        {
            _examRepository = examRepository;
        }

        public Exam Handle(UpdateExam action)
        {
            var originalExam = _examRepository.Get(action.ActionAgainst.Id);
            originalExam.Description = action.ActionAgainst.Description;
            originalExam.Location = action.ActionAgainst.Location;
            originalExam.FinalMarkPercentage = action.ActionAgainst.FinalMarkPercentage;
            originalExam.Name = action.ActionAgainst.Name;
            originalExam.StartTime = action.ActionAgainst.StartTime;

            return _examRepository.Update(action.ActionAgainst);
        }
    }
}
