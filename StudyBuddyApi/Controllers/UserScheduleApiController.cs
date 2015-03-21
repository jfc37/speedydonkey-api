using System.Collections.Generic;
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
    public class UserScheduleApiController : ApiController
    {
        private readonly IAdvancedRepository<User, IList<IEvent>> _userRepository;
        private readonly IUrlConstructor _urlConstructor;
        private readonly ICommonInterfaceCloner _cloner;

        public UserScheduleApiController(
            IAdvancedRepository<User, IList<IEvent>> userRepository,
            IUrlConstructor urlConstructor,
            ICommonInterfaceCloner cloner)
        {
            _userRepository = userRepository;
            _urlConstructor = urlConstructor;
            _cloner = cloner;
        }

        public HttpResponseMessage Get(int id)
        {
            var userSchedule = _userRepository.Get(id);

            if (userSchedule == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return userSchedule.Any()
                ? Request.CreateResponse(userSchedule.OfType<Class>().Select(x => new ClassModel().CloneFromEntity(Request, _urlConstructor, x, _cloner)))
                : Request.CreateResponse(HttpStatusCode.NotFound);
        }
    }
}