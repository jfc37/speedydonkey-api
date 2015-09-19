using System.Web.Http;
using ActionHandlers;
using Data.Repositories;
using Data.Searches;
using Models;
using SpeedyDonkeyApi.CodeChunks;
using SpeedyDonkeyApi.Models;

namespace SpeedyDonkeyApi.Controllers
{
    [RoutePrefix("api/bookings")]
    public class BookingApiController : GenericApiController<Booking>
    {
        public BookingApiController(IActionHandlerOverlord actionHandlerOverlord, IRepository<Booking> repository, IEntitySearch<Booking> entitySearch)
            : base(actionHandlerOverlord, repository, entitySearch)
        {
        }

        [Route]
        public IHttpActionResult Get()
        {
            return new SetToHttpActionResult<Booking>(this, GetAll(), x => x.ToModel()).Do();
        }

        [Route]
        public IHttpActionResult Get(string q)
        {
            return new SetToHttpActionResult<Booking>(this, Search(q), x => x.ToModel()).Do();
        }

        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            return new EntityToHttpActionResult<Booking>(this, GetById(id), x => x.ToModel()).Do();
        }
    }
}