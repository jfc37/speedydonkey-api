using Actions;
using Data.Repositories;
using Models;

namespace ActionHandlers
{
    public class DeleteCourseWorkHandler : IActionHandler<DeleteCourseWork, CourseWork>
    {
        private readonly ICourseWorkRepository<CourseWork> _courseWorkRepository;

        public DeleteCourseWorkHandler(ICourseWorkRepository<CourseWork> courseWorkRepository)
        {
            _courseWorkRepository = courseWorkRepository;
        }

        public CourseWork Handle(DeleteCourseWork action)
        {
            var assignmentToDelete = _courseWorkRepository.Get(action.ActionAgainst.Id);
            _courseWorkRepository.Delete(assignmentToDelete);
            return assignmentToDelete;
        }
    }
}
