using System.Web.Http;
using Action.Teachers;
using ActionHandlers;
using Actions;
using Contracts.MappingExtensions;
using Contracts.Teachers;
using Data.Repositories;
using Data.Searches;
using Models;
using SpeedyDonkeyApi.CodeChunks;
using SpeedyDonkeyApi.Filter;

namespace SpeedyDonkeyApi.Controllers.Teachers
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
        public IHttpActionResult Post(int id)
        {
            var model = new Teacher(new User(id));
            var result = PerformAction<SetAsTeacher, Teacher>(new SetAsTeacher(model));

            return new ActionResultToCreatedHttpActionResult<Teacher, TeacherModel>(result, x => x.ToModel(), this)
                .Do();
        }

        [Route("{id}")]
        [ClaimsAuthorise(Claim = Claim.Admin)]
        public IHttpActionResult Delete(int id)
        {
            var model = new Teacher(id);
            var result = PerformAction<RemoveAsTeacher, Teacher>(new RemoveAsTeacher(model));

            return new ActionResultToOkHttpActionResult<Teacher, TeacherModel>(result, x => x.ToModel(), this)
                .Do();
        }

        [Route]
        [ClaimsAuthorise(Claim = Claim.Teacher)]
        public IHttpActionResult Get()
        {
            return new SetToHttpActionResult<Teacher>(this, GetAll(), x => x.ToStripedModel()).Do();
        }

        [Route]
        [ClaimsAuthorise(Claim = Claim.Teacher)]
        public IHttpActionResult Get(string q)
        {
            return new SetToHttpActionResult<Teacher>(this, Search(q), x => x.ToModel()).Do();
        }

        [Route("{id:int}")]
        [ClaimsAuthorise(Claim = Claim.Teacher)]
        public IHttpActionResult Get(int id)
        {
            return new EntityToHttpActionResult<Teacher>(this, GetById(id), x => x.ToModel()).Do();
        }
    }
}
