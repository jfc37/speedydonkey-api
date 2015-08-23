using System.Net.Http;
using System.Web.Http;
using ActionHandlers;
using Actions;
using Common;
using Data.Repositories;
using Data.Searches;
using Models;
using SpeedyDonkeyApi.Filter;
using SpeedyDonkeyApi.Models;

namespace SpeedyDonkeyApi.Controllers
{
    [RoutePrefix("api/teachers")]
    public class TeacherApiController : GenericApiController<TeacherModel, Teacher>
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
            var model = new UserModel{Id = id};
            return PerformAction<SetAsTeacher, UserModel, User>(model, x => new SetAsTeacher(x));
        }

        [Route("{id}")]
        [ClaimsAuthorise(Claim = Claim.Admin)]
        public HttpResponseMessage Delete(int id)
        {
            var model = new TeacherModel { Id = id };
            return PerformAction(model, x => new RemoveAsTeacher(x));
        }

        [Route]
        [ClaimsAuthorise(Claim = Claim.Teacher)]
        public override HttpResponseMessage Get()
        {
            return base.Get();
        }

        [Route]
        [ClaimsAuthorise(Claim = Claim.Teacher)]
        public override HttpResponseMessage Get(string q)
        {
            return base.Get(q);
        }

        [Route("{id:int}")]
        [ClaimsAuthorise(Claim = Claim.Teacher)]
        public override HttpResponseMessage Get(int id)
        {
            return base.Get(id);
        }
    }
}
