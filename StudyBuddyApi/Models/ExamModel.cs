using System;

namespace SpeedyDonkeyApi.Models
{
    public class ExamModel : CourseWorkModel
    {
        public DateTime StartTime { get; set; }
        public string Location { get; set; }
    }
}