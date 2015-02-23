using System;
using System.Collections.Generic;

namespace Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public IList<Assignment> Assignments { get; set; }
        public IList<Exam> Exams { get; set; }
        public IList<Lecture> Lectures { get; set; }
        public IList<Notice> Notices { get; set; }
        public IList<Student> Students { get; set; }
        public IList<Professor> Professors { get; set; }
        public GradeType GradeType { get; set; }

        public Course()
        {
            Professors = new List<Professor>();
            Notices = new List<Notice>();
            Lectures = new List<Lecture>();
            Assignments = new List<Assignment>();
            Exams = new List<Exam>();
            Students = new List<Student>();
        }
    }

    public enum GradeType
    {
        Invalid,
        Letter,
        Percentage
    }
}
