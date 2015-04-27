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
using SpeedyDonkeyApi.Services;

namespace SpeedyDonkeyApi.Controllers
{
    public class TeacherApiController : GenericApiController<TeacherModel, Teacher>
    {
        public TeacherApiController(
            IActionHandlerOverlord actionHandlerOverlord, 
            IUrlConstructor urlConstructor,
            IRepository<Teacher> repository,
            ICommonInterfaceCloner cloner,
            IEntitySearch<Teacher> entitySearch)
            : base(actionHandlerOverlord, urlConstructor, repository, cloner, entitySearch) { }

        [ClaimsAuthorise(Claim = Claim.Admin)]
        public HttpResponseMessage Post(int id)
        {
            var model = new TeacherModel{Id = id};
            return PerformAction(model, x => new SetAsTeacher(x));
        }

        [ClaimsAuthorise(Claim = Claim.Admin)]
        public HttpResponseMessage Delete(int id)
        {
            var model = new TeacherModel { Id = id };
            return PerformAction(model, x => new RemoveAsTeacher(x));
        }


    }
}
