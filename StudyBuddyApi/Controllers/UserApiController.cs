using System.Net.Http;
using System.Web.Http;
using Action;
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
    public class UserApiController : GenericApiController<UserModel, User>
    {
        public UserApiController(
            IActionHandlerOverlord actionHandlerOverlord,
            IRepository<User> repository,
            IEntitySearch<User> entitySearch)
            : base(actionHandlerOverlord, repository, entitySearch) { }

        [AllowAnonymous]
        public HttpResponseMessage Post([FromBody] UserModel model)
        {
            return PerformAction(model, x => new CreateUser(x));
        }

        [ClaimsAuthorise(Claim = Claim.Teacher)]
        public override HttpResponseMessage Get()
        {
            return base.Get();
        }

        [ClaimsAuthorise(Claim = Claim.Teacher)]
        public override HttpResponseMessage Get(string q)
        {
            return base.Get(q);
        }

        [ClaimsAuthorise(Claim = Claim.Teacher)]
        public HttpResponseMessage Delete(int id)
        {
            var model = new UserModel
            {
                Id = id
            };

            return PerformAction(model, x => new DeleteUser(x));
        }
    }
}
