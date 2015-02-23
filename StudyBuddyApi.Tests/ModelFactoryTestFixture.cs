using System;
using System.Linq;
using System.Net.Http;
using Models;
using Models.Tests.Builders;
using NUnit.Framework;
using SpeedyDonkeyApi.Models;
using SpeedyDonkeyApi.Tests.Builders;
using SpeedyDonkeyApi.Tests.Builders.MockBuilders;

namespace SpeedyDonkeyApi.Tests
{
    [TestFixture]
    public class ModelFactoryTestFixture
    {
        private ModelFactoryBuilder _modelFactoryBuilder;
        private MockUrlConstructorBuilder _urlConstructorBuilder;
        private HttpRequestMessage _httpRequestMessage;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _urlConstructorBuilder = new MockUrlConstructorBuilder()
                .WithUrlConstruction();
            _modelFactoryBuilder = new ModelFactoryBuilder();
            _httpRequestMessage = new HttpRequestMessage();
        }

        private ModelFactory BuildModelFactory()
        {
            return _modelFactoryBuilder
                .WithUrlConstructor(_urlConstructorBuilder.BuildObject())
                .Build();
        }

        public class UserToModel : ModelFactoryTestFixture
        {
            private UserBuilder _userBuilder;

            [SetUp]
            public void Setup()
            {
                _userBuilder = new UserBuilder();
            }

            [Test]
            public void It_should_map_id()
            {
                _userBuilder.WithId(44);

                var modelFactory = BuildModelFactory();
                var userModel = modelFactory.ToModel(_httpRequestMessage, _userBuilder.Build());

                Assert.AreEqual(_userBuilder.Build().Id, userModel.Id);
            }

            [Test]
            public void It_should_map_surname()
            {
                _userBuilder.WithUsername("username");

                var modelFactory = BuildModelFactory();
                var userModel = modelFactory.ToModel(_httpRequestMessage, _userBuilder.Build());

                Assert.AreEqual(_userBuilder.Build().Username, userModel.Username);
            }

            [Test]
            public void It_should_not_map_password()
            {
                _userBuilder.WithPassword("password");

                var modelFactory = BuildModelFactory();
                var userModel = modelFactory.ToModel(_httpRequestMessage, _userBuilder.Build());

                Assert.AreNotEqual(_userBuilder.Build().Password, userModel.Password);
            }

            [Test]
            public void It_should_map_an_url()
            {
                _urlConstructorBuilder.WithUrlConstruction();

                var modelFactory = BuildModelFactory();
                var userModel = modelFactory.ToModel(_httpRequestMessage, _userBuilder.Build());

                Assert.IsNotEmpty(userModel.Url);
            }

            [Test]
            public void It_should_map_person_if_included()
            {
                _userBuilder.WithPerson(new Student());

                var modelFactory = BuildModelFactory();
                var userModel = modelFactory.ToModel(_httpRequestMessage, _userBuilder.Build());

                Assert.IsNotNull(userModel.Person);
            }

            [Test]
            public void It_should_not_map_person_if_not_included()
            {
                _userBuilder.WithPerson(null);

                var modelFactory = BuildModelFactory();
                var userModel = modelFactory.ToModel(_httpRequestMessage, _userBuilder.Build());

                Assert.IsNull(userModel.Person);
            }
        }

        public class PersonToModel : ModelFactoryTestFixture
        {
            private PersonBuilder _personBuilder;

            [SetUp]
            public void Setup()
            {
                _personBuilder = new PersonBuilder();
            }

            [Test]
            public void It_should_return_a_student_when_given_a_student_model()
            {
                _personBuilder.WithValidInputs();

                var modelFactory = BuildModelFactory();
                var personModel = modelFactory.ToModel(_httpRequestMessage, _personBuilder.BuildStudent() as Person);

                Assert.IsTrue(personModel is StudentModel);
            }

            [Test]
            public void It_should_return_a_professor_when_given_a_professor_model()
            {
                _personBuilder.WithValidInputs();

                var modelFactory = BuildModelFactory();
                var personModel = modelFactory.ToModel(_httpRequestMessage, _personBuilder.BuildProfessor() as Person);

                Assert.IsTrue(personModel is ProfessorModel);
            }

        }

        public class ProfessorToModel : ModelFactoryTestFixture
        {
            private PersonBuilder _personBuilder;

            [SetUp]
            public void Setup()
            {
                _personBuilder = new PersonBuilder();
            }

            [Test]
            public void It_should_map_id()
            {
                _personBuilder.WithId(44);

                var modelFactory = BuildModelFactory();
                var personModel = modelFactory.ToModel(_httpRequestMessage, _personBuilder.BuildProfessor());

                Assert.AreEqual(_personBuilder.BuildStudent().Id, personModel.Id);
            }

            [Test]
            public void It_should_map_surname()
            {
                _personBuilder.WithSurname("surname");

                var modelFactory = BuildModelFactory();
                var personModel = modelFactory.ToModel(_httpRequestMessage, _personBuilder.BuildProfessor());

                Assert.AreEqual(_personBuilder.BuildStudent().Surname, personModel.Surname);
            }

            [Test]
            public void It_should_map_first_name()
            {
                _personBuilder.WithFirstName("firstname");

                var modelFactory = BuildModelFactory();
                var personModel = modelFactory.ToModel(_httpRequestMessage, _personBuilder.BuildProfessor());

                Assert.AreEqual(_personBuilder.BuildStudent().FirstName, personModel.FirstName);
            }

            [Test]
            public void It_should_map_role()
            {
                var modelFactory = BuildModelFactory();
                var personModel = modelFactory.ToModel(_httpRequestMessage, _personBuilder.BuildProfessor());

                Assert.AreEqual(_personBuilder.BuildProfessor().Role.ToString(), personModel.Role);
            }

            [Test]
            public void It_should_map_an_url()
            {
                _urlConstructorBuilder.WithUrlConstruction();

                var modelFactory = BuildModelFactory();
                var personModel = modelFactory.ToModel(_httpRequestMessage, _personBuilder.BuildProfessor());

                Assert.IsNotEmpty(personModel.Url);
            }

            [Test]
            public void It_should_map_courses_if_included()
            {
                _personBuilder.WithCourse(new Course());

                var modelFactory = BuildModelFactory();
                var personModel = modelFactory.ToModel(_httpRequestMessage, _personBuilder.BuildProfessor());

                Assert.IsNotEmpty(personModel.Courses);
            }

            [Test]
            public void It_should_not_map_courses_if_not_included()
            {
                _personBuilder.WithNoCourses();

                var modelFactory = BuildModelFactory();
                var personModel = modelFactory.ToModel(_httpRequestMessage, _personBuilder.BuildProfessor());

                Assert.IsEmpty(personModel.Courses);
            }

            [Test]
            public void It_should_map_user_if_included()
            {
                _personBuilder.WithUser(new User());

                var modelFactory = BuildModelFactory();
                var personModel = modelFactory.ToModel(_httpRequestMessage, _personBuilder.BuildProfessor());

                Assert.IsNotNull(personModel.User);
            }

            [Test]
            public void It_should_not_map_user_if_not_included()
            {
                _personBuilder.WithNoUser();

                var modelFactory = BuildModelFactory();
                var personModel = modelFactory.ToModel(_httpRequestMessage, _personBuilder.BuildProfessor());

                Assert.IsNull(personModel.User);
            }
        }

        public class StudentToModel : ModelFactoryTestFixture
        {
            private PersonBuilder _personBuilder;

            [SetUp]
            public void Setup()
            {
                _personBuilder = new PersonBuilder();
            }

            [Test]
            public void It_should_map_id()
            {
                _personBuilder.WithId(44);

                var modelFactory = BuildModelFactory();
                var personModel = modelFactory.ToModel(_httpRequestMessage, _personBuilder.BuildStudent());

                Assert.AreEqual(_personBuilder.BuildStudent().Id, personModel.Id);
            }

            [Test]
            public void It_should_map_surname()
            {
                _personBuilder.WithSurname("surname");

                var modelFactory = BuildModelFactory();
                var personModel = modelFactory.ToModel(_httpRequestMessage, _personBuilder.BuildStudent());

                Assert.AreEqual(_personBuilder.BuildStudent().Surname, personModel.Surname);
            }

