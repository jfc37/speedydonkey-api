using Models;
using SpeedyDonkeyApi.Controllers;
using SpeedyDonkeyApi.Models;

namespace StudyBuddyApi.Tests.Controllers
{

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
}