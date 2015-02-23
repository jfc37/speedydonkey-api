using Actions;
using Data.Repositories;
using Models;

namespace ActionHandlers
{
    public class CreateExamHandler : IActionHandler<CreateExam, Exam>
    {
        private readonly IExamRepository _examRepository;

        public CreateExamHandler(IExamRepository examRepository)
        {
            _examRepository = examRepository;
        }

        public Exam Handle(CreateExam action)
        {
            return _examRepository.Create(action.ActionAgainst);
        }
    }
}