            [Test]
            public void It_should_map_first_name()
            {
                _personBuilder.WithFirstName("firstname");

                var modelFactory = BuildModelFactory();
                var personModel = modelFactory.ToModel(_httpRequestMessage, _personBuilder.BuildStudent());

                Assert.AreEqual(_personBuilder.BuildStudent().FirstName, personModel.FirstName);
            }

            [Test]
            public void It_should_map_role()
            {
                var modelFactory = BuildModelFactory();
                var personModel = modelFactory.ToModel(_httpRequestMessage, _personBuilder.BuildStudent());

                Assert.AreEqual(_personBuilder.BuildStudent().Role.ToString(), personModel.Role);
            }

            [Test]
            public void It_should_map_an_url()
            {
                _urlConstructorBuilder.WithUrlConstruction();

                var modelFactory = BuildModelFactory();
                var personModel = modelFactory.ToModel(_httpRequestMessage, _personBuilder.BuildStudent());

                Assert.IsNotEmpty(personModel.Url);
            }

            [Test]
            public void It_should_map_courses_if_included()
            {
                _personBuilder.WithCourse(new Course());

                var modelFactory = BuildModelFactory();
                var personModel = modelFactory.ToModel(_httpRequestMessage, _personBuilder.BuildStudent());

                Assert.IsNotEmpty(personModel.EnroledCourses);
            }

            [Test]
            public void It_should_not_map_courses_if_not_included()
            {
                _personBuilder.WithNoCourses();

                var modelFactory = BuildModelFactory();
                var personModel = modelFactory.ToModel(_httpRequestMessage, _personBuilder.BuildStudent());

                Assert.IsEmpty(personModel.EnroledCourses);
            }

            [Test]
            public void It_should_map_user_if_included()
            {
                _personBuilder.WithUser(new User());

                var modelFactory = BuildModelFactory();
                var personModel = modelFactory.ToModel(_httpRequestMessage, _personBuilder.BuildStudent());

                Assert.IsNotNull(personModel.User);
            }

            [Test]
            public void It_should_not_map_user_if_not_included()
            {
                _personBuilder.WithNoUser();

                var modelFactory = BuildModelFactory();
                var personModel = modelFactory.ToModel(_httpRequestMessage, _personBuilder.BuildStudent());

                Assert.IsNull(personModel.User);
            }

            [Test]
            public void It_should_map_grades_if_included()
            {
                _personBuilder.WithCourseGrade(new CourseGrade{Course = new Course(), Student = new Student{EnroledCourses = new []{new Course() }}});

                var modelFactory = BuildModelFactory();
                var personModel = modelFactory.ToModel(_httpRequestMessage, _personBuilder.BuildStudent());

                Assert.IsNotEmpty(personModel.Grades);
            }

            [Test]
            public void It_should_not_map_grades_if_not_included()
            {
                _personBuilder.WithNoCourseGrades();

                var modelFactory = BuildModelFactory();
                var personModel = modelFactory.ToModel(_httpRequestMessage, _personBuilder.BuildStudent());

                Assert.IsEmpty(personModel.Grades);
            }
        }

        public class CourseToModel : ModelFactoryTestFixture
        {
            private CourseBuilder _courseBuilder;

            [SetUp]
            public void Setup()
            {
                _courseBuilder = new CourseBuilder();
            }

            [Test]
            public void It_should_map_an_url()
            {
                _urlConstructorBuilder.WithUrlConstruction();

                var modelFactory = BuildModelFactory();
                var courseModel = modelFactory.ToModel(_httpRequestMessage, _courseBuilder.Build());

                Assert.IsNotEmpty(courseModel.Url);
            }

            [Test]
            public void It_should_map_id()
            {
                _courseBuilder.WithId(44);

                var modelFactory = BuildModelFactory();
                var courseModel = modelFactory.ToModel(_httpRequestMessage, _courseBuilder.Build());

                Assert.AreEqual(_courseBuilder.Build().Id, courseModel.Id);
            }

            [Test]
            public void It_should_map_name()
            {
                _courseBuilder.WithName("name");

                var modelFactory = BuildModelFactory();
                var courseModel = modelFactory.ToModel(_httpRequestMessage, _courseBuilder.Build());

                Assert.AreEqual(_courseBuilder.Build().Name, courseModel.Name);
            }

            [Test]
            public void It_should_map_title()
            {
                _courseBuilder.WithTitle("title");

                var modelFactory = BuildModelFactory();
                var courseModel = modelFactory.ToModel(_httpRequestMessage, _courseBuilder.Build());

                Assert.AreEqual(_courseBuilder.Build().Title, courseModel.Title);
            }

            [Test]
            public void It_should_map_description()
            {
                _courseBuilder.WithDescription("description");

                var modelFactory = BuildModelFactory();
                var courseModel = modelFactory.ToModel(_httpRequestMessage, _courseBuilder.Build());

                Assert.AreEqual(_courseBuilder.Build().Description, courseModel.Description);
            }

            [Test]
            public void It_should_map_start_date()
            {
                _courseBuilder.WithStartDate(DateTime.Today);

                var modelFactory = BuildModelFactory();
                var courseModel = modelFactory.ToModel(_httpRequestMessage, _courseBuilder.Build());

                Assert.AreEqual(_courseBuilder.Build().StartDate, courseModel.StartDate);
            }

            [Test]
            public void It_should_map_end_date()
            {
                _courseBuilder.WithEndDate(DateTime.Today);

                var modelFactory = BuildModelFactory();
                var courseModel = modelFactory.ToModel(_httpRequestMessage, _courseBuilder.Build());

                Assert.AreEqual(_courseBuilder.Build().EndDate, courseModel.EndDate);
            }

            [Test]
            public void It_should_map_grade_type()
            {
                _courseBuilder.WithGradeType(GradeType.Letter);

                var modelFactory = BuildModelFactory();
                var courseModel = modelFactory.ToModel(_httpRequestMessage, _courseBuilder.Build());

                Assert.AreEqual(_courseBuilder.Build().GradeType.ToString(), courseModel.GradeType);
            }

            [Test]
            public void It_should_map_professors_if_included()
            {
                _courseBuilder.WithProfessor(new Professor());

                var modelFactory = BuildModelFactory();
                var courseModel = modelFactory.ToModel(_httpRequestMessage, _courseBuilder.Build());

                Assert.IsNotEmpty(courseModel.Professors);
            }

            [Test]
            public void It_should_not_map_professors_if_included()
            {
                _courseBuilder.WithNoProfessor();

                var modelFactory = BuildModelFactory();
                var courseModel = modelFactory.ToModel(_httpRequestMessage, _courseBuilder.Build());

                Assert.IsEmpty(courseModel.Professors);
            }

            [Test]
            public void It_should_map_students_if_included()
            {
                _courseBuilder.WithStudent(new Student());

                var modelFactory = BuildModelFactory();
                var courseModel = modelFactory.ToModel(_httpRequestMessage, _courseBuilder.Build());

                Assert.IsNotEmpty(courseModel.Students);
            }

            [Test]
            public void It_should_not_map_students_if_included()
            {
                _courseBuilder.WithNoStudent();

                var modelFactory = BuildModelFactory();
                var courseModel = modelFactory.ToModel(_httpRequestMessage, _courseBuilder.Build());

                Assert.IsEmpty(courseModel.Students);
            }

            [Test]
            public void It_should_map_notices_if_included()
            {
                _courseBuilder.WithNotice(new Notice());

                var modelFactory = BuildModelFactory();
                var courseModel = modelFactory.ToModel(_httpRequestMessage, _courseBuilder.Build());

                Assert.IsNotEmpty(courseModel.Notices);
            }

            [Test]
            public void It_should_not_map_notices_if_included()
            {
                _courseBuilder.WithNoNotices();

                var modelFactory = BuildModelFactory();
                var courseModel = modelFactory.ToModel(_httpRequestMessage, _courseBuilder.Build());

                Assert.IsEmpty(courseModel.Notices);
            }

            [Test]
            public void It_should_map_lectures_if_included()
            {
                _courseBuilder.WithLecture(new Lecture());

                var modelFactory = BuildModelFactory();
                var courseModel = modelFactory.ToModel(_httpRequestMessage, _courseBuilder.Build());

                Assert.IsNotEmpty(courseModel.Lectures);
            }

