using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Common.Tests;
using Common.Tests.Builders.MockBuilders;
using Models;
using Moq;

namespace Data.Tests.Builders.MockBuilders
{
    public class MockDbContextBuilder : MockBuilder<ISpeedyDonkeyDbContext>
    {
        private IDbSet<User> _users;
        private IDbSet<Person> _people;
        private IDbSet<Course> _courses;
        private IDbSet<Notice> _notices;
        private IDbSet<Lecture> _lectures;
        private IDbSet<CourseWork> _courseWork;
        private IDbSet<CourseGrade> _courseGrades;
        private IDbSet<CourseWorkGrade> _courseWorkGrades;

        public MockDbContextBuilder()
        {
            Mock.Setup(x => x.SaveChanges())
                .Returns(1);
            Mock.Setup(x => x.SetEntityState(It.IsAny<object>(), It.IsAny<EntityState>()));

            _users = new InMemoryDbSet<User>();
            _people = new InMemoryDbSet<Person>();
            _courses = new InMemoryDbSet<Course>();
            _notices = new InMemoryDbSet<Notice>();
            _lectures = new InMemoryDbSet<Lecture>();
            _courseWork = new InMemoryDbSet<CourseWork>();
            _courseGrades = new InMemoryDbSet<CourseGrade>();
            _courseWorkGrades = new InMemoryDbSet<CourseWorkGrade>();

            Mock.Setup(x => x.GetSetOfType<User>())
                .Returns(_users);
        }

        protected override void BeforeBuild()
        {
            Mock.SetupGet(x => x.Users)
                .Returns(_users);

            Mock.SetupGet(x => x.People)
                .Returns(_people);

            Mock.SetupGet(x => x.Courses)
                .Returns(_courses);

            Mock.SetupGet(x => x.Notices)
                .Returns(_notices);

            Mock.SetupGet(x => x.Lectures)
                .Returns(_lectures);

            Mock.SetupGet(x => x.CourseWork)
                .Returns(_courseWork);

            Mock.SetupGet(x => x.CourseGrades)
                .Returns(_courseGrades);

            Mock.SetupGet(x => x.CourseWorkGrades)
                .Returns(_courseWorkGrades);
        }

        public MockDbContextBuilder WithNoUsers()
        {
            _users = new InMemoryDbSet<User>();
            return this;
        }

        public MockDbContextBuilder WithUser()
        {
            _users.Add(new User());
            return this;
        }

        public MockDbContextBuilder WithUser(User user)
        {
            _users.Add(user);
            return this;
        }

        public MockDbContextBuilder WithNoPeople()
        {
            _people = new InMemoryDbSet<Person>();
            return this;
        }

        public MockDbContextBuilder WithProfessor()
        {
            _people.Add(new Professor());
            return this;
        }

        public MockDbContextBuilder WithProfessor(Professor professor)
        {
            _people.Add(professor);
            return this;
        }

        public MockDbContextBuilder WithStudent()
        {
            _people.Add(new Student());
            return this;
        }

        public MockDbContextBuilder WithStudent(Student student)
        {
            _people.Add(student);
            return this;
        }

        public MockDbContextBuilder WithNoCourses()
        {
            _courses = new InMemoryDbSet<Course>();
            return this;
        }

        public MockDbContextBuilder WithCourse(Course course)
        {
            _courses.Add(course);
            return this;
        }

        public MockDbContextBuilder WithCourse()
        {
            _courses.Add(new Course());
            return this;
        }

        public MockDbContextBuilder WithNoNotices()
        {
            _notices = new InMemoryDbSet<Notice>();
            return this;
        }

        public MockDbContextBuilder WithNotice(Notice notice)
        {
            _notices.Add(notice);
            return this;
        }

        public MockDbContextBuilder WithNoLectures()
        {
            _lectures = new InMemoryDbSet<Lecture>();
            return this;
        }

        public MockDbContextBuilder WithLecture(Lecture lecture)
        {
            _lectures.Add(lecture);
            return this;
        }

        public MockDbContextBuilder WithNoAssignments()
        {
            _courseWork = new InMemoryDbSet<CourseWork>();
            return this;
        }

        public MockDbContextBuilder WithCourseWork(CourseWork courseWork)
        {
            _courseWork.Add(courseWork);
            return this;
        }

        public MockDbContextBuilder WithNoExams()
        {
            _courseWork = new InMemoryDbSet<CourseWork>();
            return this;
        }

        public MockDbContextBuilder WithNoCourseGrades()
        {
            _courseGrades = new InMemoryDbSet<CourseGrade>();
            return this;
        }

        public MockDbContextBuilder WithCourseGrade(CourseGrade courseGrade)
        {
            _courseGrades.Add(courseGrade);
            return this;
        }

        public MockDbContextBuilder WithNoCourseWorkGrades()
        {
            _courseWorkGrades = new InMemoryDbSet<CourseWorkGrade>();
            return this;
        }

        public MockDbContextBuilder WithCourseWorkGrade(CourseWorkGrade courseWorkGrade)
        {
            _courseWorkGrades.Add(courseWorkGrade);
            return this;
        }
    }
}