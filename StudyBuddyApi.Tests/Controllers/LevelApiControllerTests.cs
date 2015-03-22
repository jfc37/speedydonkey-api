using System.Net.Http;
using Action;
using Models;
using SpeedyDonkeyApi.Controllers;
using SpeedyDonkeyApi.Models;

namespace StudyBuddyApi.Tests.Controllers
{
    #region Get By Id
    public class LevelApiWhenTheEntityDoesntExist : WhenTheEntityDoesntExist<LevelModel, Level>
    {
        protected override GenericApiController<LevelModel, Level> GetContreteController()
        {
            return new LevelApiController(
                ActionHandlerOverlordBuilder.BuildObject(),
                UrlConstructorBuilder.BuildObject(),
                RepositoryBuilder.BuildObject(),
                Cloner,
                EntitySearchBuilder.BuildObject());
        }
    }
    public class LevelApiWhenTheEntityExists : WhenTheEntityExists<LevelModel, Level>
    {
        protected override GenericApiController<LevelModel, Level> GetContreteController()
        {
            return new LevelApiController(
                ActionHandlerOverlordBuilder.BuildObject(),
                UrlConstructorBuilder.BuildObject(),
                RepositoryBuilder.BuildObject(),
                Cloner,
                EntitySearchBuilder.BuildObject());
        }
    }
    #endregion

    #region Get All

    public class LevelApiWhenSomeEntitiesExists : WhenSomeEntitiesExists<LevelModel, Level>
    {
        protected override GenericApiController<LevelModel, Level> GetContreteController()
        {
            return new LevelApiController(
                ActionHandlerOverlordBuilder.BuildObject(),
                UrlConstructorBuilder.BuildObject(),
                RepositoryBuilder.BuildObject(),
                Cloner,
                EntitySearchBuilder.BuildObject());
        }
    }

    public class LevelApiWhenNoEntitiesExist : WhenNoEntitiesExist<LevelModel, Level>
    {
        protected override GenericApiController<LevelModel, Level> GetContreteController()
        {
            return new LevelApiController(
                ActionHandlerOverlordBuilder.BuildObject(),
                UrlConstructorBuilder.BuildObject(),
                RepositoryBuilder.BuildObject(),
                Cloner,
                EntitySearchBuilder.BuildObject());
        }
    }

    #endregion

    #region Search

    public class LevelApiWhenEntitiesMatchSearch : WhenEntitiesMatchSearch<LevelModel, Level>
    {
        protected override GenericApiController<LevelModel, Level> GetContreteController()
        {
            return new LevelApiController(
                ActionHandlerOverlordBuilder.BuildObject(),
                UrlConstructorBuilder.BuildObject(),
                RepositoryBuilder.BuildObject(),
                Cloner,
                EntitySearchBuilder.BuildObject());
        }
    }

    public class LevelApiWhenNoEntitiesMatchSearch : WhenNoEntitiesMatchSearch<LevelModel, Level>
    {
        protected override GenericApiController<LevelModel, Level> GetContreteController()
        {
            return new LevelApiController(
                ActionHandlerOverlordBuilder.BuildObject(),
                UrlConstructorBuilder.BuildObject(),
                RepositoryBuilder.BuildObject(),
                Cloner,
                EntitySearchBuilder.BuildObject());
        }
    }

    #endregion

    #region Post

    public class LevelApiWhenRequestIsValid : WhenRequestIsValid<LevelModel, Level, CreateLevel>
    {
        protected override GenericApiController<LevelModel, Level> GetContreteController()
        {
            return new LevelApiController(
                ActionHandlerOverlordBuilder.BuildObject(),
                UrlConstructorBuilder.BuildObject(),
                RepositoryBuilder.BuildObject(),
                Cloner,
                EntitySearchBuilder.BuildObject());
        }

        protected override HttpResponseMessage PerformAction()
        {
            return ((LevelApiController)GetController()).Post(Model);
        }
    }

    public class LevelApiWhenRequestIsInvalid : WhenRequestIsInvalid<LevelModel, Level, CreateLevel>
    {
        protected override GenericApiController<LevelModel, Level> GetContreteController()
        {
            return new LevelApiController(
                ActionHandlerOverlordBuilder.BuildObject(),
                UrlConstructorBuilder.BuildObject(),
                RepositoryBuilder.BuildObject(),
                Cloner,
                EntitySearchBuilder.BuildObject());
        }

        protected override HttpResponseMessage PerformAction()
        {
            return ((LevelApiController)GetController()).Post(Model);
        }
    }

    #endregion
}