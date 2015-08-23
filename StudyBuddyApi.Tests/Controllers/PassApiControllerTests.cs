using Models;
using SpeedyDonkeyApi.Controllers;
using SpeedyDonkeyApi.Models;

namespace StudyBuddyApi.Tests.Controllers
{
    #region Get By Id
    public class PassApiWhenTheEntityDoesntExist : WhenTheEntityDoesntExist<PassModel, Pass>
    {
        protected override GenericApiController<PassModel, Pass> GetContreteController()
        {
            return new PassApiController(
                ActionHandlerOverlordBuilder.BuildObject(),
                RepositoryBuilder.BuildObject(),
                EntitySearchBuilder.BuildObject());
        }
    }
    public class PassApiWhenTheEntityExists : WhenTheEntityExists<PassModel, Pass>
    {
        protected override GenericApiController<PassModel, Pass> GetContreteController()
        {
            return new PassApiController(
                ActionHandlerOverlordBuilder.BuildObject(),
                RepositoryBuilder.BuildObject(),
                EntitySearchBuilder.BuildObject());
        }
    }
    #endregion

    #region Get All

    public class PassApiWhenSomeEntitiesExists : WhenSomeEntitiesExists<PassModel, Pass>
    {
        protected override GenericApiController<PassModel, Pass> GetContreteController()
        {
            return new PassApiController(
                ActionHandlerOverlordBuilder.BuildObject(),
                RepositoryBuilder.BuildObject(),
                EntitySearchBuilder.BuildObject());
        }
    }

    public class PassApiWhenNoEntitiesExist : WhenNoEntitiesExist<PassModel, Pass>
    {
        protected override GenericApiController<PassModel, Pass> GetContreteController()
        {
            return new PassApiController(
                ActionHandlerOverlordBuilder.BuildObject(),
                RepositoryBuilder.BuildObject(),
                EntitySearchBuilder.BuildObject());
        }
    }

    #endregion

    #region Search

    public class PassApiWhenEntitiesMatchSearch : WhenEntitiesMatchSearch<PassModel, Pass>
    {
        protected override GenericApiController<PassModel, Pass> GetContreteController()
        {
            return new PassApiController(
                ActionHandlerOverlordBuilder.BuildObject(),
                RepositoryBuilder.BuildObject(),
                EntitySearchBuilder.BuildObject());
        }
    }

    public class PassApiWhenNoEntitiesMatchSearch : WhenNoEntitiesMatchSearch<PassModel, Pass>
    {
        protected override GenericApiController<PassModel, Pass> GetContreteController()
        {
            return new PassApiController(
                ActionHandlerOverlordBuilder.BuildObject(),
                RepositoryBuilder.BuildObject(),
                EntitySearchBuilder.BuildObject());
        }
    }

    #endregion
}