using System.Web.Http;
using Action;
using ActionHandlers;
using Common;
using Common.Extensions;
using Contracts;
using Contracts.MappingExtensions;
using Contracts.Passes;
using Contracts.Users;
using Data.Repositories;
using Models;
using SpeedyDonkeyApi.CodeChunks;
using SpeedyDonkeyApi.Filter;

namespace SpeedyDonkeyApi.Controllers.Users
{
    [RoutePrefix("api/users")]
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

        [Route("current/passes")]
        public IHttpActionResult Get()
        {
            return Get(_currentUser.Id);
        }

        [Route("{id:int}/passes")]
        [ClaimsAuthorise(Claim = Claim.Teacher)]
        public IHttpActionResult Get(int id)
        {
            return new EntityToHttpActionResult<User>(this, _entityRepository.Get(id), x => new CurrentUserPassesModel().ConvertFromEntity(x)).Do();
        }

        [Route("current/pass-templates/{passTemplateId:int}")]
        public IHttpActionResult Post(int passTemplateId, [FromBody]PassModel pass)
        {
            return Post(_currentUser.Id, passTemplateId, pass);
        }

        [Route("{userId:int}/pass-templates/{passTemplateId:int}")]
        public IHttpActionResult Post(int userId, int passTemplateId, [FromBody]PassModel pass)
        {
            var userModel = new UserModel
            {
                Id = userId,
                Passes = pass.PutIntoList()
            };
            var user = userModel.ToEntity();

            var result = PerformAction<PurchasePass, User>(new PurchasePass(user) { PassTemplateId = passTemplateId });

            return new ActionResultToCreatedHttpActionResult<User, UserModel>(result, x => x.ToModel(), this)
                .Do();
        }
    }
}