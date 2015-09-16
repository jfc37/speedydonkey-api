using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ActionHandlers;
using Actions;
using Data.CodeChunks;
using Data.Repositories;
using Data.Searches;
using Models;
using SpeedyDonkeyApi.Filter;
using SpeedyDonkeyApi.Models;

namespace SpeedyDonkeyApi.Controllers
{
    public class RegistrationApiController : GenericApiController<Registration>
    {
        public RegistrationApiController(
            IActionHandlerOverlord actionHandlerOverlord,
            IRepository<Registration> repository,
            IEntitySearch<Registration> entitySearch)
            : base(actionHandlerOverlord, repository, entitySearch) { }

        [AllowAnonymous]
        public HttpResponseMessage Post([FromBody] RegistrationModel model)
        {
            var result = PerformAction<CreateRegistration, Registration>(new CreateRegistration(model.ToEntity()));

            return Request.CreateResponse(result.ValidationResult.GetStatusCode(HttpStatusCode.Created),
                new ActionReponse<RegistrationModel>(result.ActionResult.ToModel(), result.ValidationResult));
        }

        [AllowAnonymous]
        public HttpResponseMessage Get(Guid registrationNumber)
        {
            var entity = new GetRegistrationFromRegistrationNumber(Repository, registrationNumber)
                .Do();

            if (entity == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            return Request.CreateResponse(HttpStatusCode.OK, entity.ToModel());
        }

        [Route("api/windy-lindy/registrations")]
        [ClaimsAuthorise(Claim = Claim.Admin)]
        public IHttpActionResult Get()
        {
            var all = GetAll().ToList();
            return all.Any()
                ? (IHttpActionResult) NotFound()
                : Ok(all);
        }
    }
}
