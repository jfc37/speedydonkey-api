using System;
using System.Collections.Generic;
using System.Net.Http;
using Models;
using SpeedyDonkeyApi.Services;

namespace SpeedyDonkeyApi.Models
{
    public class ClassModel : ApiModel<Class, ClassModel>, IClass
    {
        protected override string RouteName
        {
            get { return "ClassApi"; }
        }

        public IList<ITeacher> Teachers { get; set; }
        public IList<ITeacher> RegisteredStudents { get; set; }
        public IBooking Booking { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public IList<IUser> ActualStudents { get; set; }
        public IBlock Block { get; set; }

        protected override void AddChildUrls(HttpRequestMessage request, IUrlConstructor urlConstructor, Class entity, ClassModel model)
        {
            if (entity.Block != null)
            {
                model.Block = (IBlock)new BlockModel().CreateModelWithOnlyUrl(request, urlConstructor, entity.Block.Id);
            }

            //if (entity.Booking!= null)
            //{
            //    model.Booking = (IBooking)new BookingModel().CreateModelWithOnlyUrl(request, urlConstructor, entity.Booking.Id);
            //}
        }
    }
}