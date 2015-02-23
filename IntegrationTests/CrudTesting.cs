using System;
using System.Net;
using ActionHandlers;
using Models;
using NUnit.Framework;
using SpeedyDonkeyApi.Models;

namespace IntegrationTests
{
    [TestFixture]
    public class CrudTesting
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
        public void It_should_create_full_stack_of_entities_all_linked()
        {
            int userId = It_should_create_user();
            int professorId = It_should_create_professor_for_user(userId);
            int courseId = It_should_create_course_for_professor(professorId);
            int noticeId = It_should_create_notice_for_course(courseId);
            int lectureId = It_should_create_lecture_for_course(courseId);
            int assignmentId = It_should_create_assignment_for_course(courseId);
            int examId = It_should_create_exam_for_course(courseId);

            int userIdForStudent = It_should_create_user();
            int studentId = It_should_create_student_for_user(userIdForStudent);
            It_should_enrol_student_in_course(studentId, courseId);
            It_should_add_grade_to_course_work_for_student(studentId, courseId, assignmentId);
            It_should_add_grade_to_course_work_for_student(studentId, courseId, examId);
        }

        [Test]
        public void It_should_update_full_stack_of_entities_all_linked()
        {
            int userId = It_should_create_user();
            //It_should_update_user(userId);
            int userIdForStudent = It_should_create_user();
            //It_should_update_user(userIdForStudent);
            int professorId = It_should_create_professor_for_user(userId);
            It_should_update_professor(professorId);
            int studentId = It_should_create_student_for_user(userIdForStudent);
            It_should_update_student(studentId);
            int courseId = It_should_create_course_for_professor(professorId);
            It_should_update_course(courseId);
            int assignmentId = It_should_create_assignment_for_course(courseId);
            It_should_update_assignment(courseId, assignmentId);
            int examId = It_should_create_exam_for_course(courseId);
            It_should_update_exam(courseId, examId);
            int lectureId = It_should_create_lecture_for_course(courseId);
            It_should_update_lecture(courseId, lectureId);
            int noticeId = It_should_create_notice_for_course(courseId);
            It_should_update_notice(courseId, noticeId);
            It_should_enrol_student_in_course(studentId, courseId);
            It_should_add_grade_to_course_work_for_student(studentId, courseId, assignmentId);
            It_should_add_grade_to_course_work_for_student(studentId, courseId, examId);
            It_should_update_grade(studentId, courseId, assignmentId);
            It_should_update_grade(studentId, courseId, examId);
        }

        [Test]
        public void It_should_delete_full_stack_of_entities_all_linked()
        {
            int userId = It_should_create_user();
            int userIdForStudent = It_should_create_user();
            int professorId = It_should_create_professor_for_user(userId);
            int studentId = It_should_create_student_for_user(userIdForStudent);
            int courseId = It_should_create_course_for_professor(professorId);
            int assignmentId = It_should_create_assignment_for_course(courseId);
            int examId = It_should_create_exam_for_course(courseId);
            int lectureId = It_should_create_lecture_for_course(courseId);
            int noticeId = It_should_create_notice_for_course(courseId);
            It_should_enrol_student_in_course(studentId, courseId);
            It_should_add_grade_to_course_work_for_student(studentId, courseId, assignmentId);
            It_should_add_grade_to_course_work_for_student(studentId, courseId, examId);

            It_should_delete_grade(studentId, courseId, assignmentId);
            It_should_delete_grade(studentId, courseId, examId);
            It_should_unenrol_student_in_course(studentId, courseId);
            It_should_delete_notice(courseId, noticeId);
            It_should_delete_lecture(courseId, lectureId);
            It_should_delete_exam(courseId, examId);
            It_should_delete_assignment(courseId, assignmentId);
            It_should_delete_course(courseId);
            It_should_delete_student(studentId);
            It_should_delete_professor(professorId);
            It_should_delete_user(userId);
        }

