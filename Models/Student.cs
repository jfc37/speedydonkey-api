using System.Collections.Generic;

namespace Models
{
    public class Student : Person
    {
        public IList<CourseGrade> CourseGrades { get; set; }
        public IList<Course> EnroledCourses { get; set; } 

        public Student()
        {
            Role = Role.Student;
            EnroledCourses = new List<Course>();
            CourseGrades = new List<CourseGrade>();
        }
    }
}
