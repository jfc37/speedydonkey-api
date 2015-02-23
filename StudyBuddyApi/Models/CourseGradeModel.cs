using System.Collections.Generic;

namespace SpeedyDonkeyApi.Models
{
    public class CourseGradeModel
    {
        public string Url { get; set; }
        public int Id { get; set; }
        public int GradePercentage { get; set; }

        public StudentModel Student { get; set; }
        public CourseModel Course { get; set; }
        public IList<CourseWorkGradeModel> CourseWorkGrades { get; set; }  
    }

    public class CourseWorkGradeModel
    {
        public string Url { get; set; }
        public int Id { get; set; }
        public int GradePercentage { get; set; }

        public CourseWorkModel CourseWork { get; set; }
        public CourseGradeModel CourseGrade { get; set; }
    }
}