        private int It_should_create_user()
        {
            //Post to Users
            var newUser = new UserModel { Username = "usertest" + _random.Next(100000), Password = "password"};
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

        private void It_should_update_user(int userId)
        {
            var retrievedUser = _apiCaller.GET<UserModel>(String.Format("users/{0}", userId)).Item2;
            string oldUsername = retrievedUser.Username;

            retrievedUser.Username = "updatedusertest" + _random.Next(100000);
            retrievedUser.Password = "password" + _random.Next(100000);
            var returnedUser = _apiCaller.PUT<ActionReponse<UserModel>>(String.Format("users/{0}", userId), _apiCaller.SerializeObject(retrievedUser));
            Assert.IsTrue(returnedUser.ValidationResult.IsValid);

            AddCredentials(retrievedUser.Username, retrievedUser.Password);

            //Get User, ensure is created
            var updatedUser = _apiCaller.GET<UserModel>(String.Format("users/{0}", userId)).Item2;
            Assert.AreNotEqual(oldUsername, updatedUser.Username);
        }

        private void It_should_update_course(int courseId)
        {
            var retrievedCourse = _apiCaller.GET<CourseModel>(String.Format("courses/{0}", courseId)).Item2;
            string oldDescription = retrievedCourse.Description;
            string oldName = retrievedCourse.Name;

            retrievedCourse.Description = "updated description" + _random.Next(100000);
            retrievedCourse.Name = "updated name " + _random.Next(100000);
            var returnedCourse = _apiCaller.PUT<ActionReponse<CourseModel>>(String.Format("courses/{0}", courseId), _apiCaller.SerializeObject(retrievedCourse));
            Assert.IsTrue(returnedCourse.ValidationResult.IsValid);

            var updatedCourse = _apiCaller.GET<CourseModel>(String.Format("courses/{0}", courseId)).Item2;
            Assert.AreNotEqual(oldDescription, updatedCourse.Description);
            Assert.AreNotEqual(oldName, updatedCourse.Name);
        }

        private void It_should_update_assignment(int courseId, int assignmentId)
        {
            var retrievedAssignment = _apiCaller.GET<AssignmentModel>(String.Format("courses/{0}/assignments/{1}", courseId, assignmentId)).Item2;
            string oldDescription = retrievedAssignment.Description;
            string oldName = retrievedAssignment.Name;
            DateTime oldEndDate = retrievedAssignment.EndDate;
            DateTime oldStartDate = retrievedAssignment.StartDate;
            int oldFinalMarkPercentage = retrievedAssignment.FinalMarkPercentage;

            retrievedAssignment.Description = "updated description" + _random.Next(100000);
            retrievedAssignment.Name = "updated name " + _random.Next(100000);
            retrievedAssignment.EndDate = retrievedAssignment.EndDate.AddMonths(1);
            retrievedAssignment.StartDate = retrievedAssignment.StartDate.AddMonths(1);
            retrievedAssignment.FinalMarkPercentage = retrievedAssignment.FinalMarkPercentage + 1;
            var returnedAssignment = _apiCaller.PUT<ActionReponse<AssignmentModel>>(String.Format("courses/{0}/assignments/{1}", courseId, assignmentId), _apiCaller.SerializeObject(retrievedAssignment));
            Assert.IsTrue(returnedAssignment.ValidationResult.IsValid);

            var updatedAssignment = _apiCaller.GET<AssignmentModel>(String.Format("courses/{0}/assignments/{1}", courseId, assignmentId)).Item2;
            Assert.AreNotEqual(oldDescription, updatedAssignment.Description);
            Assert.AreNotEqual(oldName, updatedAssignment.Name);
            Assert.AreNotEqual(oldEndDate, updatedAssignment.EndDate);
            Assert.AreNotEqual(oldStartDate, updatedAssignment.StartDate);
            Assert.AreNotEqual(oldFinalMarkPercentage, updatedAssignment.FinalMarkPercentage);
        }

        private void It_should_update_exam(int courseId, int examId)
        {
            var retrievedExam = _apiCaller.GET<ExamModel>(String.Format("courses/{0}/exams/{1}", courseId, examId)).Item2;
            string oldDescription = retrievedExam.Description;
            string oldName = retrievedExam.Name;
            DateTime oldStartTime = retrievedExam.StartTime;
            string oldLocation = retrievedExam.Location;
            int oldFinalMarkPercentage = retrievedExam.FinalMarkPercentage;

            retrievedExam.Description = "updated description" + _random.Next(100000);
            retrievedExam.Name = "updated name " + _random.Next(100000);
            retrievedExam.StartTime = retrievedExam.StartTime.AddMonths(1);
            retrievedExam.Location = "updated location";
            retrievedExam.FinalMarkPercentage = retrievedExam.FinalMarkPercentage + 1;
            var returnedExam = _apiCaller.PUT<ActionReponse<ExamModel>>(String.Format("courses/{0}/exams/{1}", courseId, examId), _apiCaller.SerializeObject(retrievedExam));
            Assert.IsTrue(returnedExam.ValidationResult.IsValid);

            var updatedExam = _apiCaller.GET<ExamModel>(String.Format("courses/{0}/exams/{1}", courseId, examId)).Item2;
            Assert.AreNotEqual(oldDescription, updatedExam.Description);
            Assert.AreNotEqual(oldName, updatedExam.Name);
            Assert.AreNotEqual(oldStartTime, updatedExam.StartTime);
            Assert.AreNotEqual(oldLocation, updatedExam.Location);
            Assert.AreNotEqual(oldFinalMarkPercentage, updatedExam.FinalMarkPercentage);
        }

        private void It_should_update_lecture(int courseId, int lectureId)
        {
            var retrievedLecture = _apiCaller.GET<LectureModel>(String.Format("courses/{0}/lectures/{1}", courseId, lectureId)).Item2;
            string oldDescription = retrievedLecture.Description;
            string oldName = retrievedLecture.Name;
            DateTime oldStartDate = retrievedLecture.StartDate;
            string oldLocation = retrievedLecture.Location;

            retrievedLecture.Description = "updated description" + _random.Next(100000);
            retrievedLecture.Name = "updated name " + _random.Next(100000);
            retrievedLecture.StartDate = retrievedLecture.StartDate.AddMonths(1);
            retrievedLecture.Location = "updated location";
            var returnedLecture = _apiCaller.PUT<ActionReponse<LectureModel>>(String.Format("courses/{0}/lectures/{1}", courseId, lectureId), _apiCaller.SerializeObject(retrievedLecture));
            Assert.IsTrue(returnedLecture.ValidationResult.IsValid);

            var updatedLecture = _apiCaller.GET<LectureModel>(String.Format("courses/{0}/lectures/{1}", courseId, lectureId)).Item2;
            Assert.AreNotEqual(oldDescription, updatedLecture.Description);
            Assert.AreNotEqual(oldName, updatedLecture.Name);
            Assert.AreNotEqual(oldStartDate, updatedLecture.StartDate);
            Assert.AreNotEqual(oldLocation, updatedLecture.Location);
        }

        private void It_should_update_notice(int courseId, int noticeId)
        {
            var retrievedNotice = _apiCaller.GET<NoticeModel>(String.Format("courses/{0}/notices/{1}", courseId, noticeId)).Item2;
            string oldMessage = retrievedNotice.Message;
            DateTime oldEndDate = retrievedNotice.EndDate;
            DateTime oldStartDate = retrievedNotice.StartDate;

            retrievedNotice.Message = "updated message " + _random.Next(100000);
            retrievedNotice.StartDate = retrievedNotice.StartDate.AddMonths(1);
            retrievedNotice.EndDate = retrievedNotice.EndDate.AddMonths(1);
            var returnedNotice = _apiCaller.PUT<ActionReponse<NoticeModel>>(String.Format("courses/{0}/notices/{1}", courseId, noticeId), _apiCaller.SerializeObject(retrievedNotice));
            Assert.IsTrue(returnedNotice.ValidationResult.IsValid);

            var updatedNotice = _apiCaller.GET<NoticeModel>(String.Format("courses/{0}/notices/{1}", courseId, noticeId)).Item2;
            Assert.AreNotEqual(oldMessage, updatedNotice.Message);
            Assert.AreNotEqual(oldEndDate, updatedNotice.EndDate);
            Assert.AreNotEqual(oldStartDate, updatedNotice.StartDate);
        }

        private void It_should_update_student(int studentId)
        {
            var retrievedStudent = _apiCaller.GET<StudentModel>(String.Format("students/{0}", studentId)).Item2;
            string oldFirstName = retrievedStudent.FirstName;
            string oldSurname = retrievedStudent.Surname;

            retrievedStudent.FirstName = "updated first name " + _random.Next(100000);
            retrievedStudent.Surname = "updated surname " + _random.Next(100000);
            var returnedStudent = _apiCaller.PUT<ActionReponse<StudentModel>>(String.Format("students/{0}", studentId), _apiCaller.SerializeObject(retrievedStudent));
            Assert.IsTrue(returnedStudent.ValidationResult.IsValid);

            var updatedStudent = _apiCaller.GET<StudentModel>(String.Format("students/{0}", studentId)).Item2;
            Assert.AreNotEqual(oldFirstName, updatedStudent.FirstName);
            Assert.AreNotEqual(oldSurname, updatedStudent.Surname);
        }

        private void It_should_update_professor(int professorId)
        {
            var retrievedProfessor = _apiCaller.GET<ProfessorModel>(String.Format("professors/{0}", professorId)).Item2;
            string oldFirstName = retrievedProfessor.FirstName;
            string oldSurname = retrievedProfessor.Surname;

            retrievedProfessor.FirstName = "updated first name " + _random.Next(100000);
            retrievedProfessor.Surname = "updated surname " + _random.Next(100000);
            var returnedProfessor = _apiCaller.PUT<ActionReponse<ProfessorModel>>(String.Format("professors/{0}", professorId), _apiCaller.SerializeObject(retrievedProfessor));
            Assert.IsTrue(returnedProfessor.ValidationResult.IsValid);

            var updatedProfessor = _apiCaller.GET<ProfessorModel>(String.Format("professors/{0}", professorId)).Item2;
            Assert.AreNotEqual(oldFirstName, updatedProfessor.FirstName);
            Assert.AreNotEqual(oldSurname, updatedProfessor.Surname);
        }

        private void It_should_delete_user(int userId)
        {
            var returnedUser = _apiCaller.DELETE<ActionReponse<UserModel>>(String.Format("users/{0}", userId));
            Assert.IsTrue(returnedUser.ValidationResult.IsValid);

            //Get User, ensure is deleted
            var statusCode = _apiCaller.GET<UserModel>(String.Format("users/{0}", userId)).Item1;
            Assert.AreEqual(HttpStatusCode.NotFound, statusCode);
        }

        private int It_should_create_professor_for_user(int userId)
        {
            //Post to Professor
            var newProfessor = new ProfessorModel { FirstName = "professortest" + _random.Next(100000), Surname = "Snow" + _random.Next(100000) };
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

        private void It_should_delete_course(int courseId)
        {
            var returnedCourse = _apiCaller.DELETE<ActionReponse<CourseModel>>(String.Format("courses/{0}", courseId));
            Assert.IsTrue(returnedCourse.ValidationResult.IsValid);

            //Get course, ensure is deleted
            var statusCode = _apiCaller.GET<CourseModel>(String.Format("courses/{0}", courseId)).Item1;
            Assert.AreEqual(HttpStatusCode.NotFound, statusCode);
        }

        private void It_should_delete_grade(int personId, int courseId, int courseWorkId)
        {
            var returnedCourseWorkGrade = _apiCaller.DELETE<ActionReponse<CourseWorkGradeModel>>(String.Format("students/{0}/grades/courses/{1}/course_work/{2}", personId, courseId, courseWorkId));
            Assert.IsTrue(returnedCourseWorkGrade.ValidationResult.IsValid);

            var statusCode = _apiCaller.GET<CourseWorkGradeModel>(String.Format("students/{0}/grades/courses/{1}/course_work/{2}", personId, courseId, courseWorkId)).Item1;
            Assert.AreEqual(HttpStatusCode.NotFound, statusCode);
        }

        private void It_should_delete_notice(int courseId, int noticeId)
        {
            var returnedLecture = _apiCaller.DELETE<ActionReponse<NoticeModel>>(String.Format("courses/{0}/notices/{1}", courseId, noticeId));
            Assert.IsTrue(returnedLecture.ValidationResult.IsValid);

            var statusCode = _apiCaller.GET<NoticeModel>(String.Format("courses/{0}/notices/{1}", courseId, noticeId)).Item1;
            Assert.AreEqual(HttpStatusCode.NotFound, statusCode);
        }

        private void It_should_unenrol_student_in_course(int studentId, int courseId)
        {
            var returnedStudent = _apiCaller.DELETE<ActionReponse<StudentModel>>(String.Format("students/{0}/courses/{1}", studentId, courseId));
            Assert.IsTrue(returnedStudent.ValidationResult.IsValid);

            var retrievedStudent = _apiCaller.GET<StudentModel>(String.Format("students/{0}", studentId)).Item2;
            Assert.IsEmpty(retrievedStudent.EnroledCourses);

            var retrievedCourse = _apiCaller.GET<CourseModel>(String.Format("courses/{0}", courseId)).Item2;
            Assert.IsEmpty(retrievedCourse.Students);
        }

        private void It_should_delete_lecture(int courseId, int lectureId)
        {
            var returnedLecture = _apiCaller.DELETE<ActionReponse<LectureModel>>(String.Format("courses/{0}/lectures/{1}", courseId, lectureId));
            Assert.IsTrue(returnedLecture.ValidationResult.IsValid);

            var statusCode = _apiCaller.GET<LectureModel>(String.Format("courses/{0}/lectures/{1}", courseId, lectureId)).Item1;
            Assert.AreEqual(HttpStatusCode.NotFound, statusCode);
        }

        private void It_should_delete_exam(int courseId, int examId)
        {
            var returnedExam = _apiCaller.DELETE<ActionReponse<ExamModel>>(String.Format("courses/{0}/exams/{1}", courseId, examId));
            Assert.IsTrue(returnedExam.ValidationResult.IsValid);

            //Get assignment, ensure is deleted
            var statusCode = _apiCaller.GET<ExamModel>(String.Format("courses/{0}/exams/{1}", courseId, examId)).Item1;
            Assert.AreEqual(HttpStatusCode.NotFound, statusCode);
        }

        private void It_should_delete_assignment(int courseId, int assignmentId)
        {
            var returnedAssignment = _apiCaller.DELETE<ActionReponse<AssignmentModel>>(String.Format("courses/{0}/assignments/{1}", courseId, assignmentId));
            Assert.IsTrue(returnedAssignment.ValidationResult.IsValid);

            //Get assignment, ensure is deleted
            var statusCode = _apiCaller.GET<AssignmentModel>(String.Format("courses/{0}/assignments/{1}", courseId, assignmentId)).Item1;
            Assert.AreEqual(HttpStatusCode.NotFound, statusCode);
        }

        private void It_should_delete_professor(int professorId)
        {
            var returnedProfessor = _apiCaller.DELETE<ActionReponse<ProfessorModel>>(String.Format("professors/{0}", professorId));
            Assert.IsTrue(returnedProfessor.ValidationResult.IsValid);

            //Get professor, ensure is deleted
            var statusCode = _apiCaller.GET<ProfessorModel>(String.Format("professors/{0}", professorId)).Item1;
            Assert.AreEqual(HttpStatusCode.NotFound, statusCode);
        }

        private void It_should_delete_student(int studentId)
        {
            var returnedStudent = _apiCaller.DELETE<ActionReponse<ProfessorModel>>(String.Format("students/{0}", studentId));
            Assert.IsTrue(returnedStudent.ValidationResult.IsValid);

            //Get student, ensure is deleted
            var statusCode = _apiCaller.GET<ProfessorModel>(String.Format("students/{0}", studentId)).Item1;
            Assert.AreEqual(HttpStatusCode.NotFound, statusCode);
        }

        private int It_should_create_student_for_user(int userId)
        {
            //Post to Student
            var newStudent = new StudentModel { FirstName = "studenttest" + _random.Next(100000), Surname = "Snow" + _random.Next(100000) };
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

        private int It_should_create_course_for_professor(int professorId)
        {
            //Post to Courses
            var newCourse = new CourseModel
            {
                Name = "INFO" + _random.Next(100000), 
                GradeType = GradeType.Letter.ToString(),
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddMonths(2)
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

        private int It_should_create_notice_for_course(int courseId)
        {
            //Post to Notices
            var newNotice = new NoticeModel
            {
                Message = "Welcome students! " + _random.Next(100000),
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddMonths(2)
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

        private int It_should_create_lecture_for_course(int courseId)
        {
            //Post to Lectures
            var newLecture = new LectureModel
            {
                Name = "Thursday Class " + _random.Next(100000),
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddMonths(2),
                Location = "LAB220",
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

        private int It_should_create_assignment_for_course(int courseId)
        {
            //Post to Assignments
            var newAssignment = new AssignmentModel
            {
                Name = "Assignment " + _random.Next(100000),
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddMonths(2),
                FinalMarkPercentage = 20,
                GradeType = "Letter"
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

        private int It_should_create_exam_for_course(int courseId)
        {
            //Post to Exams
            var newExam = new ExamModel
            {
                Name = "Exam " + _random.Next(100000),
                StartTime = DateTime.Today,
                Location = "KIRK302",
                FinalMarkPercentage = 20,
                GradeType = "Letter"
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

        private int It_should_add_grade_to_course_work_for_student(int studentId, int courseId, int courseWorkId)
        {
            var gradeModel = new CourseWorkGradeModel {GradePercentage = 76};
            var returnedStudent =
                _apiCaller.POST<ActionReponse<CourseWorkGradeModel>>(
                String.Format("students/{0}/grades/courses/{1}/course_work/{2}", studentId, courseId, courseWorkId),
                    _apiCaller.SerializeObject(gradeModel));
            Assert.IsTrue(returnedStudent.ValidationResult.IsValid);
            Assert.AreEqual(gradeModel.GradePercentage, returnedStudent.ActionResult.GradePercentage);

            var retrievedCourseGrade = _apiCaller.GET<CourseGradeModel>(String.Format("students/{0}/grades/courses/{1}", studentId, courseId)).Item2;
            Assert.IsNotEmpty(retrievedCourseGrade.CourseWorkGrades);

            return returnedStudent.ActionResult.Id;
        }

        private int It_should_update_grade(int studentId, int courseId, int courseWorkId)
        {
            var retrievedCourseWorkGrade = _apiCaller.GET<CourseWorkGradeModel>(String.Format("students/{0}/grades/courses/{1}/course_work/{2}", studentId, courseId, courseWorkId)).Item2;
            int oldGrade = retrievedCourseWorkGrade.GradePercentage;

            retrievedCourseWorkGrade.GradePercentage = retrievedCourseWorkGrade.GradePercentage + 5;

            var returnedCourseWorkGrade = _apiCaller.PUT<ActionReponse<CourseWorkGradeModel>>(
                String.Format("students/{0}/grades/courses/{1}/course_work/{2}", studentId, courseId, courseWorkId),
                    _apiCaller.SerializeObject(retrievedCourseWorkGrade));
            Assert.IsTrue(returnedCourseWorkGrade.ValidationResult.IsValid);
            Assert.AreNotEqual(oldGrade, returnedCourseWorkGrade.ActionResult.GradePercentage);

            return returnedCourseWorkGrade.ActionResult.Id;
        }
    }
}
