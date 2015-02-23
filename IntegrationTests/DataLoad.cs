using System;
using ActionHandlers;
using Models;
using Newtonsoft.Json;
using NUnit.Framework;
using SpeedyDonkeyApi.Models;

namespace IntegrationTests
{
    [TestFixture]
    public class DataLoad
    {
        private ApiCaller _apiCaller;
        private Random _random;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _apiCaller = new ApiCaller();
            _random = new Random();
        }

        [Test]
        public void LoadUpTheData()
        {
            int userIdForProfessor = It_should_create_user("professor");
            int professorId = It_should_create_professor(userIdForProfessor);
            int courseIdForComp112 = It_should_create_course_for_professor(
                professorId,
                "COMP112",
                "Introduction to Computer Science",
                "This course introduces a range of important concepts and topics across Computer Science, Software Engineering and Network Engineering. Students will also gain a solid foundation of programming skills in object oriented programming. The course is an entry point to the BE(Hons) and BSc in Computer Science for students who already have basic programming skills.");
            int courseIdForComp308 = It_should_create_course_for_professor(
                professorId,
                "COMP308", 
                "Introduction to Computer Graphics",
                "Introduction to graphics programming. Graphics APIs, in particular OpenGL. Graphics processing pipeline (geometry processing, viewing, projection, transformation, illumination, texture mapping). Display hardware. Graphics cards. Image formats. Colour theory. NWEN 241 recommended.");
            int courseIdForComp102 = It_should_create_course_for_professor(
                professorId, 
                "COMP102", 
                "Introduction to Computer Program Design",
                "This course introduces the fundamentals of programming in a high-level programming language (Java), using an object oriented approach to program design. Students develop their programming skills by constructing computer programs for a variety of applications. The course provides a foundation for all later courses in computer science, and develops programming skills useful for students in many other disciplines.");
            int courseIdForLaws355 = It_should_create_course_for_professor(
                professorId, 
                "LAWS355", 
                "Employment Law",
                "The law governing the relationship between employers and employees, individually and collectively, and their collective organisations; and selected aspects of the law governing the operation of workplaces. 100% internal assessment.");
            
            It_should_create_notice_for_course(courseIdForComp112, "Welcome students to COMP 112!");
            It_should_create_notice_for_course(courseIdForComp308, "Monday's class will be held in KIRK202.");

            It_should_create_lecture_for_course(courseIdForComp112, DateTime.Today.AddHours(10), "LABY101" );
            It_should_create_lecture_for_course(courseIdForComp112, DateTime.Today.AddDays(2).AddHours(12), "KIRK103" );
            It_should_create_lecture_for_course(courseIdForComp112, DateTime.Today.AddDays(3).AddHours(15).AddMinutes(35), "LABY101" );

            It_should_create_lecture_for_course(courseIdForComp308, DateTime.Today.AddHours(11), "KIRK303" );
            It_should_create_lecture_for_course(courseIdForComp308, DateTime.Today.AddDays(4).AddHours(9), "LABY111");
            It_should_create_lecture_for_course(courseIdForComp308, DateTime.Today.AddDays(5).AddHours(13).AddMinutes(35), "HUNT100");

            It_should_create_lecture_for_course(courseIdForComp102, DateTime.Today.AddDays(1).AddHours(9), "LABY111");
            It_should_create_lecture_for_course(courseIdForComp102, DateTime.Today.AddDays(4).AddHours(13).AddMinutes(35), "HUNT100");

            It_should_create_lecture_for_course(courseIdForLaws355, DateTime.Today.AddDays(1).AddHours(8), "LABY111");
            It_should_create_lecture_for_course(courseIdForLaws355, DateTime.Today.AddDays(4).AddHours(13).AddMinutes(35), "HUNT100");

            It_should_create_assignment_for_course(courseIdForComp112, "Assignment 1", DateTime.Today.AddDays(7), 10);
            It_should_create_assignment_for_course(courseIdForComp112, "Assignment 2", DateTime.Today.AddDays(21), 10);
            It_should_create_assignment_for_course(courseIdForComp112, "Assignment 3", DateTime.Today.AddDays(42), 10);

            It_should_create_assignment_for_course(courseIdForComp102, "Read chapter 1", DateTime.Today.AddDays(7), 0);

            It_should_create_assignment_for_course(courseIdForLaws355, "Essay 1", DateTime.Today.AddDays(4), 30);


            It_should_create_exam_for_course(courseIdForComp112, "Midterm Test", DateTime.Today.AddMonths(1).AddDays(17).AddHours(13), "KIRK222", 20);
            It_should_create_exam_for_course(courseIdForComp112, "Final Exam", DateTime.Today.AddMonths(3).AddDays(21).AddHours(13), "HUNT100", 50);

            It_should_create_exam_for_course(courseIdForComp308, "Midterm Test", DateTime.Today.AddMonths(1).AddDays(14).AddHours(13), "HUNT100", 40);
            It_should_create_exam_for_course(courseIdForComp308, "Final Exam", DateTime.Today.AddMonths(3).AddDays(25).AddHours(13), "KIRK222", 60);

            It_should_create_exam_for_course(courseIdForComp102, "Midterm Test", DateTime.Today.AddMonths(1).AddDays(2).AddHours(13), "LABY111", 30);
            It_should_create_exam_for_course(courseIdForComp102, "Final Exam", DateTime.Today.AddMonths(3).AddDays(8).AddHours(13), "KIRK222", 70);

            It_should_create_exam_for_course(courseIdForComp112, "Final Exam", DateTime.Today.AddMonths(3).AddDays(5).AddHours(13), "LABY111", 70);

            int userIdForStudent = It_should_create_user("student");
            int studentId = It_should_create_student_for_user(userIdForStudent);
            It_should_enrol_student_in_course(studentId, courseIdForComp112);
            It_should_enrol_student_in_course(studentId, courseIdForComp102);
        }

