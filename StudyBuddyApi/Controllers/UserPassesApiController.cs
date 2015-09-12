using System.Net;
using System.Net.Http;
using System.Web.Http;
using Action;
using ActionHandlers;
using Common;
using Common.Extensions;
using Data.Repositories;
using Models;
using SpeedyDonkeyApi.Filter;
using SpeedyDonkeyApi.Models;

namespace SpeedyDonkeyApi.Controllers
{
    public class UserPassesApiController : EntityPropertyApiController
    {
        private readonly IRepository<User> _entityRepository;
        private readonly ICurrentUser _currentUser;

        public UserPassesApiController(
            IRepository<User> entityRepository,
            IActionHandlerOverlord actionHandlerOverlord,
            ICurrentUser currentUser)
            : base(actionHandlerOverlord)
        {
            _entityRepository = entityRepository;
            _currentUser = currentUser;
        }

        [ActiveUserRequired]
        public IHttpActionResult Get()
        {
            return Get(_currentUser.Id);
        }

        [ClaimsAuthorise(Claim = Claim.Teacher)]
        public IHttpActionResult Get(int id)
        {
            var entity = _entityRepository.Get(id);
            return entity.IsNotNull()
                ? (IHttpActionResult) Ok(new CurrentUserPassesModel().ConvertFromEntity(entity))
                : NotFound();
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
                Passes = pass.PutIntoList()
            };
            var user = userModel.ToEntity();
            var result = PerformAction<PurchasePass, User>(new PurchasePass(user) { PassTemplateId = passTemplateId });

            return Request.CreateResponse(result.ValidationResult.GetStatusCode(HttpStatusCode.Created),
                new ActionReponse<UserModel>(result.ActionResult.ToModel(), result.ValidationResult));
        }
    }
}