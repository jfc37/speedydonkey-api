using System;
using SpeedyDonkeyApi.Models;

namespace Models.Tests.Builders
{
    public class LectureBuilder
    {
        private int _id;
        private string _description;
        private DateTime _startDate;
        private DateTime _endDate;
        private Course _course;
        private string _location;
        private string _name;
        private Occurence _occurence;

        public Lecture Build()
        {
            return new Lecture
            {
                Id = _id,
                Course = _course,
                EndDate = _endDate,
                StartDate = _startDate,
                Name = _name,
                Description = _description,
                Location = _location,
                Occurence = _occurence
            };
        }

        public LectureModel BuildModel()
        {
            return new LectureModel
            {
                Id = _id,
                Course = _course == null ? null : new CourseModel{Id = _course.Id},
                EndDate = _endDate,
                StartDate = _startDate,
                Name = _name,
                Description = _description,
                Location = _location,
                Occurence = _occurence.ToString()
            };
        }

        public LectureBuilder WithId(int id)
        {
            _id = id;
            return this;
        }

        public LectureBuilder WithDescription(string description)
        {
            _description = description;
            return this;
        }

        public LectureBuilder WithStartDate(DateTime startDate)
        {
            _startDate = startDate;
            return this;
        }

        public LectureBuilder WithEndDate(DateTime endDate)
        {
            _endDate = endDate;
            return this;
        }

        public LectureBuilder WithCourse(Course course)
        {
            _course = course;
            return this;
        }

        public LectureBuilder WithNoCourse()
        {
            _course = null;
            return this;
        }

        public LectureBuilder WithValidInputs()
        {
            _description = "description";
            _name = "name";
            _startDate = new DateTime(2014,1,1);
            _endDate = new DateTime(2014,2,1);
            _occurence = Occurence.Monthly;
            return this;
        }

        public LectureBuilder WithLocation(string location)
        {
            _location = location;
            return this;
        }

        public LectureBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public LectureBuilder WithOccurence(Occurence occurence)
        {
            _occurence = occurence;
            return this;
        }
    }
}