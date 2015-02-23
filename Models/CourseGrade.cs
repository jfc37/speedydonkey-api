using System.Collections.Generic;

namespace Models
{
    public class CourseGrade
    {
        public int Id { get; set; }
        public Student Student { get; set; }
        public Course Course { get; set; }
        public int GradePercentage { get; set; }

        public IList<CourseWorkGrade> CourseWorkGrades { get; set; }

        public CourseGrade()
        {
            CourseWorkGrades = new List<CourseWorkGrade>();
        }
    }

    public class CourseWorkGrade
    {
        public int Id { get; set; }
        public int GradePercentage { get; set; }

        public CourseWork CourseWork { get; set; }
        public CourseGrade CourseGrade { get; set; }
    }
}
