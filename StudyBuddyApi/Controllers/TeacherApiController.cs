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
    public class TeacherApiController : GenericApiController<UserModel, User>
    {
        public TeacherApiController(
            IActionHandlerOverlord actionHandlerOverlord, 
            IUrlConstructor urlConstructor,
            IRepository<User> repository,
            ICommonInterfaceCloner cloner,
            IEntitySearch<User> entitySearch)
            : base(actionHandlerOverlord, urlConstructor, repository, cloner, entitySearch) { }

        [ClaimsAuthorise(Claim = Claim.Admin)]
        public HttpResponseMessage Post(int id)
        {
            var model = new UserModel{Id = id};
            return PerformAction(model, x => new SetAsTeacher(x));
        }

        [ClaimsAuthorise(Claim = Claim.Admin)]
        public override HttpResponseMessage Get()
        {
            var q = "claims_cont_teacher";
            return Get(q);
        }

        [ClaimsAuthorise(Claim = Claim.Admin)]
        public HttpResponseMessage Delete(int id)
        {
            var model = new UserModel { Id = id };
            return PerformAction(model, x => new RemoveAsTeacher(x));
        }


    }
}
