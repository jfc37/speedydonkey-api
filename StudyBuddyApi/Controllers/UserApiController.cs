using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ActionHandlers;
using Actions;
using Data.Repositories;
using Data.Searches;
using Models;
using SpeedyDonkeyApi.Filter;
using SpeedyDonkeyApi.Models;

namespace SpeedyDonkeyApi.Controllers
{
    public class UserApiController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IActionHandlerOverlord _actionHandlerOverlord;
        private readonly IModelFactory _modelFactory;
        private readonly IEntitySearch<User> _userEntitySearch;

        public UserApiController(IUserRepository userRepository, IActionHandlerOverlord actionHandlerOverlord, IModelFactory modelFactory, IEntitySearch<User> userEntitySearch)
        {
            _userRepository = userRepository;
            _actionHandlerOverlord = actionHandlerOverlord;
            _modelFactory = modelFactory;
            _userEntitySearch = userEntitySearch;
        }

        [BasicAuthAuthorise]
        public HttpResponseMessage Get()
        {
            var allUsers = _userRepository.GetAll()
                .Select(x => _modelFactory.ToModel(Request, x))
                .ToList();

            return !allUsers.Any()
                ? Request.CreateResponse(HttpStatusCode.NotFound)
                : Request.CreateResponse(HttpStatusCode.OK, allUsers);
        }

        [BasicAuthAuthorise]
        public HttpResponseMessage Get(int userId)
        {
            User user = _userRepository.Get(userId);

            if (user == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            UserModel userModel = _modelFactory.ToModel(Request, user);
            return Request.CreateResponse(HttpStatusCode.OK, userModel);
        }

        [BasicAuthAuthorise]
        public HttpResponseMessage Get(string q)
        {
            var matchingUsers = _userEntitySearch.Search(q);
            if (!matchingUsers.Any())
                return Request.CreateResponse(HttpStatusCode.NotFound);

            return Request.CreateResponse(HttpStatusCode.OK,
                matchingUsers.Select(x => _modelFactory.ToModel(Request, x)).ToList());
        }

        public HttpResponseMessage Post([FromBody]UserModel userModel)
        {
            var user = _modelFactory.Parse(userModel);

            var createUser = new CreateUser(user);
            ActionReponse<User> result = _actionHandlerOverlord.HandleAction<CreateUser, User>(createUser);
            HttpStatusCode responseCode = result.ValidationResult.IsValid
                ? HttpStatusCode.Created
                : HttpStatusCode.BadRequest;
            return Request.CreateResponse(
                responseCode,
                new ActionReponse<UserModel>
                {
                    ActionResult = _modelFactory.ToModel(Request, result.ActionResult),
                    ValidationResult = result.ValidationResult
                });
        }

        [BasicAuthAuthorise]
        public HttpResponseMessage Put(int userId, [FromBody]UserModel userModel)
        {
            userModel.Id = userId;
            var user = _modelFactory.Parse(userModel);

            var updateUser = new UpdateUser(user);
            ActionReponse<User> result = _actionHandlerOverlord.HandleAction<UpdateUser, User>(updateUser);
            HttpStatusCode responseCode = result.ValidationResult.IsValid
                ? HttpStatusCode.OK
                : HttpStatusCode.BadRequest;
            return Request.CreateResponse(
                responseCode,
                new ActionReponse<UserModel>
                {
                    ActionResult = _modelFactory.ToModel(Request, result.ActionResult),
                    ValidationResult = result.ValidationResult
                });
        }

        [BasicAuthAuthorise]
        public HttpResponseMessage Delete(int userId)
        {
            HttpStatusCode responseCode;

            User user = _userRepository.Get(userId);
            if (user == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            var deleteUser = new DeleteUser(user);
            var actionResult = _actionHandlerOverlord.HandleAction<DeleteUser, User>(deleteUser);

            responseCode = actionResult.ValidationResult.IsValid
                ? HttpStatusCode.OK
                : HttpStatusCode.BadRequest;

            return Request.CreateResponse(responseCode, actionResult);
        }
    }
}
