using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Data.Repositories;
using Models;
using SpeedyDonkeyApi.Models;

namespace SpeedyDonkeyApi.Controllers
{
    public class UserScheduleApiController : ApiController
    {
        private readonly IRepository<User> _userRepository;

        public UserScheduleApiController(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public HttpResponseMessage Get(int id)
        {
            var user = _userRepository.Get(id);
            return Request.CreateResponse(new UserScheduleModel().ConvertFromUser(user));
        }
    }
}