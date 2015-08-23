using Models;

namespace SpeedyDonkeyApi.Models
{
    public class BookingModel : ApiModel<Booking, BookingModel>, IBooking
    {
        protected virtual string RouteName
        {
            get { return "BookingApi"; }
        }

        public IRoom Room { get; set; }
        public IEvent Event { get; set; }
    }
}