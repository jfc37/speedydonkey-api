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
                UrlConstructorBuilder.BuildObject(),
                RepositoryBuilder.BuildObject(),
                Cloner,
                EntitySearchBuilder.BuildObject());
        }
    }
    public class UserApiWhenTheEntityExists : WhenTheEntityExists<UserModel, User>
    {
        protected override GenericApiController<UserModel, User> GetContreteController()
        {
            return new UserApiController(
                ActionHandlerOverlordBuilder.BuildObject(),
                UrlConstructorBuilder.BuildObject(),
                RepositoryBuilder.BuildObject(),
                Cloner,
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
                UrlConstructorBuilder.BuildObject(),
                RepositoryBuilder.BuildObject(),
                Cloner,
                EntitySearchBuilder.BuildObject());
        }
    }

    public class UserApiWhenNoEntitiesExist : WhenNoEntitiesExist<UserModel, User>
    {
        protected override GenericApiController<UserModel, User> GetContreteController()
        {
            return new UserApiController(
                ActionHandlerOverlordBuilder.BuildObject(),
                UrlConstructorBuilder.BuildObject(),
                RepositoryBuilder.BuildObject(),
                Cloner,
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
                UrlConstructorBuilder.BuildObject(),
                RepositoryBuilder.BuildObject(),
                Cloner,
                EntitySearchBuilder.BuildObject());
        }
    }

    public class UserApiWhenNoEntitiesMatchSearch : WhenNoEntitiesMatchSearch<UserModel, User>
    {
        protected override GenericApiController<UserModel, User> GetContreteController()
        {
            return new UserApiController(
                ActionHandlerOverlordBuilder.BuildObject(),
                UrlConstructorBuilder.BuildObject(),
                RepositoryBuilder.BuildObject(),
                Cloner,
                EntitySearchBuilder.BuildObject());
        }
    }

    #endregion
}