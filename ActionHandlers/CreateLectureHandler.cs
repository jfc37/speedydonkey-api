using Actions;
using Data.Repositories;
using Models;

namespace ActionHandlers
{
    public class CreateLectureHandler : IActionHandler<CreateLecture, Lecture>
    {
        private readonly ILectureRepository _lectureRepository;

        public CreateLectureHandler(ILectureRepository lectureRepository)
        {
            _lectureRepository = lectureRepository;
        }

        public Lecture Handle(CreateLecture action)
        {
            return _lectureRepository.Create(action.ActionAgainst);
        }
    }
}
