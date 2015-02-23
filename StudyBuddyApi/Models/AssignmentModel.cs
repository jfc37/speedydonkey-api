using System;

namespace SpeedyDonkeyApi.Models
{
    public class AssignmentModel : CourseWorkModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}