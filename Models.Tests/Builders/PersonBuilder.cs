using System.Collections.Generic;
using System.Linq;
using SpeedyDonkeyApi.Models;

namespace Models.Tests.Builders
{
    public class PersonBuilder
    {
        private string _firstName;
        private string _surname;
        private User _user;
        private int _id;
        private IList<Course> _courses;
        private IList<CourseGrade> _courseGrades;

        public PersonBuilder()
        {
            _courses = new List<Course>();
            _courseGrades = new List<CourseGrade>();
        }

        public PersonBuilder WithValidInputs()
        {
            _firstName = "John";
            _surname = "Snow";
            _user = new User();
            _id = 654;
            return this;
        }

        public Student BuildStudent()
        {
            return new Student
            {
                FirstName = _firstName,
                Surname = _surname,
                User = _user,
                Id = _id,
                EnroledCourses = _courses,
                CourseGrades = _courseGrades
            };
        }

        public Professor BuildProfessor()
        {
            return new Professor
            {
                FirstName = _firstName,
                Surname = _surname,
                User = _user,
                Id = _id,
                Courses = _courses
            };
        }

        public StudentModel BuildStudentModel()
        {
            var studentModel = new StudentModel
            {
                FirstName = _firstName,
                Surname = _surname,
                Id = _id,
                Grades = _courseGrades.Select(x => new CourseGradeModel()).ToList(),
                EnroledCourses = _courses.Select(x => new CourseModel()).ToList()
            };

            if (_user != null)
            {
                studentModel.User = new UserModel
                {
                    Id = _user.Id
                };
            }

            return studentModel;
        }

        public ProfessorModel BuildProfessorModel()
        {
            var professorModel = new ProfessorModel
            {
                FirstName = _firstName,
                Surname = _surname,
                Id = _id
            };

            if (_user != null)
            {
                professorModel.User = new UserModel
                {
                    Id = _user.Id
                };
            }

            return professorModel;
        }

        public PersonBuilder WithFirstName(string firstName)
        {
            _firstName = firstName;
            return this;
        }

        public PersonBuilder WithSurname(string surname)
        {
            _surname = surname;
            return this;
        }

        public PersonBuilder WithUser(User user)
        {
            _user = user;
            return this;
        }

        public PersonBuilder WithNoUser()
        {
            _user = null;
            return this;
        }

        public PersonBuilder WithId(int id)
        {
            _id = id;
            return this;
        }

        public PersonBuilder WithCourse(Course course)
        {
            _courses.Add(course);
            return this;
        }

        public PersonBuilder WithNoCourses()
        {
            _courses.Clear();
            return this;
        }

        public PersonBuilder WithCourseGrade(CourseGrade courseGrade)
        {
            _courseGrades.Add(courseGrade);
            return this;
        }

        public PersonBuilder WithNoCourseGrades()
        {
            _courseGrades.Clear();
            return this;
        }
    }
}