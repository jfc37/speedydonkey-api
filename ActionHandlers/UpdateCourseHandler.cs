using Actions;
using Data.Repositories;
using Models;

namespace ActionHandlers
{
    public class UpdateCourseHandler : IActionHandler<UpdateCourse, Course>
    {
        private readonly ICourseRepository _courseRepository;

        public UpdateCourseHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public Course Handle(UpdateCourse action)
        {
            var originalCourse = _courseRepository.Get(action.ActionAgainst.Id);
            originalCourse.Description = action.ActionAgainst.Description;
            originalCourse.EndDate = action.ActionAgainst.EndDate;
            originalCourse.GradeType = action.ActionAgainst.GradeType;
            originalCourse.Name = action.ActionAgainst.Name;
            originalCourse.StartDate = action.ActionAgainst.StartDate;

            return _courseRepository.Update(action.ActionAgainst);
        }
    }
}