            [Test]
            public void It_should_not_map_lectures_if_not_included()
            {
                _courseBuilder.WithNoLectures();

                var modelFactory = BuildModelFactory();
                var courseModel = modelFactory.ToModel(_httpRequestMessage, _courseBuilder.Build());

                Assert.IsEmpty(courseModel.Lectures);
            }

            [Test]
            public void It_should_map_assignments_if_included()
            {
                _courseBuilder.WithAssignment(new Assignment());

                var modelFactory = BuildModelFactory();
                var courseModel = modelFactory.ToModel(_httpRequestMessage, _courseBuilder.Build());

                Assert.IsNotEmpty(courseModel.Assignments);
            }

            [Test]
            public void It_should_not_map_assignments_if_not_included()
            {
                _courseBuilder.WithNoAssignments();

                var modelFactory = BuildModelFactory();
                var courseModel = modelFactory.ToModel(_httpRequestMessage, _courseBuilder.Build());

                Assert.IsEmpty(courseModel.Assignments);
            }

            [Test]
            public void It_should_map_exams_if_included()
            {
                _courseBuilder.WithExam(new Exam());

                var modelFactory = BuildModelFactory();
                var courseModel = modelFactory.ToModel(_httpRequestMessage, _courseBuilder.Build());

                Assert.IsNotEmpty(courseModel.Exams);
            }

            [Test]
            public void It_should_not_map_exams_if_not_included()
            {
                _courseBuilder.WithNoExams();

                var modelFactory = BuildModelFactory();
                var courseModel = modelFactory.ToModel(_httpRequestMessage, _courseBuilder.Build());

                Assert.IsEmpty(courseModel.Exams);
            }
        }

        public class CourseGradeToModel : ModelFactoryTestFixture
        {
            private CourseGradeBuilder _courseGradeBuilder;

            [SetUp]
            public void Setup()
            {
                _courseGradeBuilder = new CourseGradeBuilder()
                    .WithNoCourseWorkGrades()
                    .WithCourse(new Course());
            }

            [Test]
            public void It_should_map_an_url()
            {
                _urlConstructorBuilder.WithUrlConstruction();

                var modelFactory = BuildModelFactory();
                var courseGradeModel = modelFactory.ToModel(_httpRequestMessage, _courseGradeBuilder.Build());

                Assert.IsNotEmpty(courseGradeModel.Url);
            }

            [Test]
            public void It_should_map_id()
            {
                _courseGradeBuilder.WithId(44);

                var modelFactory = BuildModelFactory();
                var courseGradeModel = modelFactory.ToModel(_httpRequestMessage, _courseGradeBuilder.Build());

                Assert.AreEqual(_courseGradeBuilder.Build().Id, courseGradeModel.Id);
            }

            [Test]
            public void It_should_map_grade_percentage()
            {
                _courseGradeBuilder.WithGradePercentage(76);

                var modelFactory = BuildModelFactory();
                var courseGradeModel = modelFactory.ToModel(_httpRequestMessage, _courseGradeBuilder.Build());

                Assert.AreEqual(_courseGradeBuilder.Build().GradePercentage, courseGradeModel.GradePercentage);
            }

            [Test]
            public void It_should_map_course()
            {
                _courseGradeBuilder.WithCourse(new Course());

                var modelFactory = BuildModelFactory();
                var courseGradeModel = modelFactory.ToModel(_httpRequestMessage, _courseGradeBuilder.Build());

                Assert.IsNotNull(courseGradeModel.Course);
            }

            [Test]
            public void It_should_map_students_if_included()
            {
                _courseGradeBuilder.WithStudent(new Student());

                var modelFactory = BuildModelFactory();
                var courseGradeModel = modelFactory.ToModel(_httpRequestMessage, _courseGradeBuilder.Build());

                Assert.IsNotNull(courseGradeModel.Student);
            }

            [Test]
            public void It_should_not_map_student_if_not_included()
            {
                _courseGradeBuilder.WithNoStudent();

                var modelFactory = BuildModelFactory();
                var courseGradeModel = modelFactory.ToModel(_httpRequestMessage, _courseGradeBuilder.Build());

                Assert.IsNull(courseGradeModel.Student);
            }

            [Test]
            public void It_should_map_course_work_grades_if_included()
            {
                _courseGradeBuilder.WithCourseWorkGrades();

                var modelFactory = BuildModelFactory();
                var courseGradeModel = modelFactory.ToModel(_httpRequestMessage, _courseGradeBuilder.Build());

                Assert.IsNotEmpty(courseGradeModel.CourseWorkGrades);
            }

            [Test]
            public void It_should_not_map_course_work_grades_if_not_included()
            {
                _courseGradeBuilder.WithNoCourseWorkGrades();

                var modelFactory = BuildModelFactory();
                var courseGradeModel = modelFactory.ToModel(_httpRequestMessage, _courseGradeBuilder.Build());

                Assert.IsEmpty(courseGradeModel.CourseWorkGrades);
            }
        }

        public class CourseWorkGradeToModel : ModelFactoryTestFixture
        {
            private CourseWorkGradeBuilder _courseWorkGradeBuilder;

            [SetUp]
            public void Setup()
            {
                _courseWorkGradeBuilder = new CourseWorkGradeBuilder();
            }

            [Test]
            public void It_should_map_an_url()
            {
                _urlConstructorBuilder.WithUrlConstruction();

                var modelFactory = BuildModelFactory();
                var courseWorkGradeModel = modelFactory.ToModel(_httpRequestMessage, _courseWorkGradeBuilder.Build());

                Assert.IsNotEmpty(courseWorkGradeModel.Url);
            }

            [Test]
            public void It_should_map_id()
            {
                _courseWorkGradeBuilder.WithId(44);

                var modelFactory = BuildModelFactory();
                var courseWorkGradeModel = modelFactory.ToModel(_httpRequestMessage, _courseWorkGradeBuilder.Build());

                Assert.AreEqual(_courseWorkGradeBuilder.Build().Id, courseWorkGradeModel.Id);
            }

            [Test]
            public void It_should_map_grade_percentage()
            {
                _courseWorkGradeBuilder.WithGradePercentage(76);

                var modelFactory = BuildModelFactory();
                var courseWorkGradeModel = modelFactory.ToModel(_httpRequestMessage, _courseWorkGradeBuilder.Build());

                Assert.AreEqual(_courseWorkGradeBuilder.Build().GradePercentage, courseWorkGradeModel.GradePercentage);
            }

            [Test]
            public void It_should_map_course_grade()
            {
                _courseWorkGradeBuilder.WithCourseGradeId(1);

                var modelFactory = BuildModelFactory();
                var courseWorkGradeModel = modelFactory.ToModel(_httpRequestMessage, _courseWorkGradeBuilder.Build());

                Assert.IsNotNull(courseWorkGradeModel.CourseGrade);
            }

            [Test]
            public void It_should_map_course_work()
            {
                _courseWorkGradeBuilder.WithCourseWorkId(1);

                var modelFactory = BuildModelFactory();
                var courseWorkGradeModel = modelFactory.ToModel(_httpRequestMessage, _courseWorkGradeBuilder.Build());

                Assert.IsNotNull(courseWorkGradeModel.CourseWork);
            }

            [Test]
            public void It_should_map_course_work_when_it_is_an_exam()
            {
                var courseWorkGrade = _courseWorkGradeBuilder.Build();
                courseWorkGrade.CourseWork = new Exam();

                var modelFactory = BuildModelFactory();
                var courseWorkGradeModel = modelFactory.ToModel(_httpRequestMessage, courseWorkGrade);

                Assert.IsNotNull(courseWorkGradeModel.CourseWork);
            }

            [Test]
            public void It_should_map_course_work_when_it_is_an_assignment()
            {
                var courseWorkGrade = _courseWorkGradeBuilder.Build();
                courseWorkGrade.CourseWork = new Assignment();

                var modelFactory = BuildModelFactory();
                var courseWorkGradeModel = modelFactory.ToModel(_httpRequestMessage, courseWorkGrade);

                Assert.IsNotNull(courseWorkGradeModel.CourseWork);
            }

            [Test]
            public void It_should_return_null_when_given_null()
            {
                var modelFactory = BuildModelFactory();
                var courseWorkGradeModel = modelFactory.ToModel(_httpRequestMessage, (CourseWorkGrade) null);

                Assert.IsNull(courseWorkGradeModel);
            }
        }

