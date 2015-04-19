namespace Validation
{
    public static class ValidationMessages
    {
        public const string AlreadyEnroledInBlock = "You are already enroled in a selected block";
        public const string InvalidBlock = "You are trying to enrol in a block that doesn't exist";
        public const string InvalidClass = "You are trying to attend a class that doesn't exist";
        public const string InvalidUser = "User does not exist";
        public const string InvalidPass = "Pass does not exist";
        public const string IncorrectNumberOfAttendees = "You can only add attendance for one student at a time";
        public const string NoValidPasses = "Please purchase a pass before attending the class";
        public const string NoPaidForPasses = "Please pay for your pass before attending the class";
        public const string AlreadyAttendingClass = "You are already attending this class";
        public const string MissingPassword = "Please enter a password";
        public const string PasswordTooShort = "Please enter a password atleast 7 characters long";
        public const string MissingEmail = "Please enter an email address";
        public const string InvalidEmail = "Please enter a valid email address";
        public const string MissingFirstName = "Please enter a first name";
        public const string MissingSurname = "Please enter a surname";
        public const string DuplicateEmail = "Email address has already been registered";
        public const string InvalidUserToEnrol = "You do not have permission to enrol other students into blocks";
        public const string CannotChangePassType = "You can non change the type of pass";
        public const string ProvidePasses = "Provide passes to add";
        public const string CannotAddPassForAnother = "You don't have permission to add passes to another student";
        public const string BadActivationKey = "Bad activiation key";
        public const string BadPasswordResetKey = "Bad password reset key";
        public const string MissingDescription = "Please enter a description";
        public const string MissingPassType = "Please select a pass type";
        public const string InvalidPassType = "Pass type selected is invalid";
        public const string NegativeNumber = "Please enter a positive number";
        public const string InvalidPassTemplate = "Pass template doesn't exist";
        public const string CannontAddPaidPass = "You don't have permission to add a pass already paid for";
        public const string MissingName = "Please enter a name";
        public const string MissingStartTime = "Please enter a start time";
        public const string MissingEndTime = "Please enter an end time";
        public const string EndTimeGreaterThanStartTime = "Please enter an end time greater than the start time";
        public const string InvalidClassMinutes = "Please enter class minutes";
        public const string InvalidClassesInBlock = "Please enter number of classes in a block";
        public const string InvalidLevel = "Level doesn't exist";
    }
}
