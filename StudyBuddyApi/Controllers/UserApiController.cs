using System.Net.Http;
using System.Web.Http;
using ActionHandlers;
using Actions;
using Common;
using Data.Repositories;
using Data.Searches;
using Models;
using SpeedyDonkeyApi.Models;
using SpeedyDonkeyApi.Services;

namespace SpeedyDonkeyApi.Controllers
{
    public class UserApiController : GenericApiController<UserModel, User>
    {
        public UserApiController(
            IActionHandlerOverlord actionHandlerOverlord, 
            IUrlConstructor urlConstructor,
            IRepository<User> repository,
            ICommonInterfaceCloner cloner,
            IEntitySearch<User> entitySearch)
            : base(actionHandlerOverlord, urlConstructor, repository, cloner, entitySearch) { }

        public HttpResponseMessage Post([FromBody] UserModel model)
        {
            return PerformAction(model, x => new CreateUser(x));
        }
    }
}