        public class NoticeToModel : ModelFactoryTestFixture
        {
            private NoticeBuilder _noticeBuilder;

            [SetUp]
            public void Setup()
            {
                _noticeBuilder = new NoticeBuilder();
            }

            [Test]
            public void It_should_map_an_url()
            {
                _urlConstructorBuilder.WithUrlConstruction();

                var modelFactory = BuildModelFactory();
                var noticeModel = modelFactory.ToModel(_httpRequestMessage, _noticeBuilder.Build());

                Assert.IsNotEmpty(noticeModel.Url);
            }

            [Test]
            public void It_should_map_id()
            {
                _noticeBuilder.WithId(44);

                var modelFactory = BuildModelFactory();
                var noticeModel = modelFactory.ToModel(_httpRequestMessage, _noticeBuilder.Build());

                Assert.AreEqual(_noticeBuilder.Build().Id, noticeModel.Id);
            }

            [Test]
            public void It_should_map_message()
            {
                _noticeBuilder.WithMessage("message");

                var modelFactory = BuildModelFactory();
                var noticeModel = modelFactory.ToModel(_httpRequestMessage, _noticeBuilder.Build());

                Assert.AreEqual(_noticeBuilder.Build().Message, noticeModel.Message);
            }

            [Test]
            public void It_should_map_start_date()
            {
                _noticeBuilder.WithStartDate(DateTime.Today);

                var modelFactory = BuildModelFactory();
                var noticeModel = modelFactory.ToModel(_httpRequestMessage, _noticeBuilder.Build());

                Assert.AreEqual(_noticeBuilder.Build().StartDate, noticeModel.StartDate);
            }

            [Test]
            public void It_should_map_end_date()
            {
                _noticeBuilder.WithEndDate(DateTime.Today);

                var modelFactory = BuildModelFactory();
                var noticeModel = modelFactory.ToModel(_httpRequestMessage, _noticeBuilder.Build());

                Assert.AreEqual(_noticeBuilder.Build().EndDate, noticeModel.EndDate);
            }

            [Test]
            public void It_should_map_course_if_included()
            {
                _noticeBuilder.WithCourse(new Course());

                var modelFactory = BuildModelFactory();
                var noticeModel = modelFactory.ToModel(_httpRequestMessage, _noticeBuilder.Build());

                Assert.IsNotNull(noticeModel.Course);
            }

            [Test]
            public void It_should_not_map_course_if_not_included()
            {
                _noticeBuilder.WithNoCourse();

                var modelFactory = BuildModelFactory();
                var noticeModel = modelFactory.ToModel(_httpRequestMessage, _noticeBuilder.Build());

                Assert.IsNull(noticeModel.Course);
            }

            [Test]
            public void It_should_return_null_when_given_null()
            {
                var modelFactory = BuildModelFactory();
                var model = modelFactory.ToModel(_httpRequestMessage, (Notice)null);

                Assert.IsNull(model);
            }
        }

        public class LectureToModel : ModelFactoryTestFixture
        {
            private LectureBuilder _lectureBuilder;

            [SetUp]
            public void Setup()
            {
                _lectureBuilder = new LectureBuilder();
            }

            [Test]
            public void It_should_map_an_url()
            {
                _urlConstructorBuilder.WithUrlConstruction();

                var modelFactory = BuildModelFactory();
                var lectureModel = modelFactory.ToModel(_httpRequestMessage, _lectureBuilder.Build());

                Assert.IsNotEmpty(lectureModel.Url);
            }

            [Test]
            public void It_should_map_id()
            {
                _lectureBuilder.WithId(44);

                var modelFactory = BuildModelFactory();
                var lectureModel = modelFactory.ToModel(_httpRequestMessage, _lectureBuilder.Build());

                Assert.AreEqual(_lectureBuilder.Build().Id, lectureModel.Id);
            }

            [Test]
            public void It_should_map_description()
            {
                _lectureBuilder.WithDescription("description");

                var modelFactory = BuildModelFactory();
                var lectureModel = modelFactory.ToModel(_httpRequestMessage, _lectureBuilder.Build());

                Assert.AreEqual(_lectureBuilder.Build().Description, lectureModel.Description);
            }

            [Test]
            public void It_should_map_start_date()
            {
                _lectureBuilder.WithStartDate(DateTime.Today);

                var modelFactory = BuildModelFactory();
                var lectureModel = modelFactory.ToModel(_httpRequestMessage, _lectureBuilder.Build());

                Assert.AreEqual(_lectureBuilder.Build().StartDate, lectureModel.StartDate);
            }

            [Test]
            public void It_should_map_end_date()
            {
                _lectureBuilder.WithEndDate(DateTime.Today);

                var modelFactory = BuildModelFactory();
                var lectureModel = modelFactory.ToModel(_httpRequestMessage, _lectureBuilder.Build());

                Assert.AreEqual(_lectureBuilder.Build().EndDate, lectureModel.EndDate);
            }

            [Test]
            public void It_should_map_location()
            {
                _lectureBuilder.WithLocation("location");

                var modelFactory = BuildModelFactory();
                var lectureModel = modelFactory.ToModel(_httpRequestMessage, _lectureBuilder.Build());

                Assert.AreEqual(_lectureBuilder.Build().Location, lectureModel.Location);
            }

            [Test]
            public void It_should_map_name()
            {
                _lectureBuilder.WithName("name");

                var modelFactory = BuildModelFactory();
                var lectureModel = modelFactory.ToModel(_httpRequestMessage, _lectureBuilder.Build());

                Assert.AreEqual(_lectureBuilder.Build().Name, lectureModel.Name);
            }

            [Test]
            public void It_should_map_occurence()
            {
                _lectureBuilder.WithOccurence(Occurence.Monthly);

                var modelFactory = BuildModelFactory();
                var lectureModel = modelFactory.ToModel(_httpRequestMessage, _lectureBuilder.Build());

                Assert.AreEqual(_lectureBuilder.Build().Occurence.ToString(), lectureModel.Occurence);
            }

            [Test]
            public void It_should_map_course_if_included()
            {
                _lectureBuilder.WithCourse(new Course());

                var modelFactory = BuildModelFactory();
                var lectureModel = modelFactory.ToModel(_httpRequestMessage, _lectureBuilder.Build());

                Assert.IsNotNull(lectureModel.Course);
            }

            [Test]
            public void It_should_not_map_course_if_not_included()
            {
                _lectureBuilder.WithNoCourse();

                var modelFactory = BuildModelFactory();
                var lectureModel = modelFactory.ToModel(_httpRequestMessage, _lectureBuilder.Build());

                Assert.IsNull(lectureModel.Course);
            }

            [Test]
            public void It_should_return_null_when_given_null()
            {
                var modelFactory = BuildModelFactory();
                var model = modelFactory.ToModel(_httpRequestMessage, (Lecture)null);

                Assert.IsNull(model);
            }
        }

        public class AssignmentToModel : ModelFactoryTestFixture
        {
            private AssignmentBuilder _assignmentBuilder;

            [SetUp]
            public void Setup()
            {
                _assignmentBuilder = new AssignmentBuilder();
            }

            [Test]
            public void It_should_map_an_url()
            {
                _urlConstructorBuilder.WithUrlConstruction();

                var modelFactory = BuildModelFactory();
                var assignmentModel = modelFactory.ToModel(_httpRequestMessage, _assignmentBuilder.Build());

                Assert.IsNotEmpty(assignmentModel.Url);
            }

            [Test]
            public void It_should_map_id()
            {
                _assignmentBuilder.WithId(44);

                var modelFactory = BuildModelFactory();
                var assignmentModel = modelFactory.ToModel(_httpRequestMessage, _assignmentBuilder.Build());

                Assert.AreEqual(_assignmentBuilder.Build().Id, assignmentModel.Id);
            }

            [Test]
            public void It_should_map_description()
            {
                _assignmentBuilder.WithDescription("description");

                var modelFactory = BuildModelFactory();
                var assignmentModel = modelFactory.ToModel(_httpRequestMessage, _assignmentBuilder.Build());

                Assert.AreEqual(_assignmentBuilder.Build().Description, assignmentModel.Description);
            }

