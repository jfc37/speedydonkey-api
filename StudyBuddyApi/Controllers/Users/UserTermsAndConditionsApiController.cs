using System.Web.Http;
using Action.Users;
using ActionHandlers;
using Common;
using Contracts.MappingExtensions;
using Contracts.Users;
using Models;
using SpeedyDonkeyApi.CodeChunks;

namespace SpeedyDonkeyApi.Controllers.Users
{
    /// <summary>
    /// Api for user terms and conditions
    /// </summary>
    /// <seealso cref="BaseApiController" />
    [RoutePrefix("api/users")]
    public class UserTermsAndConditionsApiController : BaseApiController
    {
        private readonly ICurrentUser _currentUser;
        private readonly IActionHandlerOverlord _actionHandlerOverlord;

        public UserTermsAndConditionsApiController(
            ICurrentUser currentUser,
            IActionHandlerOverlord actionHandlerOverlord)
        {
            _currentUser = currentUser;
            _actionHandlerOverlord = actionHandlerOverlord;
        }

        /// <summary>
        /// User says they agree to the terms and conditions
        /// </summary>
        /// <returns></returns>
        [Route("current/terms-and-conditions")]
        public IHttpActionResult Post()
        {
            var user = new User(_currentUser.Id);

            var updateUser = new AgreeToTermsAndConditions(user);
            var result = _actionHandlerOverlord.HandleAction<AgreeToTermsAndConditions, User>(updateUser);

            return new ActionResultToOkHttpActionResult<User, UserModel>(result, x => x.ToModel(), this)
                .Do();
        }
    }
}