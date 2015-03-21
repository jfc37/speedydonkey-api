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
        public IList<IUser> RegisteredStudents { get; set; }
        public IBooking Booking { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Name { get; set; }
        public ICollection<IUser> ActualStudents { get; set; }
        public IBlock Block { get; set; }
    }
}