            [Test]
            public void It_should_map_start_date()
            {
                _assignmentBuilder.WithStartDate(DateTime.Today);

                var modelFactory = BuildModelFactory();
                var assignmentModel = modelFactory.ToModel(_httpRequestMessage, _assignmentBuilder.Build());

                Assert.AreEqual(_assignmentBuilder.Build().StartDate, assignmentModel.StartDate);
            }

            [Test]
            public void It_should_map_end_date()
            {
                _assignmentBuilder.WithEndDate(DateTime.Today);

                var modelFactory = BuildModelFactory();
                var assignmentModel = modelFactory.ToModel(_httpRequestMessage, _assignmentBuilder.Build());

                Assert.AreEqual(_assignmentBuilder.Build().EndDate, assignmentModel.EndDate);
            }

            [Test]
            public void It_should_map_final_mark_percentage()
            {
                _assignmentBuilder.WithFinalMarkPercentage(40);

                var modelFactory = BuildModelFactory();
                var assignmentModel = modelFactory.ToModel(_httpRequestMessage, _assignmentBuilder.Build());

                Assert.AreEqual(_assignmentBuilder.Build().FinalMarkPercentage, assignmentModel.FinalMarkPercentage);
            }

            [Test]
            public void It_should_map_name()
            {
                _assignmentBuilder.WithName("name");

                var modelFactory = BuildModelFactory();
                var assignmentModel = modelFactory.ToModel(_httpRequestMessage, _assignmentBuilder.Build());

                Assert.AreEqual(_assignmentBuilder.Build().Name, assignmentModel.Name);
            }

            [Test]
            public void It_should_map_grade_type()
            {
                _assignmentBuilder.WithGradeType(GradeType.Letter);

                var modelFactory = BuildModelFactory();
                var assignmentModel = modelFactory.ToModel(_httpRequestMessage, _assignmentBuilder.Build());

                Assert.AreEqual(_assignmentBuilder.Build().GradeType.ToString(), assignmentModel.GradeType);
            }

            [Test]
            public void It_should_map_course_if_included()
            {
                _assignmentBuilder.WithCourse(new Course());

                var modelFactory = BuildModelFactory();
                var assignmentModel = modelFactory.ToModel(_httpRequestMessage, _assignmentBuilder.Build());

                Assert.IsNotNull(assignmentModel.Course);
            }

            [Test]
            public void It_should_not_map_course_if_not_included()
            {
                _assignmentBuilder.WithNoCourse();

                var modelFactory = BuildModelFactory();
                var assignmentModel = modelFactory.ToModel(_httpRequestMessage, _assignmentBuilder.Build());

                Assert.IsNull(assignmentModel.Course);
            }

            [Test]
            public void It_should_return_null_when_given_null()
            {
                var modelFactory = BuildModelFactory();
                var model = modelFactory.ToModel(_httpRequestMessage, (Assignment)null);

                Assert.IsNull(model);
            }
        }

        public class ExamToModel : ModelFactoryTestFixture
        {
            private ExamBuilder _examBuilder;

            [SetUp]
            public void Setup()
            {
                _examBuilder = new ExamBuilder();
            }

            [Test]
            public void It_should_map_an_url()
            {
                _urlConstructorBuilder.WithUrlConstruction();

                var modelFactory = BuildModelFactory();
                var examModel = modelFactory.ToModel(_httpRequestMessage, _examBuilder.Build());

                Assert.IsNotEmpty(examModel.Url);
            }

            [Test]
            public void It_should_map_id()
            {
                _examBuilder.WithId(44);

                var modelFactory = BuildModelFactory();
                var examModel = modelFactory.ToModel(_httpRequestMessage, _examBuilder.Build());

                Assert.AreEqual(_examBuilder.Build().Id, examModel.Id);
            }

            [Test]
            public void It_should_map_description()
            {
                _examBuilder.WithDescription("description");

                var modelFactory = BuildModelFactory();
                var examModel = modelFactory.ToModel(_httpRequestMessage, _examBuilder.Build());

                Assert.AreEqual(_examBuilder.Build().Description, examModel.Description);
            }

            [Test]
            public void It_should_map_start_time()
            {
                _examBuilder.WithStartTime(DateTime.Today);

                var modelFactory = BuildModelFactory();
                var examModel = modelFactory.ToModel(_httpRequestMessage, _examBuilder.Build());

                Assert.AreEqual(_examBuilder.Build().StartTime, examModel.StartTime);
            }

            [Test]
            public void It_should_map_location()
            {
                _examBuilder.WithLocation("location");

                var modelFactory = BuildModelFactory();
                var examModel = modelFactory.ToModel(_httpRequestMessage, _examBuilder.Build());

                Assert.AreEqual(_examBuilder.Build().Location, examModel.Location);
            }

            [Test]
            public void It_should_map_final_mark_percentage()
            {
                _examBuilder.WithFinalMarkPercentage(40);

                var modelFactory = BuildModelFactory();
                var examModel = modelFactory.ToModel(_httpRequestMessage, _examBuilder.Build());

                Assert.AreEqual(_examBuilder.Build().FinalMarkPercentage, examModel.FinalMarkPercentage);
            }

            [Test]
            public void It_should_map_name()
            {
                _examBuilder.WithName("name");

                var modelFactory = BuildModelFactory();
                var examModel = modelFactory.ToModel(_httpRequestMessage, _examBuilder.Build());

                Assert.AreEqual(_examBuilder.Build().Name, examModel.Name);
            }

            [Test]
            public void It_should_map_grade_type()
            {
                _examBuilder.WithGradeType(GradeType.Percentage);

                var modelFactory = BuildModelFactory();
                var examModel = modelFactory.ToModel(_httpRequestMessage, _examBuilder.Build());

                Assert.AreEqual(_examBuilder.Build().GradeType.ToString(), examModel.GradeType);
            }

            [Test]
            public void It_should_map_course_if_included()
            {
                _examBuilder.WithCourse(new Course());

                var modelFactory = BuildModelFactory();
                var examModel = modelFactory.ToModel(_httpRequestMessage, _examBuilder.Build());

                Assert.IsNotNull(examModel.Course);
            }

            [Test]
            public void It_should_not_map_course_if_not_included()
            {
                _examBuilder.WithNoCourse();

                var modelFactory = BuildModelFactory();
                var examModel = modelFactory.ToModel(_httpRequestMessage, _examBuilder.Build());

                Assert.IsNull(examModel.Course);
            }

            [Test]
            public void It_should_return_null_when_given_null()
            {
                var modelFactory = BuildModelFactory();
                var model = modelFactory.ToModel(_httpRequestMessage, (Exam)null);

                Assert.IsNull(model);
            }
        }

        public class UserParse : ModelFactoryTestFixture
        {
            private UserBuilder _userBuilder;

            [SetUp]
            public void Setup()
            {
                _userBuilder = new UserBuilder();
            }

            [Test]
            public void It_should_map_id()
            {
                _userBuilder.WithId(33);

                var modelFactory = BuildModelFactory();
                var userModel = modelFactory.Parse(_userBuilder.BuildModel());

                Assert.AreEqual(_userBuilder.Build().Id, userModel.Id);
            }

            [Test]
            public void It_should_map_username()
            {
                _userBuilder.WithUsername("username");

                var modelFactory = BuildModelFactory();
                var userModel = modelFactory.Parse(_userBuilder.BuildModel());

                Assert.AreEqual(_userBuilder.Build().Username, userModel.Username);
            }

            [Test]
            public void It_should_map_password()
            {
                _userBuilder.WithPassword("password");

                var modelFactory = BuildModelFactory();
                var userModel = modelFactory.Parse(_userBuilder.BuildModel());

                Assert.AreEqual(_userBuilder.Build().Password, userModel.Password);
            }
        }

        public class PersonParse : ModelFactoryTestFixture
        {
            private PersonBuilder _personBuilder;

            [SetUp]
            public void Setup()
            {
                _personBuilder = new PersonBuilder();
            }

            [Test]
            public void It_should_return_a_student_when_given_a_student_model()
            {
                _personBuilder.WithValidInputs();

                var modelFactory = BuildModelFactory();
                var personModel = modelFactory.Parse(_personBuilder.BuildStudentModel() as PersonModel);

                Assert.IsTrue(personModel is Student);
            }

