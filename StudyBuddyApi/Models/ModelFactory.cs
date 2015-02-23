using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Net.Http;
using Models;
using SpeedyDonkeyApi.Services;
using WebGrease.Css.Extensions;

namespace SpeedyDonkeyApi.Models
{
    public interface IModelFactory
    {
        UserModel ToModel(HttpRequestMessage request, User user);
        PersonModel ToModel(HttpRequestMessage request, Person person);
        ProfessorModel ToModel(HttpRequestMessage request, Professor professor);
        StudentModel ToModel(HttpRequestMessage request, Student student);
        CourseModel ToModel(HttpRequestMessage request, Course course);
        CourseGradeModel ToModel(HttpRequestMessage request, CourseGrade courseGrade);
        CourseWorkGradeModel ToModel(HttpRequestMessage request, CourseWorkGrade courseWorkGrade);
        NoticeModel ToModel(HttpRequestMessage request, Notice notice);
        LectureModel ToModel(HttpRequestMessage request, Lecture lecture);
        AssignmentModel ToModel(HttpRequestMessage request, Assignment assignment);
        ExamModel ToModel(HttpRequestMessage request, Exam exam);
        User Parse(UserModel userModel);
        Person Parse(PersonModel personModel);
        Professor Parse(ProfessorModel professorModel);
        Student Parse(StudentModel studentModel);
        Course Parse(CourseModel courseModel);
        CourseWorkGrade Parse(CourseWorkGradeModel courseWorkGradeModel);
        Notice Parse(NoticeModel noticeModel);
        Lecture Parse(LectureModel lectureModel);
        Assignment Parse(AssignmentModel assignmentModel);
        Exam Parse(ExamModel examModel);
    }

    public class ModelFactory : IModelFactory
    {
        private readonly IUrlConstructor _urlConstructor;

        public ModelFactory(IUrlConstructor urlConstructor)
        {
            _urlConstructor = urlConstructor;
        }

        public UserModel ToModel(HttpRequestMessage request, User user)
        {
            if (user.Person != null)
            {
                user.Person.User = null;
            }

            return new UserModel
            {
                Url = _urlConstructor.Construct("UserApi", new { userId = user.Id }, request),
                Id = user.Id,
                Username = user.Username,
                Person = user.Person == null ? null : user.Person is Student ? (PersonModel) ToModel(request, user.Person as Student) : ToModel(request, user.Person as Professor)
            };
        }

        public PersonModel ToModel(HttpRequestMessage request, Person person)
        {
            PersonModel personModel = null;

            if (person is Professor)
                personModel = ToModel(request, person as Professor);
            else if (person is Student)
                personModel = ToModel(request, person as Student);

            return personModel;
        }

        public StudentModel ToModel(HttpRequestMessage request, Student student)
        {
            var studentModel = ToModel<StudentModel>(request, student);

            if (studentModel == null)
                return null;

            student.EnroledCourses.ForEach(x => x.Students.Clear());
            student.CourseGrades.ForEach(x => x.Student = null);

            studentModel.EnroledCourses = student.EnroledCourses.Select(x => ToModel(request, x))
                .ToList();
            studentModel.Grades = student.CourseGrades.Select(x => ToModel(request, x))
                .ToList();

            return studentModel;
        }

        public CourseGradeModel ToModel(HttpRequestMessage request, CourseGrade courseGrade)
        {
            if (courseGrade == null)
                return null;

            if (courseGrade.Student != null)
                courseGrade.Student.CourseGrades.Clear();
            if (courseGrade.CourseWorkGrades.Any())
                courseGrade.CourseWorkGrades.ForEach(x => x.CourseGrade = null);

            return new CourseGradeModel
            {
                Url = _urlConstructor.Construct("CourseGradeApi", new { courseId = courseGrade.Course.Id }, request),
                Id = courseGrade.Id,
                Course = ToModel(request, courseGrade.Course),
                Student = ToModel(request, courseGrade.Student),
                GradePercentage = courseGrade.GradePercentage,
                CourseWorkGrades = courseGrade.CourseWorkGrades.Select(x => ToModel(request, x)).ToList()
            };
        }

        public CourseWorkGradeModel ToModel(HttpRequestMessage request, CourseWorkGrade courseWorkGrade)
        {
            if (courseWorkGrade == null)
                return null;

            if (courseWorkGrade.CourseGrade != null)
                courseWorkGrade.CourseGrade.CourseWorkGrades.Clear();

            return new CourseWorkGradeModel
            {
                Url = _urlConstructor.Construct("CourseWorkGradeApi", new { courseWorkId = courseWorkGrade.CourseWork.Id }, request),
                Id = courseWorkGrade.Id,
                CourseGrade = ToModel(request, courseWorkGrade.CourseGrade),
                CourseWork = ToModel(request, courseWorkGrade.CourseWork),
                GradePercentage = courseWorkGrade.GradePercentage,
            };
        }

