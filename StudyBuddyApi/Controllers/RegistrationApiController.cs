﻿using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ActionHandlers;
using Actions;
using Common;
using Data.Repositories;
using Data.Searches;
using Models;
using SpeedyDonkeyApi.Models;
using SpeedyDonkeyApi.Services;

namespace SpeedyDonkeyApi.Controllers
{
    public class RegistrationApiController : GenericApiController<RegistrationModel, Registration>
    {
        public RegistrationApiController(
            IActionHandlerOverlord actionHandlerOverlord, 
            IUrlConstructor urlConstructor,
            IRepository<Registration> repository,
            ICommonInterfaceCloner cloner,
            IEntitySearch<Registration> entitySearch)
            : base(actionHandlerOverlord, urlConstructor, repository, cloner, entitySearch) { }

        [AllowAnonymous]
        public HttpResponseMessage Post([FromBody] RegistrationModel model)
        {
            return PerformAction(model, x => new CreateRegistration(x));
        }

        [AllowAnonymous]
        public HttpResponseMessage Get(Guid registrationNumber)
        {
            var entity = _repository
                .GetAll()
                .SingleOrDefault(x => x.RegistationId == registrationNumber);

            if (entity == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            var model = new RegistrationModel().CloneFromEntity(Request, _urlConstructor, entity, _cloner);
            return Request.CreateResponse(HttpStatusCode.OK, model);
        }
    }
}