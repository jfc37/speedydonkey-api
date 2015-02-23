using ActionHandlers;
using Data.Repositories;
using Data.Searches;
using Models;
using SpeedyDonkeyApi.Controllers;
using SpeedyDonkeyApi.Models;

namespace SpeedyDonkeyApi.Tests.Builders
{
    public class ExamApiControllerBuilder
    {
        private IExamRepository _examRepository;
        private IActionHandlerOverlord _actionHandlerOverlord;
        private IModelFactory _modelFactory;
        private IEntitySearch<Exam> _entitySearch;

        public ExamApiControllerBuilder WithExamRepository(IExamRepository examRepository)
        {
            _examRepository = examRepository;
            return this;
        }

        public ExamApiControllerBuilder WithActionHandlerOverlord(IActionHandlerOverlord actionHandlerOverlord)
        {
            _actionHandlerOverlord = actionHandlerOverlord;
            return this;
        }

        public ExamApiControllerBuilder WithModelFactory(IModelFactory modelFactory)
        {
            _modelFactory = modelFactory;
            return this;
        }

        public ExamApiController Build()
        {
            return new ExamApiController(_examRepository, _actionHandlerOverlord, _modelFactory, _entitySearch);
        }

        public ExamApiControllerBuilder WithEntitySearch(IEntitySearch<Exam> entitySearch)
        {
            _entitySearch = entitySearch;
            return this;
        }
    }
}