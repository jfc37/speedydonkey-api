using SpeedyDonkeyApi.Models;

namespace Models.Tests.Builders
{
    public class CourseWorkGradeBuilder
    {
        private Student _student;
        private Course _course;
        private CourseGrade _courseGrade;
        private CourseWork _courseWork;
        private int _id;
        private int _gradePercentage;

        public CourseWorkGradeBuilder()
        {
            _student = new Student();
            _course = new Course();
            _courseWork = new Assignment();
            _courseGrade = new CourseGrade
            {
                Student = _student,
                Course = _course
            };
        }

        public CourseWorkGrade Build()
        {
            return new CourseWorkGrade
            {
                Id = _id,
                CourseGrade = _courseGrade,
                CourseWork = _courseWork,
                GradePercentage = _gradePercentage
            };
        }

        public CourseWorkGradeModel BuildModel()
        {
            return new CourseWorkGradeModel
            {
                Id = _id,
                CourseGrade = _courseGrade == null ? null : new CourseGradeModel{Id = _courseGrade.Id},
                CourseWork = _courseWork == null ? null : new CourseWorkModel { Id = _courseWork.Id },
                GradePercentage = _gradePercentage
            };
        }

        public CourseWorkGradeBuilder WithStudentId(int studentId)
        {
            _student.Id = studentId;
            return this;
        }

        public CourseWorkGradeBuilder WithCourseId(int courseId)
        {
            _course.Id = courseId;
            return this;
        }

        public CourseWorkGradeBuilder WithCourseWorkId(int courseWorkId)
        {
            _courseWork.Id = courseWorkId;
            return this;
        }

        public CourseWorkGradeBuilder WithId(int id)
        {
            _id = id;
            return this;
        }

        public CourseWorkGradeBuilder WithCourseGradeId(int courseGradeId)
        {
            _courseGrade.Id = courseGradeId;
            return this;
        }

        public CourseWorkGradeBuilder WithGradePercentage(int gradePercentage)
        {
            _gradePercentage = gradePercentage;
            return this;
        }

        public CourseWorkGradeBuilder WithNoCourseGrade()
        {
            _courseGrade = null;
            return this;
        }
    }
}