using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Common;
using Data.Repositories;
using Models;
using SpeedyDonkeyApi.Models;
using SpeedyDonkeyApi.Services;

namespace SpeedyDonkeyApi.Controllers
{
    public class UserEnroledBlocksApiController : ApiController
    {
        private readonly IRepository<User> _userRepository;
        private readonly IUrlConstructor _urlConstructor;
        private readonly ICommonInterfaceCloner _cloner;

        public UserEnroledBlocksApiController(
            IRepository<User> userRepository,
            IUrlConstructor urlConstructor,
            ICommonInterfaceCloner cloner)
        {
            _userRepository = userRepository;
            _urlConstructor = urlConstructor;
            _cloner = cloner;
        }

        public HttpResponseMessage Get(int id)
        {
            var user = _userRepository.Get(id);
            var userEnroledBlocks = new UserEnroledBlocksModel().ConvertFromUser(user, Request, _urlConstructor, _cloner);
            return userEnroledBlocks.Any()
                ? Request.CreateResponse(userEnroledBlocks)
                : Request.CreateResponse(HttpStatusCode.NotFound);
        }
    }
}