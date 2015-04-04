using System.Net.Http;
using Common;
using Data.Repositories;
using Models;
using SpeedyDonkeyApi.Filter;
using SpeedyDonkeyApi.Models;
using SpeedyDonkeyApi.Services;

namespace SpeedyDonkeyApi.Controllers
{
    public class CurrentUserApiController : BaseApiController
    {
        private readonly IRepository<User> _repository;
        private readonly ICurrentUser _currentUser;
        private readonly IUrlConstructor _urlConstructor;
        private readonly ICommonInterfaceCloner _cloner;

        public CurrentUserApiController(
            IRepository<User> repository, 
            ICurrentUser currentUser,
            IUrlConstructor urlConstructor,
            ICommonInterfaceCloner cloner)
        {
            _repository = repository;
            _currentUser = currentUser;
            _urlConstructor = urlConstructor;
            _cloner = cloner;
        }

        [ActiveUserRequired]
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(new UserModel().CloneFromEntity(Request, _urlConstructor, _repository.Get(_currentUser.Id), _cloner));
        }
    }
}