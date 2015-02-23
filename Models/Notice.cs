using System;

namespace Models
{
    public class Notice
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Course Course { get; set; }
    }
}
