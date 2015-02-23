using System;

namespace Models
{
    public class Lecture
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Location { get; set; }
        public Occurence Occurence { get; set; }

        public Course Course { get; set; }
    }

    public enum Occurence
    {
        Invalid,
        Daily,
        Weekly,
        Fortnightly,
        Monthly
    }
}
