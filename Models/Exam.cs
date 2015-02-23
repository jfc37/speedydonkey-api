using System;

namespace Models
{
    public class Exam : CourseWork
    {
        public DateTime StartTime { get; set; }
        public string Location { get; set; }

        public Course Course { get; set; }
    }
}
