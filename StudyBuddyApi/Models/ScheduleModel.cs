using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Common;
using Models;
using SpeedyDonkeyApi.Services;

namespace SpeedyDonkeyApi.Models
{
    public class ScheduleModel : ApiModel<Schedule, ScheduleModel>, ISchedule
    {
        protected override string RouteName
        {
            get { return "ScheduleApi"; }
        }

        public IList<IBooking> Bookings { get; set; }

        protected override void AddFullChild(HttpRequestMessage request, IUrlConstructor urlConstructor, Schedule entity, ScheduleModel model,
            ICommonInterfaceCloner cloner)
        {
            if (entity.Bookings != null)
            {
                var bookingModel = new BookingModel();
                model.Bookings = (IList<IBooking>) entity.Bookings.Select(x => bookingModel.CloneFromEntity(request, urlConstructor, (Booking) x, cloner))
                    .ToList();
            }
        }
    }
}