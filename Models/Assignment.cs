using System;

namespace Models
{
    public class Assignment : CourseWork
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Course Course { get; set; }
    }
}
