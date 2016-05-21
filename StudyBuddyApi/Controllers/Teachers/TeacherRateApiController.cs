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
    [RoutePrefix("api/teachers")]
    public class TeacherRateApiController : GenericApiController<Teacher>
    {
        public TeacherRateApiController(
            IActionHandlerOverlord actionHandlerOverlord, 
            IRepository<Teacher> repository, 
            IEntitySearch<Teacher> entitySearch) 
            : base(actionHandlerOverlord, repository, entitySearch)
        {
        }

        [Route("{id}/rates")]
        [ClaimsAuthorise(Claim = Claim.Admin)]
        public IHttpActionResult Post(int id, [FromBody] TeacherRateModel model)
        {
            var teacher = new Teacher(id);
            teacher.Rate = model.ToEntity();

            var result = PerformAction<UpdateTeacherRate, Teacher>(new UpdateTeacherRate(teacher));

            return new ActionResultToCreatedHttpActionResult<Teacher, TeacherModel>(result, x => x.ToModel(), this)
                .Do();
        }

        [Route("{id}/rates")]
        [ClaimsAuthorise(Claim = Claim.Admin)]
        public IHttpActionResult Get(int id)
        {
            var teacher = GetById(id);
            
            var rate = GetById(id)
                .Select(x => x.Rate)
                .ConvertSetToOption();
            return new EntityToHttpActionResult<TeacherRate>(this, rate, x => x.ToModel()).Do();
        }
    }
}