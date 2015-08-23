using Models;
using SpeedyDonkeyApi.Controllers;
using SpeedyDonkeyApi.Models;

namespace StudyBuddyApi.Tests.Controllers
{
    #region Get By Id
    public class ClassApiWhenTheEntityDoesntExist : WhenTheEntityDoesntExist<ClassModel, Class>
    {
        protected override GenericApiController<ClassModel, Class> GetContreteController()
        {
            return new ClassApiController(
                ActionHandlerOverlordBuilder.BuildObject(),
                RepositoryBuilder.BuildObject(),
                EntitySearchBuilder.BuildObject());
        }
    }
    public class ClassApiWhenTheEntityExists : WhenTheEntityExists<ClassModel, Class>
    {
        protected override GenericApiController<ClassModel, Class> GetContreteController()
        {
            return new ClassApiController(
                ActionHandlerOverlordBuilder.BuildObject(),
                RepositoryBuilder.BuildObject(),
                EntitySearchBuilder.BuildObject());
        }
    }
    #endregion

    #region Get All

    public class ClassApiWhenSomeEntitiesExists : WhenSomeEntitiesExists<ClassModel, Class>
    {
        protected override GenericApiController<ClassModel, Class> GetContreteController()
        {
            return new ClassApiController(
                ActionHandlerOverlordBuilder.BuildObject(),
                RepositoryBuilder.BuildObject(),
                EntitySearchBuilder.BuildObject());
        }
    }

    public class ClassApiWhenNoEntitiesExist : WhenNoEntitiesExist<ClassModel, Class>
    {
        protected override GenericApiController<ClassModel, Class> GetContreteController()
        {
            return new ClassApiController(
                ActionHandlerOverlordBuilder.BuildObject(),
                RepositoryBuilder.BuildObject(),
                EntitySearchBuilder.BuildObject());
        }
    }

    #endregion

    #region Search

    public class ClassApiWhenEntitiesMatchSearch : WhenEntitiesMatchSearch<ClassModel, Class>
    {
        protected override GenericApiController<ClassModel, Class> GetContreteController()
        {
            return new ClassApiController(
                ActionHandlerOverlordBuilder.BuildObject(),
                RepositoryBuilder.BuildObject(),
                EntitySearchBuilder.BuildObject());
        }
    }

    public class ClassApiWhenNoEntitiesMatchSearch : WhenNoEntitiesMatchSearch<ClassModel, Class>
    {
        protected override GenericApiController<ClassModel, Class> GetContreteController()
        {
            return new ClassApiController(
                ActionHandlerOverlordBuilder.BuildObject(),
                RepositoryBuilder.BuildObject(),
                EntitySearchBuilder.BuildObject());
        }
    }

    #endregion
}