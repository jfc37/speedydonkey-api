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
    public class UserPassesApiController : UserPropertyApiController<CurrentUserPassesModel, PassModel>
    {
        public UserPassesApiController(
            IRepository<User> userRepository, 
            IUrlConstructor urlConstructor, 
            ICommonInterfaceCloner cloner) : base(userRepository, urlConstructor, cloner)
        {
        }
    }
    public class UserEnroledBlocksApiController : UserPropertyApiController<UserEnroledBlocksModel, BlockModel>
    {
        public UserEnroledBlocksApiController(
            IRepository<User> userRepository, 
            IUrlConstructor urlConstructor, 
            ICommonInterfaceCloner cloner) : base(userRepository, urlConstructor, cloner)
        {
        }
    }

    public abstract class UserPropertyApiController<TViewModel, TModel> : ApiController where TViewModel : IUserView<TModel>, new()
    {
        private readonly IRepository<User> _userRepository;
        private readonly IUrlConstructor _urlConstructor;
        private readonly ICommonInterfaceCloner _cloner;

        protected UserPropertyApiController(
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
            if (user == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            var userScheduleModels = new TViewModel().ConvertFromUser(user, Request, _urlConstructor, _cloner);
            return userScheduleModels.Any()
                ? Request.CreateResponse(userScheduleModels)
                : Request.CreateResponse(HttpStatusCode.NotFound);
        }
    }
}