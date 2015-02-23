using ActionHandlers;
using Data.Repositories;
using Data.Searches;
using Models;
using SpeedyDonkeyApi.Controllers;
using SpeedyDonkeyApi.Models;

namespace SpeedyDonkeyApi.Tests.Builders
{
    public class AssignmentApiControllerBuilder
    {
        private IAssignmentRepository _assignmentRepository;
        private IActionHandlerOverlord _actionHandlerOverlord;
        private IModelFactory _modelFactory;
        private IEntitySearch<Assignment> _entitySearch;

        public AssignmentApiControllerBuilder WithAssignmentRepository(IAssignmentRepository assignmentRepository)
        {
            _assignmentRepository = assignmentRepository;
            return this;
        }

        public AssignmentApiControllerBuilder WithActionHandlerOverlord(IActionHandlerOverlord actionHandlerOverlord)
        {
            _actionHandlerOverlord = actionHandlerOverlord;
            return this;
        }

        public AssignmentApiControllerBuilder WithModelFactory(IModelFactory modelFactory)
        {
            _modelFactory = modelFactory;
            return this;
        }

        public AssignmentApiController Build()
        {
            return new AssignmentApiController(_assignmentRepository, _actionHandlerOverlord, _modelFactory, _entitySearch);
        }

        public AssignmentApiControllerBuilder WithEntitySearch(IEntitySearch<Assignment> entitySearch)
        {
            _entitySearch = entitySearch;
            return this;
        }
    }
}