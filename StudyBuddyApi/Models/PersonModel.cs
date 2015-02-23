using System.Collections.Generic;

namespace SpeedyDonkeyApi.Models
{
    public abstract class PersonModel
    {
        public string Url { get; set; }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Role { get; set; }

        public UserModel User { get; set; }
    }

    public class StudentModel : PersonModel
    {
        public IList<CourseModel> EnroledCourses { get; set; }
        public IList<CourseGradeModel> Grades { get; set; }

        public StudentModel()
        {
            EnroledCourses = new List<CourseModel>();
            Grades = new List<CourseGradeModel>();
        }
    }

    public class ProfessorModel : PersonModel
    {
        public IList<CourseModel> Courses { get; set; }

        public ProfessorModel()
        {
            Courses = new List<CourseModel>();
        }
    }
}