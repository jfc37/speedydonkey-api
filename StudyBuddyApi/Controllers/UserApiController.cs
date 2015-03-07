using System.Net.Http;
using System.Web.Http;
using ActionHandlers;
using Actions;
using Data.Repositories;
using Models;
using SpeedyDonkeyApi.Models;
using SpeedyDonkeyApi.Services;

namespace SpeedyDonkeyApi.Controllers
{
    public class UserApiController : GenericController<UserModel, User>
    {
        public UserApiController(
            IActionHandlerOverlord actionHandlerOverlord, 
            IUrlConstructor urlConstructor, 
            IRepository<User> repository) : base(actionHandlerOverlord, urlConstructor, repository) { }

        public HttpResponseMessage Post(int accountId, [FromBody] UserModel model)
        {
            model.Account.Id = accountId;
            return Post(model, x => new CreateUser(x));
        }
    }
}
