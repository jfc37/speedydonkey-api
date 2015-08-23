using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Common;
using Data.Repositories;
using Models;
using SpeedyDonkeyApi.Filter;
using SpeedyDonkeyApi.Models;

namespace SpeedyDonkeyApi.Controllers
{
    public class UserScheduleApiController : BaseApiController
    {
        private readonly IAdvancedRepository<User, IList<IEvent>> _userRepository;
        private readonly ICurrentUser _currentUser;

        public UserScheduleApiController(
            IAdvancedRepository<User, IList<IEvent>> userRepository,
            ICurrentUser currentUser)
        {
            _userRepository = userRepository;
            _currentUser = currentUser;
        }

        [ActiveUserRequired]
        public HttpResponseMessage Get()
        {
            return Get(_currentUser.Id);
        }

        [ClaimsAuthorise(Claim = Claim.Teacher)]
        public HttpResponseMessage Get(int id)
        {
            var userSchedule = _userRepository.Get(id);

            if (userSchedule == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            var today = DateTime.Now.Date;
            var nextWeek = today.AddDays(7);
            var thisWeeksSchedule = userSchedule
                .Where(x => x.StartTime > today && x.StartTime < nextWeek)
                .ToList();

            return thisWeeksSchedule.Any()
                ? Request.CreateResponse(thisWeeksSchedule.OfType<Class>().Select(x => new ClassModel().CloneFromEntity(Request, x)))
                : Request.CreateResponse(HttpStatusCode.NotFound);
        }
    }
}