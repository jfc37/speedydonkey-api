using System;
using System.Web.Http;
using Action;
using ActionHandlers;
using Actions;
using Contracts;
using Contracts.Users;
using Data.Repositories;
using Data.Searches;
using Models;
using SpeedyDonkeyApi.CodeChunks;
using SpeedyDonkeyApi.Filter;

namespace SpeedyDonkeyApi.Controllers.Users
{
    [RoutePrefix("api/users")]
    public class UserApiController : GenericApiController<User>
    {
        public UserApiController(
            IActionHandlerOverlord actionHandlerOverlord,
            IRepository<User> repository,
            IEntitySearch<User> entitySearch)
            : base(actionHandlerOverlord, repository, entitySearch) { }

        [Route]
        [AllowAnonymous]
        public IHttpActionResult Post([FromBody] UserModel model)
        {
            throw new Exception("xxxx");
            var result = PerformAction<CreateUser, User>(new CreateUser(model.ToEntity()));

            return new ActionResultToCreatedHttpActionResult<User, UserModel>(result, x => x.ToModel(), this).Do();
        }

        [Route("{id:int}")]
        [ClaimsAuthorise(Claim = Claim.Teacher)]
        public IHttpActionResult Get(int id)
        {
            return new EntityToHttpActionResult<User>(this, GetById(id), x => x.ToModel()).Do();
        }

        [Route]
        [ClaimsAuthorise(Claim = Claim.Teacher)]
        public IHttpActionResult Get()
        {
            return new SetToHttpActionResult<User>(this, GetAll(), x => x.ToModel()).Do();
        }

        [Route]
        [ClaimsAuthorise(Claim = Claim.Teacher)]
        public IHttpActionResult Get(string q)
        {
            return new SetToHttpActionResult<User>(this, Search(q), x => x.ToModel()).Do();
        }

        [Route("{id:int}")]
        [ClaimsAuthorise(Claim = Claim.Teacher)]
        public IHttpActionResult Delete(int id)
        {
            var model = new User(id);
            var result = PerformAction<DeleteUser, User>(new DeleteUser(model));


            return new ActionResultToCreatedHttpActionResult<User, UserModel>(result, x => x.ToModel(), this)
                .Do();
        }
    }
}
