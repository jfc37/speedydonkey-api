using ActionHandlers;
using Data.Repositories;
using Data.Searches;
using Models;

namespace SpeedyDonkeyApi.Controllers
{
    public class BookingApiController : GenericApiController<Booking>
    {
        public BookingApiController(IActionHandlerOverlord actionHandlerOverlord, IRepository<Booking> repository, IEntitySearch<Booking> entitySearch) : base(actionHandlerOverlord, repository, entitySearch)
        {
        }
    }
}