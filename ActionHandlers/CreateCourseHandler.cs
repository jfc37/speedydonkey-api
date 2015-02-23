using Actions;
using Data.Repositories;
using Models;

namespace ActionHandlers
{
    public class CreateCourseHandler : IActionHandler<CreateCourse, Course>
    {
        private readonly ICourseRepository _courseRepository;

        public CreateCourseHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public Course Handle(CreateCourse action)
        {
            return _courseRepository.Create(action.ActionAgainst);
        }
    }
}
