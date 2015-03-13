using ActionHandlers;
using Common;
using Data.Repositories;
using Data.Searches;
using Models;
using SpeedyDonkeyApi.Models;
using SpeedyDonkeyApi.Services;

namespace SpeedyDonkeyApi.Controllers
{
    public class BookingApiController : GenericApiController<BookingModel, Booking>
    {
        public BookingApiController(IActionHandlerOverlord actionHandlerOverlord, IUrlConstructor urlConstructor, IRepository<Booking> repository, ICommonInterfaceCloner cloner, IEntitySearch<Booking> entitySearch) : base(actionHandlerOverlord, urlConstructor, repository, cloner, entitySearch)
        {
        }
    }
}