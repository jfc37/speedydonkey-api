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
    public class AccountApiController : GenericController<AccountModel, Account>
    {
        public AccountApiController(
            IActionHandlerOverlord actionHandlerOverlord,
            IUrlConstructor urlConstructor,
            IRepository<Account> repository) : base(actionHandlerOverlord, urlConstructor, repository) { }

        public HttpResponseMessage Post([FromBody] AccountModel model)
        {
            return Post(model, x => new CreateAccount(x));
        }
    }
}
