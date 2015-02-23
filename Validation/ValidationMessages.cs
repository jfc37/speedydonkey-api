namespace Validation
{
    public static class ValidationMessages
    {
        #region User messages
        public const string MissingUsername = "Please provide a username";
        public const string MissingPassword = "Please provide a password";
        public const string DuplicateUsername = "Username is already in use. Please try another";
        public const string UserDoesntExist = "User doesn't exist";
        #endregion

        #region Person messages
        public const string MissingFirstName = "Please provide a first name";
        public const string MissingSurname = "Please provide a surname";
        public const string NoLinkedUser = "Can't create person against no user";
        public const string LinkedUserAlreadyHasPerson = "User already has person linked";
        #endregion

        #region Student messages
        public const string StudentDoesntExist = "Student doesn't exist";
        public const string EnrolingInMultipleCourses = "Can not enrol in multiple courses at once";
        public const string CoursesDontExist = "Courses don't exist";
        public const string StudentAlreadyEnroled = "Student already enroled in course";
        #endregion

        #region Professor messages
        public const string ProfessorDoesntExist = "Professor doesn't exist";
        #endregion

        #region Course messages
        public const string MissingName = "Please provide a name";
        public const string MissingStartDate = "Please provide a start date less than a year ago";
        public const string MissingGradeType = "Please provide a grade type";
        public const string EndDateBeforeStartDate = "Please provide an end date greater than the start date";
        public const string DuplicateCourseName = "Course name is already in use. Please try another";
        public const string CourseDoesntExist = "Course doesn't exist";
        #endregion

        #region Notice messages
        public const string MissingNoticeMessage = "Please provide a message";
        public const string NoticeDoesntExist = "Notice doesn't exist";
        #endregion

        #region Lecture messages
        public const string MissingOccurence = "Please provide an occurence";
        public const string LectureDoesntExist = "Lecture doesn't exist";
        #endregion

        #region Assignment messages
        public const string FinalMarkPercentageGreaterThan100 = "Final mark percentage can't be greater than 100";
        public const string AssignmentDoesntExist = "Assignment doesn't exist";
        #endregion

        #region Exam messages
        public const string MissingStartTime = "Please provide a start time";
        public const string ExamDoesntExist = "Exam doesn't exist";
        #endregion

        #region CourseGrades messages
        public const string CourseWorkDoesntExist = "Course work doesn't exist";
        public const string StudentAlreadyHasGradeForCourseWork = "Student already has grade for course work";
        public const string CourseWorkGradeDoesntExist = "Course work grade doesn't exist";

        #endregion
    }
}
