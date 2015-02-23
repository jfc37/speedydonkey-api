using System;

namespace SpeedyDonkeyApi.Models
{
    public class NoticeModel
    {
        public string Url { get; set; }
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public CourseModel Course { get; set; }

    }
}