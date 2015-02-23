using System;

namespace SpeedyDonkeyApi.Models
{
    public class LectureModel
    {
        public string Url { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Location { get; set; }
        public string Occurence { get; set; }

        public CourseModel Course { get; set; }

    }
}