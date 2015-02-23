using Actions;
using Data.Repositories;
using Models;

namespace ActionHandlers
{
    public class UpdateLectureHandler : IActionHandler<UpdateLecture, Lecture>
    {
        private readonly ILectureRepository _lectureRepository;

        public UpdateLectureHandler(ILectureRepository lectureRepository)
        {
            _lectureRepository = lectureRepository;
        }

        public Lecture Handle(UpdateLecture action)
        {
            var originalLecture = _lectureRepository.Get(action.ActionAgainst.Id);
            originalLecture.Description = action.ActionAgainst.Description;
            originalLecture.Location = action.ActionAgainst.Location;
            originalLecture.EndDate = action.ActionAgainst.EndDate;
            originalLecture.Name = action.ActionAgainst.Name;
            originalLecture.Occurence = action.ActionAgainst.Occurence;
            originalLecture.StartDate = action.ActionAgainst.StartDate;

            return _lectureRepository.Update(action.ActionAgainst);
        }
    }
}
