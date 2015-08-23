using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Action;
using ActionHandlers;
using Common;
using Data.Repositories;
using Models;
using SpeedyDonkeyApi.Filter;
using SpeedyDonkeyApi.Models;

namespace SpeedyDonkeyApi.Controllers
{
    public class UserPassesApiController : EntityPropertyApiController<CurrentUserPassesModel, PassModel, User>
    {
        private readonly ICurrentUser _currentUser;

        public UserPassesApiController(
            IRepository<User> entityRepository,
            IActionHandlerOverlord actionHandlerOverlord,
            ICurrentUser currentUser)
            : base(entityRepository, actionHandlerOverlord)
        {
            _currentUser = currentUser;
        }

        [ActiveUserRequired]
        public HttpResponseMessage Get()
        {
            return Get(_currentUser.Id);
        }

        [ClaimsAuthorise(Claim = Claim.Teacher)]
        public override HttpResponseMessage Get(int id)
        {
            return base.Get(id);
        }

        [ActiveUserRequired]
        public HttpResponseMessage Post(int passTemplateId, [FromBody]PassModel pass)
        {
            return Post(_currentUser.Id, passTemplateId, pass);
        }

        public HttpResponseMessage Post(int userId, int passTemplateId, [FromBody]PassModel pass)
        {
            var userModel = new UserModel
            {
                Id = userId,
                Passes = new List<IPass>{pass}
            };
            var user = userModel.ToEntity();
            return PerformAction<PurchasePass, UserModel, User>(userModel, new PurchasePass(user){ PassTemplateId = passTemplateId});
        }
    }
}