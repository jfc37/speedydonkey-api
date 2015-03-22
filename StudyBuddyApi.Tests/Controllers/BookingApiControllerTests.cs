using Models;
using SpeedyDonkeyApi.Controllers;
using SpeedyDonkeyApi.Models;

namespace StudyBuddyApi.Tests.Controllers
{
    #region Get By Id
    public class BookingApiWhenTheEntityDoesntExist : WhenTheEntityDoesntExist<BookingModel, Booking>
    {
        protected override GenericApiController<BookingModel, Booking> GetContreteController()
        {
            return new BookingApiController(
                ActionHandlerOverlordBuilder.BuildObject(),
                UrlConstructorBuilder.BuildObject(),
                RepositoryBuilder.BuildObject(),
                Cloner,
                EntitySearchBuilder.BuildObject());
        }
    }
    public class BookingApiWhenTheEntityExists : WhenTheEntityExists<BookingModel, Booking>
    {
        protected override GenericApiController<BookingModel, Booking> GetContreteController()
        {
            return new BookingApiController(
                ActionHandlerOverlordBuilder.BuildObject(),
                UrlConstructorBuilder.BuildObject(),
                RepositoryBuilder.BuildObject(),
                Cloner,
                EntitySearchBuilder.BuildObject());
        }
    }
    #endregion

    #region Get All

    public class BookingApiWhenSomeEntitiesExists : WhenSomeEntitiesExists<BookingModel, Booking>
    {
        protected override GenericApiController<BookingModel, Booking> GetContreteController()
        {
            return new BookingApiController(
                ActionHandlerOverlordBuilder.BuildObject(),
                UrlConstructorBuilder.BuildObject(),
                RepositoryBuilder.BuildObject(),
                Cloner,
                EntitySearchBuilder.BuildObject());
        }
    }

    public class BookingApiWhenNoEntitiesExist : WhenNoEntitiesExist<BookingModel, Booking>
    {
        protected override GenericApiController<BookingModel, Booking> GetContreteController()
        {
            return new BookingApiController(
                ActionHandlerOverlordBuilder.BuildObject(),
                UrlConstructorBuilder.BuildObject(),
                RepositoryBuilder.BuildObject(),
                Cloner,
                EntitySearchBuilder.BuildObject());
        }
    }

    #endregion

    #region Search

    public class BookingApiWhenEntitiesMatchSearch : WhenEntitiesMatchSearch<BookingModel, Booking>
    {
        protected override GenericApiController<BookingModel, Booking> GetContreteController()
        {
            return new BookingApiController(
                ActionHandlerOverlordBuilder.BuildObject(),
                UrlConstructorBuilder.BuildObject(),
                RepositoryBuilder.BuildObject(),
                Cloner,
                EntitySearchBuilder.BuildObject());
        }
    }

    public class BookingApiWhenNoEntitiesMatchSearch : WhenNoEntitiesMatchSearch<BookingModel, Booking>
    {
        protected override GenericApiController<BookingModel, Booking> GetContreteController()
        {
            return new BookingApiController(
                ActionHandlerOverlordBuilder.BuildObject(),
                UrlConstructorBuilder.BuildObject(),
                RepositoryBuilder.BuildObject(),
                Cloner,
                EntitySearchBuilder.BuildObject());
        }
    }

    #endregion
}