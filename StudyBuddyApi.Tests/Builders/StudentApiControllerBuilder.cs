using ActionHandlers;
using Data.Repositories;
using Data.Searches;
using Models;
using SpeedyDonkeyApi.Controllers;
using SpeedyDonkeyApi.Models;

namespace SpeedyDonkeyApi.Tests.Builders
{
    public class StudentApiControllerBuilder
    {
        private IPersonRepository<Student> _studentRepository;
        private IActionHandlerOverlord _actionHandlerOverlord;
        private IModelFactory _modelFactory;
        private IEntitySearch<Student> _entitySearch;

        public StudentApiController BuildStudentApiController()
        {
            return new StudentApiController(_studentRepository, _actionHandlerOverlord, _modelFactory, _entitySearch);
        }

        public StudentApiControllerBuilder WithRepository(IPersonRepository<Student> studentRepository)
        {
            _studentRepository = studentRepository;
            return this;
        }

        public StudentApiControllerBuilder WithActionHandlerOverlord(IActionHandlerOverlord actionHandlerOverlord)
        {
            _actionHandlerOverlord = actionHandlerOverlord;
            return this;
        }

        public StudentApiControllerBuilder WithModelFactory(IModelFactory modelFactory)
        {
            _modelFactory = modelFactory;
            return this;
        }
    }
}