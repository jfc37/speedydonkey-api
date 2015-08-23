using ActionHandlers;
using Common;
using Data.Repositories;
using Data.Searches;
using Models;
using SpeedyDonkeyApi.Models;

namespace SpeedyDonkeyApi.Controllers
{
    public class BookingApiController : GenericApiController<BookingModel, Booking>
    {
        public BookingApiController(IActionHandlerOverlord actionHandlerOverlord, IRepository<Booking> repository, IEntitySearch<Booking> entitySearch) : base(actionHandlerOverlord, repository, entitySearch)
        {
        }
    }
}