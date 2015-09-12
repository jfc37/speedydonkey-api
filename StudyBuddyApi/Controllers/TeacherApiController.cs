using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ActionHandlers;
using Actions;
using Common.Extensions;
using Data.Repositories;
using Data.Searches;
using Models;
using SpeedyDonkeyApi.Filter;
using SpeedyDonkeyApi.Models;

namespace SpeedyDonkeyApi.Controllers
{
    [RoutePrefix("api/teachers")]
    public class TeacherApiController : GenericApiController<Teacher>
    {
        public TeacherApiController(
            IActionHandlerOverlord actionHandlerOverlord,
            IRepository<Teacher> repository,
            IEntitySearch<Teacher> entitySearch)
            : base(actionHandlerOverlord, repository, entitySearch) { }

        [Route("{id}")]
        [ClaimsAuthorise(Claim = Claim.Admin)]
        public HttpResponseMessage Post(int id)
        {
            var model = new Teacher
            {
                User = new User
                {
                    Id = id
                }
            };
            var result = PerformAction<SetAsTeacher, Teacher>(new SetAsTeacher(model));

            return Request.CreateResponse(result.ValidationResult.GetStatusCode(HttpStatusCode.Created),
                new ActionReponse<TeacherModel>(result.ActionResult.ToModel(), result.ValidationResult));
        }

        [Route("{id}")]
        [ClaimsAuthorise(Claim = Claim.Admin)]
        public HttpResponseMessage Delete(int id)
        {
            var model = new Teacher { Id = id };
            var result = PerformAction<RemoveAsTeacher, Teacher>(new RemoveAsTeacher(model));

            return Request.CreateResponse(result.ValidationResult.GetStatusCode(HttpStatusCode.OK),
                new ActionReponse<TeacherModel>(result.ActionResult.ToModel(), result.ValidationResult));
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
        public IHttpActionResult Get(int id)
        {
            var result = GetById(id);

            return result.IsNotNull()
                ? (IHttpActionResult) Ok(result.ToModel())
                : NotFound();
        }
    }
}