            [Test]
            public void It_should_return_a_professpr_when_given_a_professor_model()
            {
                _personBuilder.WithValidInputs();

                var modelFactory = BuildModelFactory();
                var personModel = modelFactory.Parse(_personBuilder.BuildProfessorModel() as PersonModel);

                Assert.IsTrue(personModel is Professor);
            }
        }

        public class StudentParse : ModelFactoryTestFixture
        {
            private PersonBuilder _personBuilder;

            [SetUp]
            public void Setup()
            {
                _personBuilder = new PersonBuilder();
            }

            [Test]
            public void It_should_map_surname()
            {
                _personBuilder.WithSurname("surname");

                var modelFactory = BuildModelFactory();
                var personModel = modelFactory.Parse(_personBuilder.BuildStudentModel());

                Assert.AreEqual(_personBuilder.BuildStudent().Surname, personModel.Surname);
            }

            [Test]
            public void It_should_map_first_name()
            {
                _personBuilder.WithFirstName("firstname");

                var modelFactory = BuildModelFactory();
                var personModel = modelFactory.Parse(_personBuilder.BuildStudentModel());

                Assert.AreEqual(_personBuilder.BuildStudent().FirstName, personModel.FirstName);
            }

            [Test]
            public void It_should_map_user_id()
            {
                _personBuilder.WithUser(new UserBuilder().WithId(353).Build());

                var modelFactory = BuildModelFactory();
                var personModel = modelFactory.Parse(_personBuilder.BuildStudentModel());

                Assert.AreEqual(_personBuilder.BuildStudent().User.Id, personModel.User.Id);
            }

            [Test]
            public void It_should_map_student_id()
            {
                _personBuilder.WithId(654);

                var modelFactory = BuildModelFactory();
                var personModel = modelFactory.Parse(_personBuilder.BuildStudentModel());

                Assert.AreEqual(_personBuilder.BuildStudent().Id, personModel.Id);
            }

            [Test]
            public void It_should_map_grades_if_included()
            {
                _personBuilder.WithCourseGrade(new CourseGrade());

                var modelFactory = BuildModelFactory();
                var personModel = modelFactory.Parse(_personBuilder.BuildStudentModel());

                Assert.IsNotEmpty(personModel.CourseGrades);
            }

            [Test]
            public void It_should_not_map_grades_if_not_included()
            {
                _personBuilder.WithNoCourseGrades();

                var modelFactory = BuildModelFactory();
                var personModel = modelFactory.Parse(_personBuilder.BuildStudentModel());

                Assert.IsEmpty(personModel.CourseGrades);
            }

            [Test]
            public void It_should_map_enroled_courses_if_included()
            {
                _personBuilder.WithCourse(new Course());

                var modelFactory = BuildModelFactory();
                var personModel = modelFactory.Parse(_personBuilder.BuildStudentModel());

                Assert.IsNotEmpty(personModel.EnroledCourses);
            }

            [Test]
            public void It_should_not_map_enroled_courses_if_not_included()
            {
                _personBuilder.WithNoCourses();

                var modelFactory = BuildModelFactory();
                var personModel = modelFactory.Parse(_personBuilder.BuildStudentModel());

                Assert.IsEmpty(personModel.EnroledCourses);
            }
        }

        public class CourseParse : ModelFactoryTestFixture
        {
            private CourseBuilder _courseBuilder;

            [SetUp]
            public void Setup()
            {
                _courseBuilder = new CourseBuilder();
            }

            [Test]
            public void It_should_map_id()
            {
                _courseBuilder.WithId(546);

                var modelFactory = BuildModelFactory();
                var course = modelFactory.Parse(_courseBuilder.BuildModel());

                Assert.AreEqual(_courseBuilder.Build().Id, course.Id);
            }

            [Test]
            public void It_should_map_name()
            {
                _courseBuilder.WithName("name");

                var modelFactory = BuildModelFactory();
                var course = modelFactory.Parse(_courseBuilder.BuildModel());

                Assert.AreEqual(_courseBuilder.Build().Name, course.Name);
            }

            [Test]
            public void It_should_map_title()
            {
                _courseBuilder.WithTitle("title");

                var modelFactory = BuildModelFactory();
                var course = modelFactory.Parse(_courseBuilder.BuildModel());

                Assert.AreEqual(_courseBuilder.Build().Title, course.Title);
            }

            [Test]
            public void It_should_map_description()
            {
                _courseBuilder.WithDescription("description");

                var modelFactory = BuildModelFactory();
                var course = modelFactory.Parse(_courseBuilder.BuildModel());

                Assert.AreEqual(_courseBuilder.Build().Description, course.Description);
            }

            [Test]
            public void It_should_map_start_date()
            {
                _courseBuilder.WithStartDate(DateTime.Today);

                var modelFactory = BuildModelFactory();
                var course = modelFactory.Parse(_courseBuilder.BuildModel());

                Assert.AreEqual(_courseBuilder.Build().StartDate, course.StartDate);
            }

            [Test]
            public void It_should_map_end_date()
            {
                _courseBuilder.WithEndDate(DateTime.Today);

                var modelFactory = BuildModelFactory();
                var course = modelFactory.Parse(_courseBuilder.BuildModel());

                Assert.AreEqual(_courseBuilder.Build().EndDate, course.EndDate);
            }

            [Test]
            public void It_should_map_grade_type()
            {
                _courseBuilder.WithGradeType(GradeType.Letter);

                var modelFactory = BuildModelFactory();
                var course = modelFactory.Parse(_courseBuilder.BuildModel());

                Assert.AreEqual(_courseBuilder.Build().GradeType, course.GradeType);
            }

            [Test]
            public void It_should_map_professors_if_included()
            {
                _courseBuilder.WithProfessor(new Professor{Id = 342});

                var modelFactory = BuildModelFactory();
                var course = modelFactory.Parse(_courseBuilder.BuildModel());

                Assert.IsNotEmpty(course.Professors);
                Assert.AreEqual(_courseBuilder.Build().Professors.Single().Id, course.Professors.Single().Id);
            }

            [Test]
            public void It_should_not_map_professors_if_included()
            {
                _courseBuilder.WithNoProfessor();

                var modelFactory = BuildModelFactory();
                var course = modelFactory.Parse(_courseBuilder.BuildModel());

                Assert.IsEmpty(course.Professors);
            }

            [Test]
            public void It_should_map_notices_if_included()
            {
                _courseBuilder.WithNotice(new Notice{Id = 342});

                var modelFactory = BuildModelFactory();
                var course = modelFactory.Parse(_courseBuilder.BuildModel());

                Assert.IsNotEmpty(course.Notices);
                Assert.AreEqual(_courseBuilder.Build().Notices.Single().Id, course.Notices.Single().Id);
            }

            [Test]
            public void It_should_not_map_notices_if_not_included()
            {
                _courseBuilder.WithNoNotices();

                var modelFactory = BuildModelFactory();
                var course = modelFactory.Parse(_courseBuilder.BuildModel());

                Assert.IsEmpty(course.Notices);
            }

            [Test]
            public void It_should_map_lectures_if_included()
            {
                _courseBuilder.WithLecture(new Lecture{Id = 342});

                var modelFactory = BuildModelFactory();
                var course = modelFactory.Parse(_courseBuilder.BuildModel());

                Assert.IsNotEmpty(course.Lectures);
                Assert.AreEqual(_courseBuilder.Build().Lectures.Single().Id, course.Lectures.Single().Id);
            }

            [Test]
            public void It_should_not_map_lectures_if_not_included()
            {
                _courseBuilder.WithNoLectures();

                var modelFactory = BuildModelFactory();
                var course = modelFactory.Parse(_courseBuilder.BuildModel());

                Assert.IsEmpty(course.Lectures);
            }

            [Test]
            public void It_should_map_assignments_if_included()
            {
                _courseBuilder.WithAssignment(new Assignment{Id = 342});

                var modelFactory = BuildModelFactory();
                var course = modelFactory.Parse(_courseBuilder.BuildModel());

                Assert.IsNotEmpty(course.Assignments);
                Assert.AreEqual(_courseBuilder.Build().Assignments.Single().Id, course.Assignments.Single().Id);
            }

