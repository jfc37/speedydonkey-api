using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Action;
using ActionHandlers;
using Actions;
using Data.Repositories;
using Data.Searches;
using Models;
using SpeedyDonkeyApi.Filter;
using SpeedyDonkeyApi.Models;

namespace SpeedyDonkeyApi.Controllers
{
    [RoutePrefix("api/users")]
    public class UserApiController : GenericApiController<User>
    {
        public UserApiController(
            IActionHandlerOverlord actionHandlerOverlord,
            IRepository<User> repository,
            IEntitySearch<User> entitySearch)
            : base(actionHandlerOverlord, repository, entitySearch) { }

        [Route]
        [AllowAnonymous]
        public HttpResponseMessage Post([FromBody] UserModel model)
        {
            var result = PerformAction<CreateUser, User>(new CreateUser(model.ToEntity()));

            return Request.CreateResponse(result.ValidationResult.GetStatusCode(HttpStatusCode.Created),
                new ActionReponse<UserModel>(result.ActionResult.ToModel(), result.ValidationResult));
        }

        [Route]
        [ClaimsAuthorise(Claim = Claim.Teacher)]
        public IHttpActionResult Get()
        {
            var all = GetAll().ToList();

            return all.Any()
                ? (IHttpActionResult) Ok(all.Select(x => x.ToModel()))
                : NotFound();
        }

        [Route]
        [ClaimsAuthorise(Claim = Claim.Teacher)]
        public IHttpActionResult Get(string q)
        {
            var all = Search(q).ToList();

            return all.Any()
                ? (IHttpActionResult)Ok(all.Select(x => x.ToModel()))
                : NotFound();
        }

        [Route("{id:int}")]
        [ClaimsAuthorise(Claim = Claim.Teacher)]
        public HttpResponseMessage Delete(int id)
        {
            var model = new User
            {
                Id = id
            };

            var result = PerformAction<DeleteUser, User>(new DeleteUser(model));

            return Request.CreateResponse(result.ValidationResult.GetStatusCode(HttpStatusCode.OK),
                new ActionReponse<UserModel>(result.ActionResult.ToModel(), result.ValidationResult));
        }
    }
}
