using System;
using SpeedyDonkeyApi.Models;

namespace Models.Tests.Builders
{
    public class NoticeBuilder
    {
        private int _id;
        private string _message;
        private DateTime _startDate;
        private DateTime _endDate;
        private Course _course;

        public Notice Build()
        {
            return new Notice
            {
                Id = _id,
                Course = _course,
                EndDate = _endDate,
                StartDate = _startDate,
                Message = _message
            };
        }

        public NoticeModel BuildModel()
        {
            return new NoticeModel
            {
                Id = _id,
                Course = _course == null ? null : new CourseModel{Id = _course.Id},
                EndDate = _endDate,
                StartDate = _startDate,
                Message = _message
            };
        }

        public NoticeBuilder WithId(int id)
        {
            _id = id;
            return this;
        }

        public NoticeBuilder WithMessage(string message)
        {
            _message = message;
            return this;
        }

        public NoticeBuilder WithStartDate(DateTime startDate)
        {
            _startDate = startDate;
            return this;
        }

        public NoticeBuilder WithEndDate(DateTime endDate)
        {
            _endDate = endDate;
            return this;
        }

        public NoticeBuilder WithCourse(Course course)
        {
            _course = course;
            return this;
        }

        public NoticeBuilder WithNoCourse()
        {
            _course = null;
            return this;
        }

        public NoticeBuilder WithValidInputs()
        {
            _message = "message";
            _startDate = new DateTime(2014,1,1);
            _endDate = new DateTime(2014,2,1);
            return this;
        }
    }
}