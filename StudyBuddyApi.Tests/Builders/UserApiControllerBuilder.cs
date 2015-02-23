using ActionHandlers;
using Data.Repositories;
using Data.Searches;
using Models;
using SpeedyDonkeyApi.Controllers;
using SpeedyDonkeyApi.Models;

namespace SpeedyDonkeyApi.Tests.Builders
{
    public class UserApiControllerBuilder
    {
        private IUserRepository _userRepository;
        private IActionHandlerOverlord _actionHandlerOverlord;
        private IModelFactory _modelFactory;
        private IEntitySearch<User> _userEntitySearch;

        public UserApiController Build()
        {
            return new UserApiController(_userRepository, _actionHandlerOverlord, _modelFactory, _userEntitySearch);
        }

        public UserApiControllerBuilder WithUserRepository(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            return this;
        }

        public UserApiControllerBuilder WithActionHandlerOverlord(IActionHandlerOverlord actionHandlerOverlord)
        {
            _actionHandlerOverlord = actionHandlerOverlord;
            return this;
        }

        public UserApiControllerBuilder WithModelFactory(IModelFactory modelFactory)
        {
            _modelFactory = modelFactory;
            return this;
        }

        public UserApiControllerBuilder WithUserSearch(IEntitySearch<User> userEntitySearch)
        {
            _userEntitySearch = userEntitySearch;
            return this;
        }
    }
}
