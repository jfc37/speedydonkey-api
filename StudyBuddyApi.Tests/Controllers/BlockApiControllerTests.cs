using System.Net.Http;
using Action;
using Models;
using SpeedyDonkeyApi.Controllers;
using SpeedyDonkeyApi.Models;

namespace StudyBuddyApi.Tests.Controllers
{
    #region Get By Id
    public class BlockApiWhenTheEntityDoesntExist : WhenTheEntityDoesntExist<BlockModel, Block>
    {
        protected override GenericApiController<BlockModel, Block> GetContreteController()
        {
            return new BlockApiController(
                ActionHandlerOverlordBuilder.BuildObject(),
                RepositoryBuilder.BuildObject(),
                EntitySearchBuilder.BuildObject());
        }
    }
    public class BlockApiWhenTheEntityExists : WhenTheEntityExists<BlockModel, Block>
    {
        protected override GenericApiController<BlockModel, Block> GetContreteController()
        {
            return new BlockApiController(
                ActionHandlerOverlordBuilder.BuildObject(),
                RepositoryBuilder.BuildObject(),
                EntitySearchBuilder.BuildObject());
        }
    }
    #endregion

    #region Get All

    public class BlockApiWhenSomeEntitiesExists : WhenSomeEntitiesExists<BlockModel, Block>
    {
        protected override GenericApiController<BlockModel, Block> GetContreteController()
        {
            return new BlockApiController(
                ActionHandlerOverlordBuilder.BuildObject(),
                RepositoryBuilder.BuildObject(),
                EntitySearchBuilder.BuildObject());
        }
    }

    public class BlockApiWhenNoEntitiesExist : WhenNoEntitiesExist<BlockModel, Block>
    {
        protected override GenericApiController<BlockModel, Block> GetContreteController()
        {
            return new BlockApiController(
                ActionHandlerOverlordBuilder.BuildObject(),
                RepositoryBuilder.BuildObject(),
                EntitySearchBuilder.BuildObject());
        }
    }

    #endregion

    #region Search

    public class BlockApiWhenEntitiesMatchSearch : WhenEntitiesMatchSearch<BlockModel, Block>
    {
        protected override GenericApiController<BlockModel, Block> GetContreteController()
        {
            return new BlockApiController(
                ActionHandlerOverlordBuilder.BuildObject(),
                RepositoryBuilder.BuildObject(),
                EntitySearchBuilder.BuildObject());
        }
    }

    public class BlockApiWhenNoEntitiesMatchSearch : WhenNoEntitiesMatchSearch<BlockModel, Block>
    {
        protected override GenericApiController<BlockModel, Block> GetContreteController()
        {
            return new BlockApiController(
                ActionHandlerOverlordBuilder.BuildObject(),
                RepositoryBuilder.BuildObject(),
                EntitySearchBuilder.BuildObject());
        }
    }

    #endregion

    #region PerformAction

    public class BlockApiWhenRequestIsValid : WhenRequestIsValid<BlockModel, Block, CreateBlock>
    {
        private int _levelId;
        protected override GenericApiController<BlockModel, Block> GetContreteController()
        {
            return new BlockApiController(
                ActionHandlerOverlordBuilder.BuildObject(),
                RepositoryBuilder.BuildObject(),
                EntitySearchBuilder.BuildObject());
        }

        protected override HttpResponseMessage PerformAction()
        {
            return ((BlockApiController)GetController()).Post(_levelId);
        }
    }

    public class BlockApiWhenRequestIsInvalid : WhenRequestIsInvalid<BlockModel, Block, CreateBlock>
    {
        private int _levelId;

        protected override GenericApiController<BlockModel, Block> GetContreteController()
        {
            return new BlockApiController(
                ActionHandlerOverlordBuilder.BuildObject(),
                RepositoryBuilder.BuildObject(),
                EntitySearchBuilder.BuildObject());
        }

        protected override HttpResponseMessage PerformAction()
        {
            return ((BlockApiController)GetController()).Post(_levelId);
        }
    }

    #endregion
}