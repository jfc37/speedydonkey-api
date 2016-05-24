using System.Linq;
using System.Web.Http;
using Action.Teachers;
using ActionHandlers;
using Common;
using Contracts.MappingExtensions;
using Contracts.Teachers;
using Data.Repositories;
using Data.Searches;
using Models;
using SpeedyDonkeyApi.CodeChunks;
using SpeedyDonkeyApi.Filter;

namespace SpeedyDonkeyApi.Controllers.Teachers
{
    [RoutePrefix("api/teacher-rates")]
    public class TeacherRateApiController : GenericApiController<Teacher>
    {
        public TeacherRateApiController(
            IActionHandlerOverlord actionHandlerOverlord, 
            IRepository<Teacher> repository, 
            IEntitySearch<Teacher> entitySearch) 
            : base(actionHandlerOverlord, repository, entitySearch)
        {
        }

        [Route("{id}")]
        [ClaimsAuthorise(Claim = Claim.Admin)]
        public IHttpActionResult Post(int id, [FromBody] TeacherRateModel model)
        {
            var teacher = new Teacher(id);
            teacher.Rate = model.ToEntity();

            var result = PerformAction<UpdateTeacherRate, Teacher>(new UpdateTeacherRate(teacher));

            return new ActionResultToCreatedHttpActionResult<Teacher, TeacherModel>(result, x => x.ToModel(), this)
                .Do();
        }

        [Route("{id}")]
        [ClaimsAuthorise(Claim = Claim.Admin)]
        public IHttpActionResult Get(int id)
        {
            var teacher = GetById(id);
            
            return new EntityToHttpActionResult<Teacher>(this, teacher, x => x.ToRateModel()).Do();
        }

        [Route]
        [ClaimsAuthorise(Claim = Claim.Admin)]
        public IHttpActionResult Get()
        {
            return new SetToHttpActionResult<Teacher>(this, GetAll(), x => x.ToRateModel()).Do();
        }
    }
}