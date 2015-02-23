using System.Collections.Generic;

namespace Models.Tests.Builders
{
    public class CourseGradeBuilder
    {
        private Student _student;
        private Course _course;
        private int _id;
        private int _gradePercentage;
        private List<CourseWorkGrade> _courseWorkGrades;

        public CourseGradeBuilder()
        {
            _courseWorkGrades = new List<CourseWorkGrade>();
        }

        public CourseGrade Build()
        {
            return new CourseGrade
            {
                Id = _id,
                Student = _student,
                Course = _course,
                GradePercentage = _gradePercentage,
                CourseWorkGrades = _courseWorkGrades
            };
        }

        public CourseGradeBuilder WithStudent(Student student)
        {
            _student = student;
            return this;
        }

        public CourseGradeBuilder WithCourse(Course course)
        {
            _course = course;
            return this;
        }

        public CourseGradeBuilder WithId(int id)
        {
            _id = id;
            return this;
        }

        public CourseGradeBuilder WithNoStudent()
        {
            _student = null;
            return this;
        }

        public CourseGradeBuilder WithGradePercentage(int gradePercentage)
        {
            _gradePercentage = gradePercentage;
            return this;
        }

        public CourseGradeBuilder WithCourseWorkGrades()
        {
            _courseWorkGrades = new List<CourseWorkGrade>
            {
                new CourseWorkGrade{CourseWork = new Assignment()},
                new CourseWorkGrade{CourseWork = new Assignment()},
                new CourseWorkGrade{CourseWork = new Assignment()}
            };
            return this;
        }

        public CourseGradeBuilder WithNoCourseWorkGrades()
        {
            _courseWorkGrades = new List<CourseWorkGrade>();
            return this;
        }
    }
}