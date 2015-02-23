using ActionHandlers;
using Data.Repositories;
using Data.Searches;
using Models;
using SpeedyDonkeyApi.Controllers;
using SpeedyDonkeyApi.Models;

namespace SpeedyDonkeyApi.Tests.Builders
{
    public class ProfessorApiControllerBuilder
    {
        private IPersonRepository<Professor> _professorRepository;
        private IActionHandlerOverlord _actionHandlerOverlord;
        private IModelFactory _modelFactory;
        private IEntitySearch<Professor> _entitySearch;

        public ProfessorApiController BuildProfessorApiController()
        {
            return new ProfessorApiController(_professorRepository, _actionHandlerOverlord, _modelFactory, _entitySearch);
        }

        public ProfessorApiControllerBuilder WithRepository(IPersonRepository<Professor> professorRepository)
        {
            _professorRepository = professorRepository;
            return this;
        }

        public ProfessorApiControllerBuilder WithActionHandlerOverlord(IActionHandlerOverlord actionHandlerOverlord)
        {
            _actionHandlerOverlord = actionHandlerOverlord;
            return this;
        }

        public ProfessorApiControllerBuilder WithModelFactory(IModelFactory modelFactory)
        {
            _modelFactory = modelFactory;
            return this;
        }

        public ProfessorApiControllerBuilder WithEntitySearch(IEntitySearch<Professor> entitySearch)
        {
            _entitySearch = entitySearch;
            return this;
        }
    }
}