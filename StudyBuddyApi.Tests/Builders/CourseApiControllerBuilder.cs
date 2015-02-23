using ActionHandlers;
using Data.Repositories;
using Data.Searches;
using Models;
using SpeedyDonkeyApi.Controllers;
using SpeedyDonkeyApi.Models;

namespace SpeedyDonkeyApi.Tests.Builders
{
    public class CourseApiControllerBuilder
    {
        private ICourseRepository _courseRepository;
        private IActionHandlerOverlord _actionHandlerOverlord;
        private IModelFactory _modelFactory;
        private IEntitySearch<Course> _courseSearch;

        public CourseApiController Build()
        {
            return new CourseApiController(_courseRepository, _actionHandlerOverlord, _modelFactory, _courseSearch);
        }

        public CourseApiControllerBuilder WithCourseRepository(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
            return this;
        }

        public CourseApiControllerBuilder WithActionHandlerOverlord(IActionHandlerOverlord actionHandlerOverlord)
        {
            _actionHandlerOverlord = actionHandlerOverlord;
            return this;
        }

        public CourseApiControllerBuilder WithModelFactory(IModelFactory modelFactory)
        {
            _modelFactory = modelFactory;
            return this;
        }

        public CourseApiControllerBuilder WithCourseSearch(IEntitySearch<Course> courseSearch)
        {
            _courseSearch = courseSearch;
            return this;
        }
    }
}