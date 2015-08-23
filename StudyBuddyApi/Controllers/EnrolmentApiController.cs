using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Action;
using ActionHandlers;
using Common;
using Data.Repositories;
using Data.Searches;
using Models;
using SpeedyDonkeyApi.Filter;
using SpeedyDonkeyApi.Models;

namespace SpeedyDonkeyApi.Controllers
{
    [BasicAuthAuthorise]
    public class EnrolmentApiController : GenericApiController<UserModel, User>
    {
        public EnrolmentApiController(IActionHandlerOverlord actionHandlerOverlord, IRepository<User> repository, IEntitySearch<User> entitySearch) : base(actionHandlerOverlord, repository, entitySearch)
        {
        }

        public HttpResponseMessage Post(int id, [FromBody] EnrolmentModel model)
        {
            var user = new UserModel
            {
                Id = id
            };
            if (model.BlockIds != null)
                user.EnroledBlocks = model.BlockIds.Select(x => (IBlock) new BlockModel {Id = x}).ToList();
            if (model.PassTypes != null)
            {
                user.Passes = model.PassTypes.Select(x => (IPass)new PassModel { PassType = x }).ToList();
                foreach (var pass in user.Passes)
                {
                    pass.PaymentStatus = PassPaymentStatus.Pending.ToString();
                }
            }
            return PerformAction(user, x => new EnrolInBlock(x));
        }
    }
}