        private int It_should_create_user(string username)
        {
            //Post to Users
            var newUser = new UserModel { Username = username, Password = "password"};
            var returnedUser = _apiCaller.POST<ActionReponse<User>>("users", _apiCaller.SerializeObject(newUser));
            Assert.IsTrue(returnedUser.ValidationResult.IsValid);

            AddCredentials(newUser.Username, newUser.Password);

            //Get User, ensure is created
            var retrievedUser = _apiCaller.GET<UserModel>(String.Format("users/{0}", returnedUser.ActionResult.Id)).Item2;
            Assert.AreEqual(returnedUser.ActionResult.Username, retrievedUser.Username);

            return returnedUser.ActionResult.Id;
        }

        private void AddCredentials(string username, string password)
        {
            _apiCaller.AddCredentials(username, password);
        }

        private int It_should_create_professor(int userId)
        {
            //Post to Professor
            var newProfessor = new ProfessorModel
            {
                FirstName = "Richard",
                Surname = "Belding"
            };
            var returnedProfessor =
                _apiCaller.POST<ActionReponse<ProfessorModel>>(
                    String.Format("users/{0}/professors", userId),
                    _apiCaller.SerializeObject(newProfessor));
            Assert.IsTrue(returnedProfessor.ValidationResult.IsValid);

            //Get Professor, ensure is created
            var retrievedProfessor = _apiCaller.GET<ProfessorModel>(String.Format("professors/{0}", returnedProfessor.ActionResult.Id)).Item2;
            Assert.AreEqual(returnedProfessor.ActionResult.FirstName, retrievedProfessor.FirstName);

            return returnedProfessor.ActionResult.Id;
        }

        private int It_should_create_student_for_user(int userId)
        {
            //Post to Student
            var newStudent = new StudentModel
            {
                FirstName = "Zack", 
                Surname = "Morris"
            };
            var returnedStudent =
                _apiCaller.POST<ActionReponse<StudentModel>>(
                    String.Format("users/{0}/students", userId),
                    _apiCaller.SerializeObject(newStudent));
            Assert.IsTrue(returnedStudent.ValidationResult.IsValid);

            //Get Student, ensure is created
            var retrievedStudent = _apiCaller.GET<ProfessorModel>(String.Format("students/{0}", returnedStudent.ActionResult.Id)).Item2;
            Assert.AreEqual(returnedStudent.ActionResult.FirstName, retrievedStudent.FirstName);

            return returnedStudent.ActionResult.Id;
        }

        private int It_should_create_course_for_professor(int professorId, string courseName, string courseTitle, string courseDescription)
        {
            //Post to Courses
            var newCourse = new CourseModel
            {
                Name = courseName,
                Description = courseDescription,
                Title = courseTitle,
                GradeType = GradeType.Letter.ToString(),
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddMonths(3)
            };
            var returnedCourse =
                _apiCaller.POST<ActionReponse<CourseModel>>(
                    String.Format("professors/{0}/courses", professorId),
                    _apiCaller.SerializeObject(newCourse));
            Assert.IsTrue(returnedCourse.ValidationResult.IsValid);

            //Get course, ensure is created
            var retrievedCourse = _apiCaller.GET<CourseModel>(String.Format("courses/{0}", returnedCourse.ActionResult.Id)).Item2;
            Assert.AreEqual(returnedCourse.ActionResult.Name, retrievedCourse.Name);

            return returnedCourse.ActionResult.Id;
        }