            [Test]
            public void It_should_not_map_assignments_if_not_included()
            {
                _courseBuilder.WithNoAssignments();

                var modelFactory = BuildModelFactory();
                var course = modelFactory.Parse(_courseBuilder.BuildModel());

                Assert.IsEmpty(course.Assignments);
            }

            [Test]
            public void It_should_map_exams_if_included()
            {
                _courseBuilder.WithExam(new Exam{Id = 342});

                var modelFactory = BuildModelFactory();
                var course = modelFactory.Parse(_courseBuilder.BuildModel());

                Assert.IsNotEmpty(course.Exams);
                Assert.AreEqual(_courseBuilder.Build().Exams.Single().Id, course.Exams.Single().Id);
            }

            [Test]
            public void It_should_not_map_exams_if_not_included()
            {
                _courseBuilder.WithNoExams();

                var modelFactory = BuildModelFactory();
                var course = modelFactory.Parse(_courseBuilder.BuildModel());

                Assert.IsEmpty(course.Exams);
            }
        }

        public class CourseWorkGradeParse : ModelFactoryTestFixture
        {
            private CourseWorkGradeBuilder _courseWorkGradeBuilder;

            [SetUp]
            public void Setup()
            {
                _courseWorkGradeBuilder = new CourseWorkGradeBuilder();
            }

            [Test]
            public void It_should_map_id()
            {
                _courseWorkGradeBuilder.WithId(546);

                var modelFactory = BuildModelFactory();
                var course = modelFactory.Parse(_courseWorkGradeBuilder.BuildModel());

                Assert.AreEqual(_courseWorkGradeBuilder.Build().CourseGrade.Course.Id, course.Id);
            }

            [Test]
            public void It_should_map_grade_percentage()
            {
                _courseWorkGradeBuilder.WithGradePercentage(74);

                var modelFactory = BuildModelFactory();
                var course = modelFactory.Parse(_courseWorkGradeBuilder.BuildModel());

                Assert.AreEqual(_courseWorkGradeBuilder.Build().Id, course.Id);
            }

            [Test]
            public void It_should_map_course_grade_if_included()
            {
                _courseWorkGradeBuilder.WithCourseGradeId(342);

                var modelFactory = BuildModelFactory();
                var course = modelFactory.Parse(_courseWorkGradeBuilder.BuildModel());

                Assert.IsNotNull(course.CourseGrade);
                Assert.AreEqual(_courseWorkGradeBuilder.Build().CourseGrade.Id, course.CourseGrade.Id);
            }

            [Test]
            public void It_should_map_course_grade_properties_if_included()
            {
                var courseWorkGradeModel = new CourseWorkGradeModel
                {
                    CourseGrade = new CourseGradeModel
                    {
                        Student = new StudentModel(),
                        Course = new CourseModel(),
                        GradePercentage = 30
                    }
                };

                var modelFactory = BuildModelFactory();
                var course = modelFactory.Parse(courseWorkGradeModel);

                Assert.IsNotNull(course.CourseGrade);
                Assert.IsNotNull(course.CourseGrade.Student);
                Assert.IsNotNull(course.CourseGrade.Course);
                Assert.AreEqual(30, course.CourseGrade.GradePercentage);
            }

            [Test]
            public void It_should_not_map_course_grade_if_not_included()
            {
                _courseWorkGradeBuilder.WithNoCourseGrade();

                var modelFactory = BuildModelFactory();
                var course = modelFactory.Parse(_courseWorkGradeBuilder.BuildModel());

                Assert.IsNull(course.CourseGrade);
            }

            [Test]
            public void It_should_map_course_work_when_it_is_an_assignment()
            {
                var courseWorkGradeModel = new CourseWorkGradeModel
                {
                    CourseWork = new AssignmentModel()
                };

                var modelFactory = BuildModelFactory();
                var course = modelFactory.Parse(courseWorkGradeModel);

                Assert.IsTrue(course.CourseWork is Assignment);
            }

            [Test]
            public void It_should_map_course_work_when_it_is_an_exam()
            {
                var courseWorkGradeModel = new CourseWorkGradeModel
                {
                    CourseWork = new ExamModel()
                };

                var modelFactory = BuildModelFactory();
                var course = modelFactory.Parse(courseWorkGradeModel);

                Assert.IsTrue(course.CourseWork is Exam);
            }

            [Test]
            public void It_should_map_course_work_id()
            {
                var courseWorkGradeModel = new CourseWorkGradeModel
                {
                    CourseWork = new ExamModel
                    {
                        Id = 1
                    }
                };

                var modelFactory = BuildModelFactory();
                var course = modelFactory.Parse(courseWorkGradeModel);

                Assert.AreEqual(1, course.CourseWork.Id);
            }
        }

        public class NoticeParse : ModelFactoryTestFixture
        {
            private NoticeBuilder _noticeBuilder;

            [SetUp]
            public void Setup()
            {
                _noticeBuilder = new NoticeBuilder();
            }

            [Test]
            public void It_should_map_id()
            {
                _noticeBuilder.WithId(2);

                var modelFactory = BuildModelFactory();
                var notice = modelFactory.Parse(_noticeBuilder.BuildModel());

                Assert.AreEqual(_noticeBuilder.Build().Id, notice.Id);
            }

            [Test]
            public void It_should_map_start_date()
            {
                _noticeBuilder.WithStartDate(DateTime.Today);

                var modelFactory = BuildModelFactory();
                var notice = modelFactory.Parse(_noticeBuilder.BuildModel());

                Assert.AreEqual(_noticeBuilder.Build().StartDate, notice.StartDate);
            }

            [Test]
            public void It_should_map_end_date()
            {
                _noticeBuilder.WithEndDate(DateTime.Today);

                var modelFactory = BuildModelFactory();
                var notice = modelFactory.Parse(_noticeBuilder.BuildModel());

                Assert.AreEqual(_noticeBuilder.Build().EndDate, notice.EndDate);
            }

            [Test]
            public void It_should_map_message()
            {
                _noticeBuilder.WithMessage("message");

                var modelFactory = BuildModelFactory();
                var notice = modelFactory.Parse(_noticeBuilder.BuildModel());

                Assert.AreEqual(_noticeBuilder.Build().Message, notice.Message);
            }

            [Test]
            public void It_should_map_course_if_included()
            {
                _noticeBuilder.WithCourse(new Course{Id = 342});

                var modelFactory = BuildModelFactory();
                var notice = modelFactory.Parse(_noticeBuilder.BuildModel());

                Assert.IsNotNull(notice.Course);
                Assert.AreEqual(_noticeBuilder.Build().Course.Id, notice.Course.Id);
            }

            [Test]
            public void It_should_not_map_course_if_not_included()
            {
                _noticeBuilder.WithNoCourse();

                var modelFactory = BuildModelFactory();
                var notice = modelFactory.Parse(_noticeBuilder.BuildModel());

                Assert.IsNull(notice.Course);
            }
        }

        public class LectureParse : ModelFactoryTestFixture
        {
            private LectureBuilder _lectureBuilder;

            [SetUp]
            public void Setup()
            {
                _lectureBuilder = new LectureBuilder();
            }

            [Test]
            public void It_should_map_id()
            {
                _lectureBuilder.WithId(1);

                var modelFactory = BuildModelFactory();
                var lecture = modelFactory.Parse(_lectureBuilder.BuildModel());

                Assert.AreEqual(_lectureBuilder.Build().Id, lecture.Id);
            }

            [Test]
            public void It_should_map_start_date()
            {
                _lectureBuilder.WithStartDate(DateTime.Today);

                var modelFactory = BuildModelFactory();
                var lecture = modelFactory.Parse(_lectureBuilder.BuildModel());

                Assert.AreEqual(_lectureBuilder.Build().StartDate, lecture.StartDate);
            }

            [Test]
            public void It_should_map_end_date()
            {
                _lectureBuilder.WithEndDate(DateTime.Today);

                var modelFactory = BuildModelFactory();
                var lecture = modelFactory.Parse(_lectureBuilder.BuildModel());

                Assert.AreEqual(_lectureBuilder.Build().EndDate, lecture.EndDate);
            }

            [Test]
            public void It_should_map_description()
            {
                _lectureBuilder.WithDescription("description");

                var modelFactory = BuildModelFactory();
                var lecture = modelFactory.Parse(_lectureBuilder.BuildModel());

                Assert.AreEqual(_lectureBuilder.Build().Description, lecture.Description);
            }

