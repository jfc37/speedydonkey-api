using System;

namespace SpeedyDonkeyApi.Models
{
    public class CourseWorkModel
    {
        public string Url { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int FinalMarkPercentage { get; set; }
        public string GradeType { get; set; }

        public CourseModel Course { get; set; }

    }
}