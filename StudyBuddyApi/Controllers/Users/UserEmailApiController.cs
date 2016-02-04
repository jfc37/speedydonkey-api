using System.Web.Http;
using Action.Users;
using ActionHandlers;
using Common;
using Models;
using SpeedyDonkeyApi.CodeChunks;
using SpeedyDonkeyApi.Filter;
using SpeedyDonkeyApi.Models;

namespace SpeedyDonkeyApi.Controllers.Users
{
    /// <summary>
    /// Api for user email rules
    /// </summary>
    /// <seealso cref="SpeedyDonkeyApi.Controllers.BaseApiController" />
    [RoutePrefix("api/users")]
    public class UserEmailApiController : BaseApiController
    {
        private readonly ICurrentUser _currentUser;
        private readonly IActionHandlerOverlord _actionHandlerOverlord;

        public UserEmailApiController(
            ICurrentUser currentUser,
            IActionHandlerOverlord actionHandlerOverlord)
        {
            _currentUser = currentUser;
            _actionHandlerOverlord = actionHandlerOverlord;
        }

        /// <summary>
        /// User says they don't want to receive emails
        /// </summary>
        /// <returns></returns>
        [Route("current/do-not-email")]
        public IHttpActionResult Post()
        {
            return Post(_currentUser.Id);
        }

        /// <summary>
        /// Admin says not to send emails to user
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        [ClaimsAuthorise(Claim = Claim.Admin)]
        [Route("{userId}/do-not-email")]
        public IHttpActionResult Post(int userId)
        {
            var user = new User(userId);

            var updateUser = new SetDoNotEmailForUser(user);
            var result = _actionHandlerOverlord.HandleAction<SetDoNotEmailForUser, User>(updateUser);

            return new ActionResultToOkHttpActionResult<User, UserModel>(result, x => x.ToModel(), this)
                .Do();
        }

        /// <summary>
        /// User says they want to receive emails
        /// </summary>
        /// <returns></returns>
        [Route("current/do-not-email")]
        public IHttpActionResult Delete()
        {
            return Delete(_currentUser.Id);
        }

        /// <summary>
        /// Admin says to send emails to user
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        [ClaimsAuthorise(Claim = Claim.Admin)]
        [Route("{userId}/do-not-email")]
        public IHttpActionResult Delete(int userId)
        {
            var user = new User(userId);

            var updateUser = new RemoveDoNotEmailForUser(user);
            var result = _actionHandlerOverlord.HandleAction<RemoveDoNotEmailForUser, User>(updateUser);

            return new ActionResultToOkHttpActionResult<User, UserModel>(result, x => x.ToModel(), this)
                .Do();
        }
    }
}