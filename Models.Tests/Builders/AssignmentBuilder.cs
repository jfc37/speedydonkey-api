using System;
using SpeedyDonkeyApi.Models;

namespace Models.Tests.Builders
{
    public class AssignmentBuilder
    {
        private int _id;
        private string _description;
        private DateTime _startDate;
        private DateTime _endDate;
        private Course _course;
        private string _name;
        private int _finalMarkPercentage;
        private GradeType _gradeType;

        public Assignment Build()
        {
            return new Assignment
            {
                Id = _id,
                Course = _course,
                EndDate = _endDate,
                StartDate = _startDate,
                Name = _name,
                Description = _description,
                FinalMarkPercentage = _finalMarkPercentage,
                GradeType = _gradeType
            };
        }

        public AssignmentModel BuildModel()
        {
            return new AssignmentModel
            {
                Id = _id,
                Course = _course == null ? null : new CourseModel{Id = _course.Id},
                EndDate = _endDate,
                StartDate = _startDate,
                Name = _name,
                Description = _description,
                FinalMarkPercentage = _finalMarkPercentage,
                GradeType = _gradeType.ToString()
            };
        }

        public AssignmentBuilder WithId(int id)
        {
            _id = id;
            return this;
        }

        public AssignmentBuilder WithDescription(string description)
        {
            _description = description;
            return this;
        }

        public AssignmentBuilder WithStartDate(DateTime startDate)
        {
            _startDate = startDate;
            return this;
        }

        public AssignmentBuilder WithEndDate(DateTime endDate)
        {
            _endDate = endDate;
            return this;
        }

        public AssignmentBuilder WithCourse(Course course)
        {
            _course = course;
            return this;
        }

        public AssignmentBuilder WithNoCourse()
        {
            _course = null;
            return this;
        }

        public AssignmentBuilder WithValidInputs()
        {
            _description = "description";
            _name = "name";
            _startDate = DateTime.Now.AddMonths(-7);
            _endDate = DateTime.Now.AddMonths(2);
            _finalMarkPercentage = 44;
            _gradeType = GradeType.Letter;
            return this;
        }

        public AssignmentBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public AssignmentBuilder WithFinalMarkPercentage(int finalMarkPercentage)
        {
            _finalMarkPercentage = finalMarkPercentage;
            return this;
        }

        public AssignmentBuilder WithGradeType(GradeType gradeType)
        {
            _gradeType = gradeType;
            return this;
        }
    }
}