        private CourseWorkModel ToModel(HttpRequestMessage request, CourseWork courseWork)
        {
            CourseWorkModel courseWorkModel = null;

            if (courseWork is Assignment)
                courseWorkModel = ToModel(request, courseWork as Assignment);
            else if (courseWork is Exam)
                courseWorkModel = ToModel(request, courseWork as Exam);

            return courseWorkModel;
        }

        public ProfessorModel ToModel(HttpRequestMessage request, Professor professor)
        {
            var professorModel = ToModel<ProfessorModel>(request, professor);

            professor.Courses.ForEach(x => x.Professors.Clear());
            professorModel.Courses = professor.Courses.Select(x => ToModel(request, x))
                .ToList();

            return professorModel;
        }

        private TModel ToModel<TModel>(HttpRequestMessage request, Person person) where TModel : PersonModel, new()
        {
            if (person == null)
            {
                return null;
            }

            if (person.User != null)
            {
                person.User.Person = null;
            }

            string routeName = GetRouteNameBasedOnPersonType(person);
            return new TModel
            {
                Url = _urlConstructor.Construct(routeName, new {personId = person.Id}, request),
                Id = person.Id,
                Surname = person.Surname,
                FirstName = person.FirstName,
                Role = person.Role.ToString(),

                User = person.User == null ? null : ToModel(request, person.User)
            };
        }

        private string GetRouteNameBasedOnPersonType(Person person)
        {
            string routeName = "StudentApi";

            if (person is Professor)
                routeName = "ProfessorApi";

            return routeName;
        }

        public CourseModel ToModel(HttpRequestMessage request, Course course)
        {
            if (course == null)
                return null;

            course.Professors.ForEach(x => x.Courses.Clear());
            course.Students.ForEach(x => x.EnroledCourses.Clear());
            course.Notices.ForEach(x => x.Course = null);
            course.Lectures.ForEach(x => x.Course = null);
            course.Assignments.ForEach(x => x.Course = null);
            course.Exams.ForEach(x => x.Course = null);

            return new CourseModel
            {
                Url = _urlConstructor.Construct("CourseApi", new { courseId = course.Id }, request),
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                EndDate = course.EndDate,
                StartDate = course.StartDate,
                Name = course.Name,
                GradeType = course.GradeType.ToString(),

                Professors = course.Professors.Select(x => ToModel(request, x)).ToList(),
                Students = course.Students.Select(x => ToModel(request, x)).ToList(),
                Notices = course.Notices.Select(x => ToModel(request, x)).ToList(),
                Lectures = course.Lectures.Select(x => ToModel(request, x)).ToList(),
                Assignments = course.Assignments.Select(x => ToModel(request, x)).ToList(),
                Exams = course.Exams.Select(x => ToModel(request, x)).ToList(),
            };
        }

        public NoticeModel ToModel(HttpRequestMessage request, Notice notice)
        {
            if (notice == null)
                return null;
            
            if (notice.Course != null)
                notice.Course.Notices.Clear();

            return new NoticeModel
            {
                Url = _urlConstructor.Construct("NoticeApi", new { noticeId = notice.Id }, request),
                Id = notice.Id,
                Message = notice.Message,
                EndDate = notice.EndDate,
                StartDate = notice.StartDate,

                Course = ToModel(request, notice.Course)
            };
        }

        public LectureModel ToModel(HttpRequestMessage request, Lecture lecture)
        {
            if (lecture == null)
                return null;
            
            if (lecture.Course != null)
                lecture.Course.Lectures.Clear();

            return new LectureModel
            {
                Url = _urlConstructor.Construct("LectureApi", new { lectureId = lecture.Id }, request),
                Id = lecture.Id,
                EndDate = lecture.EndDate,
                StartDate = lecture.StartDate,
                Description = lecture.Description,
                Location = lecture.Location,
                Name = lecture.Name,
                Occurence = lecture.Occurence.ToString(),

                Course = ToModel(request, lecture.Course)
            };
        }

        public AssignmentModel ToModel(HttpRequestMessage request, Assignment assignment)
        {
            if (assignment == null)
                return null;

            var assignmentModel = ToModel<AssignmentModel>(request, assignment);
            if (assignment.Course != null)
            {
                assignment.Course.Assignments.Clear();
                assignmentModel.Course = ToModel(request, assignment.Course);
            }
            assignmentModel.StartDate = assignment.StartDate;
            assignmentModel.EndDate = assignment.EndDate;
            assignmentModel.GradeType = assignment.GradeType.ToString();
            return assignmentModel;
        }

