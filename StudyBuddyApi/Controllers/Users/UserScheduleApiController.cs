using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Common;
using Common.Extensions;
using Contracts;
using Contracts.MappingExtensions;
using Data.Repositories;
using Models;
using SpeedyDonkeyApi.CodeChunks;
using SpeedyDonkeyApi.Filter;

namespace SpeedyDonkeyApi.Controllers.Users
{
    public class UserScheduleApiController : BaseApiController
    {
        private readonly IAdvancedRepository<User, IList<Event>> _userRepository;
        private readonly ICurrentUser _currentUser;

        public UserScheduleApiController(
            IAdvancedRepository<User, IList<Event>> userRepository,
            ICurrentUser currentUser)
        {
            _userRepository = userRepository;
            _currentUser = currentUser;
        }

        [Route("api/users/current/schedules")]
        public IHttpActionResult Get()
        {
            return Get(_currentUser.Id);
        }

        [Route("api/users/{id:int}/schedules")]
        [ClaimsAuthorise(Claim = Claim.Teacher)]
        public IHttpActionResult Get(int id)
        {
            var userSchedule = _userRepository.Get(id);

            if (userSchedule.IsNull())
            {
                return NotFound();
            }

            var upcomingSchedule = new GetUpcomingSchedule(userSchedule)
                .Do()
                .ToList();

            return upcomingSchedule.Any()
                ? (IHttpActionResult)Ok(upcomingSchedule.Select(x => x.ToModel()))
                : NotFound();
        }
    }
}