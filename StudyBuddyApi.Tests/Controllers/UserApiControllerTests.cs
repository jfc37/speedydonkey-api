using System.Net.Http;
using Actions;
using Models;
using SpeedyDonkeyApi.Controllers;
using SpeedyDonkeyApi.Models;

namespace StudyBuddyApi.Tests.Controllers
{
    #region Get By Id
    public class UserApiWhenTheEntityDoesntExist : WhenTheEntityDoesntExist<UserModel, User>
    {
        protected override GenericApiController<UserModel, User> GetContreteController()
        {
            return new UserApiController(
                ActionHandlerOverlordBuilder.BuildObject(),
                RepositoryBuilder.BuildObject(),
                EntitySearchBuilder.BuildObject());
        }
    }
    public class UserApiWhenTheEntityExists : WhenTheEntityExists<UserModel, User>
    {
        protected override GenericApiController<UserModel, User> GetContreteController()
        {
            return new UserApiController(
                ActionHandlerOverlordBuilder.BuildObject(),
                RepositoryBuilder.BuildObject(),
                EntitySearchBuilder.BuildObject());
        }
    }
    #endregion

    #region Get All

    public class UserApiWhenSomeEntitiesExists : WhenSomeEntitiesExists<UserModel, User>
    {
        protected override GenericApiController<UserModel, User> GetContreteController()
        {
            return new UserApiController(
                ActionHandlerOverlordBuilder.BuildObject(),
                RepositoryBuilder.BuildObject(),
                EntitySearchBuilder.BuildObject());
        }
    }

    public class UserApiWhenNoEntitiesExist : WhenNoEntitiesExist<UserModel, User>
    {
        protected override GenericApiController<UserModel, User> GetContreteController()
        {
            return new UserApiController(
                ActionHandlerOverlordBuilder.BuildObject(),
                RepositoryBuilder.BuildObject(),
                EntitySearchBuilder.BuildObject());
        }
    }

    #endregion

    #region Search

    public class UserApiWhenEntitiesMatchSearch : WhenEntitiesMatchSearch<UserModel, User>
    {
        protected override GenericApiController<UserModel, User> GetContreteController()
        {
            return new UserApiController(
                ActionHandlerOverlordBuilder.BuildObject(),
                RepositoryBuilder.BuildObject(),
                EntitySearchBuilder.BuildObject());
        }
    }

    public class UserApiWhenNoEntitiesMatchSearch : WhenNoEntitiesMatchSearch<UserModel, User>
    {
        protected override GenericApiController<UserModel, User> GetContreteController()
        {
            return new UserApiController(
                ActionHandlerOverlordBuilder.BuildObject(),
                RepositoryBuilder.BuildObject(),
                EntitySearchBuilder.BuildObject());
        }
    }

    #endregion

    #region PerformAction

    public class UserApiWhenRequestIsValid : WhenRequestIsValid<UserModel, User, CreateUser>
    {
        protected override GenericApiController<UserModel, User> GetContreteController()
        {
            return new UserApiController(
                ActionHandlerOverlordBuilder.BuildObject(),
                RepositoryBuilder.BuildObject(),
                EntitySearchBuilder.BuildObject());
        }

        protected override HttpResponseMessage PerformAction()
        {
            return ((UserApiController) GetController()).Post(Model);
        }
    }

    public class UserApiWhenRequestIsInvalid : WhenRequestIsInvalid<UserModel, User, CreateUser>
    {
        protected override GenericApiController<UserModel, User> GetContreteController()
        {
            return new UserApiController(
                ActionHandlerOverlordBuilder.BuildObject(),
                RepositoryBuilder.BuildObject(),
                EntitySearchBuilder.BuildObject());
        }

        protected override HttpResponseMessage PerformAction()
        {
            return ((UserApiController)GetController()).Post(Model);
        }
    }

    #endregion
}