        public ExamModel ToModel(HttpRequestMessage request, Exam exam)
        {
            if (exam == null)
                return null;

            var examModel = ToModel<ExamModel>(request, exam);
            if (exam.Course != null)
            {
                exam.Course.Exams.Clear();
                examModel.Course = ToModel(request, exam.Course);
            }
            examModel.StartTime = exam.StartTime;
            examModel.Location = exam.Location;
            examModel.GradeType = exam.GradeType.ToString();
            return examModel;
        }

        private TModel ToModel<TModel>(HttpRequestMessage request, CourseWork courseWork) where TModel : CourseWorkModel, new()
        {
            string routeName = GetRouteNameBasedOnCourseWorkType(courseWork);
            return new TModel
            {
                Url = _urlConstructor.Construct(routeName, new { courseWorkId = courseWork.Id }, request),
                Id = courseWork.Id,
                Name = courseWork.Name,
                Description = courseWork.Description,
                FinalMarkPercentage = courseWork.FinalMarkPercentage
            };
        }

        private string GetRouteNameBasedOnCourseWorkType(CourseWork courseWork)
        {
            string routeName = "ExamApi";

            if (courseWork is Assignment)
                routeName = "AssignmentApi";

            return routeName;
        }

        public User Parse(UserModel userModel)
        {
            var user = new User();

            if (userModel.Id != default(int))
                user.Id = userModel.Id;

            if (userModel.Username != default(string))
                user.Username = userModel.Username;

            if (userModel.Password != default(string))
                user.Password = userModel.Password;

            return user;
        }

        public Person Parse(PersonModel personModel)
        {
            Person person = null;

            if (personModel is ProfessorModel)
                person = Parse(personModel as ProfessorModel);
            else if (personModel is StudentModel)
                person = Parse(personModel as StudentModel);

            return person;
        }

        public Professor Parse(ProfessorModel professorModel)
        {
            var professor = Parse<Professor>(professorModel);
            return professor;
        }

        public Student Parse(StudentModel studentModel)
        {
            var student = Parse<Student>(studentModel);

            if (studentModel.Grades.Any())
                student.CourseGrades = studentModel.Grades.Select(x => Parse(x)).ToList();

            if (studentModel.EnroledCourses.Any())
                student.EnroledCourses = studentModel.EnroledCourses.Select(x => Parse(x)).ToList();

            return student;
        }

        private CourseGrade Parse(CourseGradeModel courseGradeModel)
        {
            CourseGrade courseGrade = new CourseGrade();

            if (courseGradeModel.Course != null)
                courseGrade.Course = Parse(courseGradeModel.Course);

            if (courseGradeModel.GradePercentage != default(int))
                courseGrade.GradePercentage = courseGradeModel.GradePercentage;

            if (courseGradeModel.Id != default(int))
                courseGrade.Id = courseGradeModel.Id;

            if (courseGradeModel.Student != null)
                courseGrade.Student = Parse<Student>(courseGradeModel.Student);

            return courseGrade;
        }

        private CourseWork Parse(CourseWorkModel courseWorkModel)
        {
            CourseWork courseWork = new Assignment();

            if (courseWorkModel is AssignmentModel)
                courseWork = Parse(courseWorkModel as AssignmentModel);
            else if (courseWorkModel is ExamModel)
                courseWork = Parse(courseWorkModel as ExamModel);

            if (courseWorkModel.Id != default(int))
                courseWork.Id = courseWorkModel.Id;

            return courseWork;
        }

        private TPerson Parse<TPerson>(PersonModel personModel) where TPerson : Person, new()
        {
            var person = new TPerson();

            if (personModel.Id != default(int))
                person.Id = personModel.Id;

            if (personModel.FirstName != default(string))
                person.FirstName = personModel.FirstName;

            if (personModel.Surname != default(string))
                person.Surname = personModel.Surname;

            if (personModel.User != null)
            {
                person.User = new User
                {
                    Id = personModel.User.Id
                };
            }

            return person;
        }

        public Course Parse(CourseModel courseModel)
        {
            var course = new Course();

            if (courseModel.Id != default(int))
                course.Id = courseModel.Id;

            if (courseModel.Description != default(string))
                course.Description = courseModel.Description;

            if (courseModel.Name != default(string))
                course.Name = courseModel.Name;

            if (courseModel.Title != default(string))
                course.Title = courseModel.Title;

            if (courseModel.EndDate != default(DateTime))
                course.EndDate = courseModel.EndDate;

            if (courseModel.StartDate != default(DateTime))
                course.StartDate = courseModel.StartDate;

            if (courseModel.GradeType != default(string))
                course.GradeType = (GradeType)Enum.Parse(typeof(GradeType), courseModel.GradeType);

            if (courseModel.Professors.Any())
                courseModel.Professors.ForEach(x => course.Professors.Add(new Professor{Id = x.Id}));

            if (courseModel.Notices.Any())
                courseModel.Notices.ForEach(x => course.Notices.Add(new Notice { Id = x.Id }));

            if (courseModel.Lectures.Any())
                courseModel.Lectures.ForEach(x => course.Lectures.Add(new Lecture { Id = x.Id }));

            if (courseModel.Assignments.Any())
                courseModel.Assignments.ForEach(x => course.Assignments.Add(new Assignment { Id = x.Id }));

            if (courseModel.Exams.Any())
                courseModel.Exams.ForEach(x => course.Exams.Add(new Exam { Id = x.Id }));

            return course;
        }