            [Test]
            public void It_should_map_location()
            {
                _lectureBuilder.WithLocation("location");

                var modelFactory = BuildModelFactory();
                var lecture = modelFactory.Parse(_lectureBuilder.BuildModel());

                Assert.AreEqual(_lectureBuilder.Build().Location, lecture.Location);
            }

            [Test]
            public void It_should_map_occurence()
            {
                _lectureBuilder.WithOccurence(Occurence.Monthly);

                var modelFactory = BuildModelFactory();
                var lecture = modelFactory.Parse(_lectureBuilder.BuildModel());

                Assert.AreEqual(_lectureBuilder.Build().Occurence, lecture.Occurence);
            }

            [Test]
            public void It_should_map_name()
            {
                _lectureBuilder.WithName("name");

                var modelFactory = BuildModelFactory();
                var lecture = modelFactory.Parse(_lectureBuilder.BuildModel());

                Assert.AreEqual(_lectureBuilder.Build().Name, lecture.Name);
            }

            [Test]
            public void It_should_map_course_if_included()
            {
                _lectureBuilder.WithCourse(new Course{Id = 342});

                var modelFactory = BuildModelFactory();
                var lecture = modelFactory.Parse(_lectureBuilder.BuildModel());

                Assert.IsNotNull(lecture.Course);
                Assert.AreEqual(_lectureBuilder.Build().Course.Id, lecture.Course.Id);
            }

            [Test]
            public void It_should_not_map_course_if_not_included()
            {
                _lectureBuilder.WithNoCourse();

                var modelFactory = BuildModelFactory();
                var lecture = modelFactory.Parse(_lectureBuilder.BuildModel());

                Assert.IsNull(lecture.Course);
            }
        }

        public class AssignmentParse : ModelFactoryTestFixture
        {
            private AssignmentBuilder _assignmentBuilder;

            [SetUp]
            public void Setup()
            {
                _assignmentBuilder = new AssignmentBuilder();
            }

            [Test]
            public void It_should_map_id()
            {
                _assignmentBuilder.WithId(1);

                var modelFactory = BuildModelFactory();
                var assignment = modelFactory.Parse(_assignmentBuilder.BuildModel());

                Assert.AreEqual(_assignmentBuilder.Build().Id, assignment.Id);
            }

            [Test]
            public void It_should_map_start_date()
            {
                _assignmentBuilder.WithStartDate(DateTime.Today);

                var modelFactory = BuildModelFactory();
                var assignment = modelFactory.Parse(_assignmentBuilder.BuildModel());

                Assert.AreEqual(_assignmentBuilder.Build().StartDate, assignment.StartDate);
            }

            [Test]
            public void It_should_map_end_date()
            {
                _assignmentBuilder.WithEndDate(DateTime.Today);

                var modelFactory = BuildModelFactory();
                var assignment = modelFactory.Parse(_assignmentBuilder.BuildModel());

                Assert.AreEqual(_assignmentBuilder.Build().EndDate, assignment.EndDate);
            }

            [Test]
            public void It_should_map_description()
            {
                _assignmentBuilder.WithDescription("description");

                var modelFactory = BuildModelFactory();
                var assignment = modelFactory.Parse(_assignmentBuilder.BuildModel());

                Assert.AreEqual(_assignmentBuilder.Build().Description, assignment.Description);
            }

            [Test]
            public void It_should_map_name()
            {
                _assignmentBuilder.WithName("name");

                var modelFactory = BuildModelFactory();
                var assignment = modelFactory.Parse(_assignmentBuilder.BuildModel());

                Assert.AreEqual(_assignmentBuilder.Build().Name, assignment.Name);
            }

            [Test]
            public void It_should_map_grade_type()
            {
                _assignmentBuilder.WithGradeType(GradeType.Letter);

                var modelFactory = BuildModelFactory();
                var assignment = modelFactory.Parse(_assignmentBuilder.BuildModel());

                Assert.AreEqual(_assignmentBuilder.Build().GradeType, assignment.GradeType);
            }

            [Test]
            public void It_should_map_final_mark_percentage()
            {
                _assignmentBuilder.WithFinalMarkPercentage(40);

                var modelFactory = BuildModelFactory();
                var assignment = modelFactory.Parse(_assignmentBuilder.BuildModel());

                Assert.AreEqual(_assignmentBuilder.Build().FinalMarkPercentage, assignment.FinalMarkPercentage);
            }

            [Test]
            public void It_should_map_course_if_included()
            {
                _assignmentBuilder.WithCourse(new Course{Id = 342});

                var modelFactory = BuildModelFactory();
                var assignment = modelFactory.Parse(_assignmentBuilder.BuildModel());

                Assert.IsNotNull(assignment.Course);
                Assert.AreEqual(_assignmentBuilder.Build().Course.Id, assignment.Course.Id);
            }

            [Test]
            public void It_should_not_map_course_if_not_included()
            {
                _assignmentBuilder.WithNoCourse();

                var modelFactory = BuildModelFactory();
                var assignment = modelFactory.Parse(_assignmentBuilder.BuildModel());

                Assert.IsNull(assignment.Course);
            }
        }

        public class ExamParse : ModelFactoryTestFixture
        {
            private ExamBuilder _examBuilder;

            [SetUp]
            public void Setup()
            {
                _examBuilder = new ExamBuilder();
            }

            [Test]
            public void It_should_map_start_time()
            {
                _examBuilder.WithStartTime(DateTime.Today);

                var modelFactory = BuildModelFactory();
                var exam = modelFactory.Parse(_examBuilder.BuildModel());

                Assert.AreEqual(_examBuilder.Build().StartTime, exam.StartTime);
            }

            [Test]
            public void It_should_map_id()
            {
                _examBuilder.WithId(1);

                var modelFactory = BuildModelFactory();
                var exam = modelFactory.Parse(_examBuilder.BuildModel());

                Assert.AreEqual(_examBuilder.Build().Id, exam.Id);
            }

            [Test]
            public void It_should_map_location()
            {
                _examBuilder.WithLocation("location");

                var modelFactory = BuildModelFactory();
                var exam = modelFactory.Parse(_examBuilder.BuildModel());

                Assert.AreEqual(_examBuilder.Build().Location, exam.Location);
            }

            [Test]
            public void It_should_map_description()
            {
                _examBuilder.WithDescription("description");

                var modelFactory = BuildModelFactory();
                var exam = modelFactory.Parse(_examBuilder.BuildModel());

                Assert.AreEqual(_examBuilder.Build().Description, exam.Description);
            }

            [Test]
            public void It_should_map_name()
            {
                _examBuilder.WithName("name");

                var modelFactory = BuildModelFactory();
                var exam = modelFactory.Parse(_examBuilder.BuildModel());

                Assert.AreEqual(_examBuilder.Build().Name, exam.Name);
            }

            [Test]
            public void It_should_map_grade_type()
            {
                _examBuilder.WithGradeType(GradeType.Letter);

                var modelFactory = BuildModelFactory();
                var exam = modelFactory.Parse(_examBuilder.BuildModel());

                Assert.AreEqual(_examBuilder.Build().GradeType.ToString(), exam.GradeType.ToString());
            }

            [Test]
            public void It_should_map_final_mark_percentage()
            {
                _examBuilder.WithFinalMarkPercentage(40);

                var modelFactory = BuildModelFactory();
                var exam = modelFactory.Parse(_examBuilder.BuildModel());

                Assert.AreEqual(_examBuilder.Build().FinalMarkPercentage, exam.FinalMarkPercentage);
            }

            [Test]
            public void It_should_map_course_if_included()
            {
                _examBuilder.WithCourse(new Course{Id = 342});

                var modelFactory = BuildModelFactory();
                var exam = modelFactory.Parse(_examBuilder.BuildModel());

                Assert.IsNotNull(exam.Course);
                Assert.AreEqual(_examBuilder.Build().Course.Id, exam.Course.Id);
            }

            [Test]
            public void It_should_not_map_course_if_not_included()
            {
                _examBuilder.WithNoCourse();

                var modelFactory = BuildModelFactory();
                var exam = modelFactory.Parse(_examBuilder.BuildModel());

                Assert.IsNull(exam.Course);
            }
        }
    }
}
