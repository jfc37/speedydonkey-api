using System;
using SpeedyDonkeyApi.Models;

namespace Models.Tests.Builders
{
    public class ExamBuilder
    {
        private int _id;
        private string _description;
        private DateTime _startTime;
        private string _location;
        private Course _course;
        private string _name;
        private int _finalMarkPercentage;
        private GradeType _gradeType;

        public Exam Build()
        {
            return new Exam
            {
                Id = _id,
                Course = _course,
                Location = _location,
                StartTime = _startTime,
                Name = _name,
                Description = _description,
                FinalMarkPercentage = _finalMarkPercentage,
                GradeType = _gradeType
            };
        }

        public ExamModel BuildModel()
        {
            return new ExamModel
            {
                Id = _id,
                Course = _course == null ? null : new CourseModel{Id = _course.Id},
                Location = _location,
                StartTime = _startTime,
                Name = _name,
                Description = _description,
                FinalMarkPercentage = _finalMarkPercentage,
                GradeType = _gradeType.ToString()
            };
        }

        public ExamBuilder WithId(int id)
        {
            _id = id;
            return this;
        }

        public ExamBuilder WithDescription(string description)
        {
            _description = description;
            return this;
        }

        public ExamBuilder WithStartTime(DateTime startDate)
        {
            _startTime = startDate;
            return this;
        }

        public ExamBuilder WithLocation(string location)
        {
            _location = location;
            return this;
        }

        public ExamBuilder WithCourse(Course course)
        {
            _course = course;
            return this;
        }

        public ExamBuilder WithNoCourse()
        {
            _course = null;
            return this;
        }

        public ExamBuilder WithValidInputs()
        {
            _description = "description";
            _name = "name";
            _startTime = DateTime.Now.AddDays(5);
            _location = "location";
            _finalMarkPercentage = 44;
            _gradeType = GradeType.Letter;
            return this;
        }

        public ExamBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public ExamBuilder WithFinalMarkPercentage(int finalMarkPercentage)
        {
            _finalMarkPercentage = finalMarkPercentage;
            return this;
        }

        public ExamBuilder WithGradeType(GradeType gradeType)
        {
            _gradeType = gradeType;
            return this;
        }
    }
}