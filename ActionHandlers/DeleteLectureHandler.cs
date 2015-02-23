using Actions;
using Data.Repositories;
using Models;

namespace ActionHandlers
{
    public class DeleteLectureHandler : IActionHandler<DeleteLecture, Lecture>
    {
        private readonly ILectureRepository _lectureRepository;

        public DeleteLectureHandler(ILectureRepository lectureRepository)
        {
            _lectureRepository = lectureRepository;
        }

        public Lecture Handle(DeleteLecture action)
        {
            var lectureToDelete = _lectureRepository.Get(action.ActionAgainst.Id);
            _lectureRepository.Delete(lectureToDelete);
            return lectureToDelete;
        }
    }
}
