using Actions;
using Data.Repositories;
using Models;

namespace ActionHandlers
{
    public class DeleteCourseHandler : IActionHandler<DeleteCourse, Course>
    {
        private readonly ICourseRepository _courseRepository;

        public DeleteCourseHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public Course Handle(DeleteCourse action)
        {
            var courseToDelete = _courseRepository.Get(action.ActionAgainst.Id);
            _courseRepository.Delete(courseToDelete);
            return courseToDelete;
        }
    }
}
