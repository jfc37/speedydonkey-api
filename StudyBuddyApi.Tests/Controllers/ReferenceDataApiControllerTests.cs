using System.Net.Http;
using Action;
using Models;
using SpeedyDonkeyApi.Controllers;
using SpeedyDonkeyApi.Models;

namespace StudyBuddyApi.Tests.Controllers
{
    #region Get By Id
    public class ReferenceDataApiWhenTheEntityDoesntExist : WhenTheEntityDoesntExist<ReferenceDataModel, ReferenceData>
    {
        protected override GenericApiController<ReferenceDataModel, ReferenceData> GetContreteController()
        {
            return new ReferenceDataApiController(
                ActionHandlerOverlordBuilder.BuildObject(),
                UrlConstructorBuilder.BuildObject(),
                RepositoryBuilder.BuildObject(),
                Cloner,
                EntitySearchBuilder.BuildObject());
        }
    }
    public class ReferenceDataApiWhenTheEntityExists : WhenTheEntityExists<ReferenceDataModel, ReferenceData>
    {
        protected override GenericApiController<ReferenceDataModel, ReferenceData> GetContreteController()
        {
            return new ReferenceDataApiController(
                ActionHandlerOverlordBuilder.BuildObject(),
                UrlConstructorBuilder.BuildObject(),
                RepositoryBuilder.BuildObject(),
                Cloner,
                EntitySearchBuilder.BuildObject());
        }
    }
    #endregion

    #region Get All

    public class ReferenceDataApiWhenSomeEntitiesExists : WhenSomeEntitiesExists<ReferenceDataModel, ReferenceData>
    {
        protected override GenericApiController<ReferenceDataModel, ReferenceData> GetContreteController()
        {
            return new ReferenceDataApiController(
                ActionHandlerOverlordBuilder.BuildObject(),
                UrlConstructorBuilder.BuildObject(),
                RepositoryBuilder.BuildObject(),
                Cloner,
                EntitySearchBuilder.BuildObject());
        }
    }

    public class ReferenceDataApiWhenNoEntitiesExist : WhenNoEntitiesExist<ReferenceDataModel, ReferenceData>
    {
        protected override GenericApiController<ReferenceDataModel, ReferenceData> GetContreteController()
        {
            return new ReferenceDataApiController(
                ActionHandlerOverlordBuilder.BuildObject(),
                UrlConstructorBuilder.BuildObject(),
                RepositoryBuilder.BuildObject(),
                Cloner,
                EntitySearchBuilder.BuildObject());
        }
    }

    #endregion

    #region Search

    public class ReferenceDataApiWhenEntitiesMatchSearch : WhenEntitiesMatchSearch<ReferenceDataModel, ReferenceData>
    {
        protected override GenericApiController<ReferenceDataModel, ReferenceData> GetContreteController()
        {
            return new ReferenceDataApiController(
                ActionHandlerOverlordBuilder.BuildObject(),
                UrlConstructorBuilder.BuildObject(),
                RepositoryBuilder.BuildObject(),
                Cloner,
                EntitySearchBuilder.BuildObject());
        }
    }

    public class ReferenceDataApiWhenNoEntitiesMatchSearch : WhenNoEntitiesMatchSearch<ReferenceDataModel, ReferenceData>
    {
        protected override GenericApiController<ReferenceDataModel, ReferenceData> GetContreteController()
        {
            return new ReferenceDataApiController(
                ActionHandlerOverlordBuilder.BuildObject(),
                UrlConstructorBuilder.BuildObject(),
                RepositoryBuilder.BuildObject(),
                Cloner,
                EntitySearchBuilder.BuildObject());
        }
    }

    #endregion

    #region PerformAction

    public class ReferenceDataApiWhenRequestIsValid : WhenRequestIsValid<ReferenceDataModel, ReferenceData, CreateReferenceData>
    {
        protected override GenericApiController<ReferenceDataModel, ReferenceData> GetContreteController()
        {
            return new ReferenceDataApiController(
                ActionHandlerOverlordBuilder.BuildObject(),
                UrlConstructorBuilder.BuildObject(),
                RepositoryBuilder.BuildObject(),
                Cloner,
                EntitySearchBuilder.BuildObject());
        }

        protected override HttpResponseMessage PerformAction()
        {
            return ((ReferenceDataApiController)GetController()).Post(Model);
        }
    }

    public class ReferenceDataApiWhenRequestIsInvalid : WhenRequestIsInvalid<ReferenceDataModel, ReferenceData, CreateReferenceData>
    {
        protected override GenericApiController<ReferenceDataModel, ReferenceData> GetContreteController()
        {
            return new ReferenceDataApiController(
                ActionHandlerOverlordBuilder.BuildObject(),
                UrlConstructorBuilder.BuildObject(),
                RepositoryBuilder.BuildObject(),
                Cloner,
                EntitySearchBuilder.BuildObject());
        }

        protected override HttpResponseMessage PerformAction()
        {
            return ((ReferenceDataApiController)GetController()).Post(Model);
        }
    }

    #endregion
}