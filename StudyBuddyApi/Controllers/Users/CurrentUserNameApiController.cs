using System.Web.Http;
using Action.Users;
using ActionHandlers;
using Common;
using Contracts;
using Contracts.Users;
using Data.Repositories;
using Models;
using SpeedyDonkeyApi.CodeChunks;

namespace SpeedyDonkeyApi.Controllers.Users
{
    /// <summary>
    /// Api for user names (first and last name)
    /// </summary>
    /// <seealso cref="SpeedyDonkeyApi.Controllers.BaseApiController" />
    [RoutePrefix("api/users/current/names")]
    public class CurrentUserNameApiController : BaseApiController
    {
        private readonly ICurrentUser _currentUser;
        private readonly IActionHandlerOverlord _actionHandlerOverlord;
        private readonly IRepository<User> _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CurrentUserNameApiController"/> class.
        /// </summary>
        /// <param name="currentUser">The current user.</param>
        /// <param name="actionHandlerOverlord">The action handler overlord.</param>
        /// <param name="repository">The repository.</param>
        public CurrentUserNameApiController(
            ICurrentUser currentUser,
            IActionHandlerOverlord actionHandlerOverlord,
            IRepository<User> repository)
        {
            _currentUser = currentUser;
            _actionHandlerOverlord = actionHandlerOverlord;
            _repository = repository;
        }

        /// <summary>
        /// Gets the current user's first and last name.
        /// </summary>
        /// <returns></returns>
        [Route]
        public IHttpActionResult Get()
        {
            var user = _repository.Get(_currentUser.Id);
            var userNames = new UserNamesModel(user.FirstName, user.Surname);

            return Ok(userNames);
        }

        /// <summary>
        /// Updates the current user's first and last name.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [Route]
        public IHttpActionResult Put([FromBody] UserNamesModel model)
        {
            var user = new User(_currentUser.Id)
            {
                FirstName = model.FirstName,
                Surname = model.Surname
            };

            var updateUser = new UpdateUserNames(user);
            var result = _actionHandlerOverlord.HandleAction<UpdateUserNames, User>(updateUser);

            return new ActionResultToOkHttpActionResult<User, UserModel>(result, x => x.ToModel(), this)
                .Do();
        }
    }
}