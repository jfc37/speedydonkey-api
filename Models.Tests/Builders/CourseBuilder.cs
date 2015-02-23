using System;
using System.Collections.Generic;
using System.Linq;
using SpeedyDonkeyApi.Models;

namespace Models.Tests.Builders
{
    public class CourseBuilder
    {
        private string _name;
        private DateTime _startDate;
        private DateTime _endDate;
        private GradeType _gradeType;
        private int _id;
        private string _description;
        private IList<Professor> _professors;
        private IList<Notice> _notices;
        private IList<Lecture> _lectures;
        private IList<Assignment> _assignments;
        private IList<Exam> _exams;
        private IList<Student> _students;
        private string _title;

        public CourseBuilder()
        {
            _professors = new List<Professor>();
            _notices = new List<Notice>();
            _lectures = new List<Lecture>();
            _assignments = new List<Assignment>();
            _exams = new List<Exam>();
            _students = new List<Student>();
        }

        public CourseBuilder WithValidInputs()
        {
            _name = "course name";
            _startDate = DateTime.Today;
            _endDate = DateTime.Today.AddMonths(6);
            _gradeType = GradeType.Letter;
            _id = 565;
            _title = "course title";
            return this;
        }

        public Course Build()
        {
            return new Course
            {
                Name = _name,
                StartDate = _startDate,
                EndDate = _endDate,
                GradeType = _gradeType,
                Id = _id,
                Description = _description,
                Title = _title,
                Professors = _professors,
                Notices = _notices,
                Lectures = _lectures,
                Assignments = _assignments,
                Exams = _exams,
                Students = _students
            };
        }

        public CourseModel BuildModel()
        {
            var courseModel = new CourseModel
            {
                Name = _name,
                StartDate = _startDate,
                EndDate = _endDate,
                GradeType = _gradeType.ToString(),
                Id = _id,
                Title = _title,
                Description = _description,
                Notices = _notices.Select(x => new NoticeModel{Id = x.Id}).ToList(),
                Lectures = _lectures.Select(x => new LectureModel{Id = x.Id}).ToList(),
                Assignments = _assignments.Select(x => new AssignmentModel{Id = x.Id}).ToList(),
                Exams = _exams.Select(x => new ExamModel{Id = x.Id}).ToList(),
            };
            if (_professors != null)
            {
                courseModel.Professors = _professors.Select(x => new ProfessorModel {Id = x.Id}).ToList();
            }
            return courseModel;
        }

        public CourseBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public CourseBuilder WithStartDate(DateTime startDate)
        {
            _startDate = startDate;
            return this;
        }

        public CourseBuilder WithEndDate(DateTime endDate)
        {
            _endDate = endDate;
            return this;
        }

        public CourseBuilder WithGradeType(GradeType gradeType)
        {
            _gradeType = gradeType;
            return this;
        }

        public CourseBuilder WithId(int id)
        {
            _id = id;
            return this;
        }

        public CourseBuilder WithDescription(string description)
        {
            _description = description;
            return this;
        }

        public CourseBuilder WithProfessor(Professor professor)
        {
            _professors.Add(professor);
            return this;
        }

        public CourseBuilder WithNoProfessor()
        {
            _professors.Clear();
            return this;
        }

        public CourseBuilder WithNotice(Notice notice)
        {
            _notices.Add(notice);
            return this;
        }

        public CourseBuilder WithNoNotices()
        {
            _notices.Clear();
            return this;
        }

        public CourseBuilder WithLecture(Lecture lecture)
        {
            _lectures.Add(lecture);
            return this;
        }

        public CourseBuilder WithNoLectures()
        {
            _lectures.Clear();
            return this;
        }

        public CourseBuilder WithAssignment(Assignment assignment)
        {
            _assignments.Add(assignment);
            return this;
        }

        public CourseBuilder WithNoAssignments()
        {
            _assignments.Clear();
            return this;
        }

        public CourseBuilder WithExam(Exam exam)
        {
            _exams.Add(exam);
            return this;
        }

        public CourseBuilder WithNoExams()
        {
            _exams.Clear();
            return this;
        }

        public CourseBuilder WithStudent(Student student)
        {
            _students.Add(student);
            return this;
        }

        public CourseBuilder WithNoStudent()
        {
            _students.Clear();
            return this;
        }

        public CourseBuilder WithTitle(string title)
        {
            _title = title;
            return this;
        }
    }
}