        private int It_should_create_notice_for_course(int courseId, string message)
        {
            //Post to Notices
            var newNotice = new NoticeModel
            {
                Message = message,
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(2)
            };
            var returnedNotice =
                _apiCaller.POST<ActionReponse<NoticeModel>>(
                    String.Format("courses/{0}/notices", courseId),
                    _apiCaller.SerializeObject(newNotice));
            Assert.IsTrue(returnedNotice.ValidationResult.IsValid);

            var retrievedNotice = _apiCaller.GET<NoticeModel>(String.Format("courses/{0}/notices/{1}", courseId, returnedNotice.ActionResult.Id)).Item2;
            Assert.AreEqual(returnedNotice.ActionResult.Message, retrievedNotice.Message);

            return returnedNotice.ActionResult.Id;
        }

        private int It_should_create_lecture_for_course(int courseId, DateTime startDate, string location)
        {
            //Post to Lectures
            var newLecture = new LectureModel
            {
                Name = String.Format("{0} class", startDate.ToString("dddd")),
                StartDate = startDate,
                EndDate = startDate.AddMonths(3),
                Location = location,
                Occurence = "Weekly"
            };
            var returnedLecture =
                _apiCaller.POST<ActionReponse<LectureModel>>(
                    String.Format("courses/{0}/lectures", courseId),
                    _apiCaller.SerializeObject(newLecture));
            Assert.IsTrue(returnedLecture.ValidationResult.IsValid);

            var retrievedLecture = _apiCaller.GET<LectureModel>(String.Format("courses/{0}/lectures/{1}", courseId, returnedLecture.ActionResult.Id)).Item2;
            Assert.AreEqual(returnedLecture.ActionResult.Name, retrievedLecture.Name);

            return returnedLecture.ActionResult.Id;
        }

        private int It_should_create_assignment_for_course(int courseId, string name, DateTime startDate, int gradeWeighting)
        {
            //Post to Assignments
            var newAssignment = new AssignmentModel
            {
                Name = name,
                StartDate = startDate,
                EndDate = startDate.AddDays(14),
                FinalMarkPercentage = gradeWeighting,
                GradeType = GradeType.Letter.ToString()
            };
            var returnedAssignment =
                _apiCaller.POST<ActionReponse<AssignmentModel>>(
                    String.Format("courses/{0}/assignments", courseId),
                    _apiCaller.SerializeObject(newAssignment));
            Assert.IsTrue(returnedAssignment.ValidationResult.IsValid);

            var retrievedAssignment = _apiCaller.GET<AssignmentModel>(String.Format("courses/{0}/assignments/{1}", courseId, returnedAssignment.ActionResult.Id)).Item2;
            Assert.AreEqual(returnedAssignment.ActionResult.Name, retrievedAssignment.Name);

            return returnedAssignment.ActionResult.Id;
        }

        private int It_should_create_exam_for_course(int courseId, string name, DateTime startTime, string location, int gradeWeighting)
        {
            //Post to Exams
            var newExam = new ExamModel
            {
                Name = name,
                StartTime = startTime,
                Location = location,
                FinalMarkPercentage = gradeWeighting,
                GradeType = GradeType.Letter.ToString()
            };
            var returnedExam =
                _apiCaller.POST<ActionReponse<ExamModel>>(
                    String.Format("courses/{0}/exams", courseId),
                    _apiCaller.SerializeObject(newExam));
            Assert.IsTrue(returnedExam.ValidationResult.IsValid);

            var retrievedExam = _apiCaller.GET<ExamModel>(String.Format("courses/{0}/exams/{1}", courseId, returnedExam.ActionResult.Id)).Item2;
            Assert.AreEqual(returnedExam.ActionResult.Name, retrievedExam.Name);

            return returnedExam.ActionResult.Id;
        }

        private int It_should_enrol_student_in_course(int studentId, int courseId)
        {
            var returnedStudent =
                _apiCaller.POST<ActionReponse<StudentModel>>(
                String.Format("students/{0}/courses/{1}", studentId, courseId), "");
            Assert.IsTrue(returnedStudent.ValidationResult.IsValid);
            Assert.IsNotEmpty(returnedStudent.ActionResult.EnroledCourses);

            var retrievedStudent = _apiCaller.GET<StudentModel>(String.Format("students/{0}", studentId)).Item2;
            Assert.IsNotEmpty(retrievedStudent.EnroledCourses);

            var retrievedCourse = _apiCaller.GET<CourseModel>(String.Format("courses/{0}", courseId)).Item2;
            Assert.IsNotEmpty(retrievedCourse.Students);

            return returnedStudent.ActionResult.Id;
        }
    }
}