        public CourseWorkGrade Parse(CourseWorkGradeModel courseWorkGradeModel)
        {
            var courseWorkGrade = new CourseWorkGrade();

            if (courseWorkGradeModel.GradePercentage != default(int))
                courseWorkGrade.GradePercentage = courseWorkGradeModel.GradePercentage;

            if (courseWorkGradeModel.CourseGrade != null)
                courseWorkGrade.CourseGrade = Parse(courseWorkGradeModel.CourseGrade);

            if (courseWorkGradeModel.CourseWork != null)
                courseWorkGrade.CourseWork = Parse(courseWorkGradeModel.CourseWork);

            return courseWorkGrade;
        }

        public Notice Parse(NoticeModel noticeModel)
        {
            var notice = new Notice();

            if (noticeModel.Id != default(int))
                notice.Id = noticeModel.Id;

            if (noticeModel.Message != default(string))
                notice.Message = noticeModel.Message;

            if (noticeModel.EndDate != default(DateTime))
                notice.EndDate = noticeModel.EndDate;

            if (noticeModel.StartDate != default(DateTime))
                notice.StartDate = noticeModel.StartDate;

            if (noticeModel.Course != null)
                notice.Course = new Course { Id = noticeModel.Course.Id };

            return notice;
        }

        public Lecture Parse(LectureModel lectureModel)
        {
            var lecture = new Lecture();

            if (lectureModel.Id != default(int))
                lecture.Id = lectureModel.Id;

            if (lectureModel.Description != default(string))
                lecture.Description = lectureModel.Description;

            if (lectureModel.Location != default(string))
                lecture.Location = lectureModel.Location;

            if (lectureModel.Name != default(string))
                lecture.Name = lectureModel.Name;

            if (lectureModel.Occurence != default(string))
                lecture.Occurence = (Occurence)Enum.Parse(typeof(Occurence), lectureModel.Occurence);

            if (lectureModel.EndDate != default(DateTime))
                lecture.EndDate = lectureModel.EndDate;

            if (lectureModel.StartDate != default(DateTime))
                lecture.StartDate = lectureModel.StartDate;

            if (lectureModel.Course != null)
                lecture.Course = new Course { Id = lectureModel.Course.Id };

            return lecture;
        }

        public Assignment Parse(AssignmentModel assignmentModel)
        {
            var assignment = new Assignment();

            if (assignmentModel.Id != default(int))
                assignment.Id = assignmentModel.Id;

            if (assignmentModel.Description != default(string))
                assignment.Description = assignmentModel.Description;

            if (assignmentModel.FinalMarkPercentage != default(int))
                assignment.FinalMarkPercentage = assignmentModel.FinalMarkPercentage;

            if (assignmentModel.GradeType != default(string))
            {
                GradeType gradeType;
                if (Enum.TryParse(assignmentModel.GradeType, true, out gradeType))
                    assignment.GradeType = gradeType;
            }

            if (assignmentModel.Name != default(string))
                assignment.Name = assignmentModel.Name;

            if (assignmentModel.EndDate != default(DateTime))
                assignment.EndDate = assignmentModel.EndDate;

            if (assignmentModel.StartDate != default(DateTime))
                assignment.StartDate = assignmentModel.StartDate;

            if (assignmentModel.Course != null)
                assignment.Course = new Course { Id = assignmentModel.Course.Id };

            return assignment;
        }

        public Exam Parse(ExamModel examModel)
        {
            var exam = new Exam();

            if (examModel.Id != default(int))
                exam.Id = examModel.Id;

            if (examModel.Description != default(string))
                exam.Description = examModel.Description;

            if (examModel.FinalMarkPercentage != default(int))
                exam.FinalMarkPercentage = examModel.FinalMarkPercentage;

            if (examModel.Name != default(string))
                exam.Name = examModel.Name;

            if (examModel.StartTime != default(DateTime))
                exam.StartTime = examModel.StartTime;

            if (examModel.Location != default(string))
                exam.Location = examModel.Location;

            if (examModel.GradeType != default(string))
            {
                GradeType gradeType;
                if (Enum.TryParse(examModel.GradeType, true, out gradeType))
                    exam.GradeType = gradeType;
            }

            if (examModel.Course != null)
                exam.Course = new Course { Id = examModel.Course.Id };

            return exam;
        }
    }
}