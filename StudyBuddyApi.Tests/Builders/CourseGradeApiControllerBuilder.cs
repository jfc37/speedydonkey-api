using ActionHandlers;
using Data.Repositories;
using SpeedyDonkeyApi.Controllers;
using SpeedyDonkeyApi.Models;

namespace SpeedyDonkeyApi.Tests.Builders
{
    public class CourseGradeApiControllerBuilder
    {
        private ICourseGradeRepository _courseGradeRepository;
        private IModelFactory _modelFactory;

        public CourseGradeApiControllerBuilder WithCourseGradeRepository(ICourseGradeRepository courseGradeRepository)
        {
            _courseGradeRepository = courseGradeRepository;
            return this;
        }

        public CourseGradeApiControllerBuilder WithModelFactory(IModelFactory modelFactory)
        {
            _modelFactory = modelFactory;
            return this;
        }

        public CourseGradeApiController Build()
        {
            return new CourseGradeApiController(_courseGradeRepository, _modelFactory);
        }
    }
}