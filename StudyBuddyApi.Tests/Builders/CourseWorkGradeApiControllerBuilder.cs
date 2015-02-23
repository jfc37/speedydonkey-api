using ActionHandlers;
using Data.Repositories;
using SpeedyDonkeyApi.Controllers;
using SpeedyDonkeyApi.Models;

namespace SpeedyDonkeyApi.Tests.Builders
{
    public class CourseWorkGradeApiControllerBuilder
    {
        private ICourseWorkGradeRepository _courseWorkGradeRepository;
        private IActionHandlerOverlord _actionHandlerOverlord;
        private IModelFactory _modelFactory;

        public CourseWorkGradeApiControllerBuilder WithCourseWorkGradeRepository(ICourseWorkGradeRepository courseWorkGradeRepository)
        {
            _courseWorkGradeRepository = courseWorkGradeRepository;
            return this;
        }

        public CourseWorkGradeApiControllerBuilder WithActionHandlerOverlord(IActionHandlerOverlord actionHandlerOverlord)
        {
            _actionHandlerOverlord = actionHandlerOverlord;
            return this;
        }

        public CourseWorkGradeApiControllerBuilder WithModelFactory(IModelFactory modelFactory)
        {
            _modelFactory = modelFactory;
            return this;
        }

        public CourseWorkGradeApiController Build()
        {
            return new CourseWorkGradeApiController(_courseWorkGradeRepository, _actionHandlerOverlord, _modelFactory);
        }
    }
}