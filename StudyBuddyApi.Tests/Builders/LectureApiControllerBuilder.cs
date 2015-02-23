using ActionHandlers;
using Data.Repositories;
using Data.Searches;
using Models;
using SpeedyDonkeyApi.Controllers;
using SpeedyDonkeyApi.Models;

namespace SpeedyDonkeyApi.Tests.Builders
{
    public class LectureApiControllerBuilder
    {
        private ILectureRepository _lectureRepository;
        private IActionHandlerOverlord _actionHandlerOverlord;
        private IModelFactory _modelFactory;
        private IEntitySearch<Lecture> _entitySearch;

        public LectureApiControllerBuilder WithLectureRepository(ILectureRepository lectureRepository)
        {
            _lectureRepository = lectureRepository;
            return this;
        }

        public LectureApiControllerBuilder WithActionHandlerOverlord(IActionHandlerOverlord actionHandlerOverlord)
        {
            _actionHandlerOverlord = actionHandlerOverlord;
            return this;
        }

        public LectureApiControllerBuilder WithModelFactory(IModelFactory modelFactory)
        {
            _modelFactory = modelFactory;
            return this;
        }

        public LectureApiController Build()
        {
            return new LectureApiController(_lectureRepository, _actionHandlerOverlord, _modelFactory, _entitySearch);
        }

        public LectureApiControllerBuilder WithEntitySearch(IEntitySearch<Lecture> entitySearch)
        {
            _entitySearch = entitySearch;
            return this;
        }
    }
}