using System.Linq;
using System.Web.Http;
using Action;
using Action.StandAloneEvents;
using ActionHandlers;
using Common;
using Contracts;
using Contracts.Blocks;
using Contracts.Enrolment;
using Contracts.Events;
using Contracts.MappingExtensions;
using Contracts.Users;
using Data.Repositories;
using Data.Searches;
using Models;
using SpeedyDonkeyApi.CodeChunks;
using System.Collections.Generic;

namespace SpeedyDonkeyApi.Controllers.Users
{
    [RoutePrefix("api/users")]
    public class EnrolmentApiController : GenericApiController<User>
    {
        private readonly ICurrentUser _currentUser;

        public EnrolmentApiController(
            IActionHandlerOverlord actionHandlerOverlord, 
            IRepository<User> repository, 
            IEntitySearch<User> entitySearch,
            ICurrentUser currentUser) 
            : base(actionHandlerOverlord, repository, entitySearch)
        {
            _currentUser = currentUser;
        }

        [Route("current/enrolment")]
        public IHttpActionResult Post([FromBody] EnrolmentModel model)
        {
            return Post(_currentUser.Id, model);
        }

        [Route("{id:int}/enrolment")]
        public IHttpActionResult Post(int id, [FromBody] EnrolmentModel model)
        {
            var user = new UserModel(id) { EnroledBlocks = model.BlockIds.Select(x => new BlockModel(x)).ToList() };

            var result = PerformAction<EnrolInBlock, User>(new EnrolInBlock(user.ToEntity()));

            return new ActionResultToOkHttpActionResult<User, UserModel>(result, x => x.ToModel(), this)
                .Do();
        }

        [Route("{id:int}/enrolment/{blockId:int}")]
        public IHttpActionResult Delete(int id, int blockId)
        {
            var user = new UserModel(id) { EnroledBlocks = new List<BlockModel> { new BlockModel(blockId) } };

            var result = PerformAction<UnenrolInBlock, User>(new UnenrolInBlock(user.ToEntity()));

            return new ActionResultToOkHttpActionResult<User, UserModel>(result, x => x.ToModel(), this)
                .Do();
        }

        [Route("current/registration/event")]
        public IHttpActionResult Post([FromBody] EventRegistrationModel model)
        {
            return Post(_currentUser.Id, model);
        }

        [Route("{id:int}/registration/event")]
        public IHttpActionResult Post(int id, [FromBody] EventRegistrationModel model)
        {
            var user = new User(id) { Schedule = model.EventIds.Select(x => new StandAloneEvent(x)).ToList<Event>() };

            var result = PerformAction<RegisterForStandAloneEvent, User>(new RegisterForStandAloneEvent(user));

            return new ActionResultToOkHttpActionResult<User, UserModel>(result, x => x.ToModel(), this)
                .Do();
        }
    }
}