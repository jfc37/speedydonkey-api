using System.Net.Http;
using Models;
using SpeedyDonkeyApi.Services;

namespace SpeedyDonkeyApi.Models
{
    public class BookingModel : ApiModel<Booking, BookingModel>, IBooking
    {
        protected override string RouteName
        {
            get { return "BookingApi"; }
        }

        public IRoom Room { get; set; }
        public IEvent Event { get; set; }
    }
}