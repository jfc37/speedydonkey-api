﻿namespace Validation
{
    public static class ValidationMessages
    {
        public const string AlreadyEnroledInBlock = "You are already enroled in a selected block";
        public const string NotEnroledInBlock = "You are not enroled in the selected block";
        public const string InvalidBlock = "Block doesn't exist";
        public const string UnavailableBlock = "Block isn't available for enrolment";
        public const string FullBlock = "Block is full";
        public const string InvalidRoom = "Room doesn't exist";
        public const string InvalidClass = "Class doesn't exist";
        public const string InvalidEvent = "Event doesn't exist";
        public const string InvalidUser = "User does not exist";
        public const string InvalidPass = "Pass does not exist";
        public const string IncorrectNumberOfAttendees = "You can only add attendance for one student at a time";
        public const string NoValidPasses = "Please purchase a pass before attending the class";
        public const string NoPaidForPasses = "Please pay for your pass before attending the class";
        public const string AlreadyAttending = "You are already attending";
        public const string MissingPassword = "Please enter a password";
        public const string PasswordTooShort = "Please enter a password atleast 7 characters long";
        public const string MissingEmail = "Please enter an email address";
        public const string InvalidEmail = "Please enter a valid email address";
        public const string MissingGlobalId = "Please enter a global id";
        public const string AuthUserAlreadyExistsAsUser = "Auth0 user already exists as a user";
        public const string MissingFirstName = "Please enter a first name";
        public const string MissingSurname = "Please enter a surname";
        public const string TermsAndConditions = "Please agree to the terms and conditions";
        public const string DuplicateEmail = "Email address has already been registered";
        public const string InvalidUserToEnrol = "You do not have permission to enrol other students into blocks";
        public const string ProvidePasses = "Provide passes to add";
        public const string CannotAddPassForAnother = "You don't have permission to add passes to another student";
        public const string BadActivationKey = "Bad activiation key";
        public const string BadPasswordResetKey = "Bad password reset key";
        public const string MissingDescription = "Please enter a description";
        public const string MissingPassType = "Please select a pass type";
        public const string InvalidPassType = "Pass type selected is invalid";
        public const string NegativeNumber = "Please enter a positive number";
        public const string ItemDoesntExist = "Item doesn't exist";
        public const string CannontAddPaidPass = "You don't have permission to add a pass already paid for";
        public const string MissingName = "Please enter a name";
        public const string MissingPrice = "Please enter a price";
        public const string MissingLocation = "Please enter a location";
        public const string MissingStartTime = "Please enter a start time";
        public const string MissingEndTime = "Please enter an end time";
        public const string EndTimeGreaterThanStartTime = "Please enter an end time greater than the start time";
        public const string InvalidClassMinutes = "Please enter class minutes";
        public const string InvalidClassesInBlock = "Please enter number of classes in a block";
        public const string InvalidClassCapacity = "Please enter a class capacity greater than 0";
        public const string InvalidTeacherRate = "Please enter a teacher rate greater than 0";
        public const string InvalidTeachers = "Teachers selected are invalid";
        public const string TeachersRequired = "Please select at least one teacher";
        public const string RoomRequired = "Please select a room";
        public const string InvalidClipsRemaining = "Invalid number of clips remaining";
        public const string UserIsAlreadyATeacher = "User is already a teacher";
        public const string CannotDeleteClassWithAttendance = "Cannot delete class with attendance";
        public const string InvalidOpeningDay = "Please select a day";
        public const string InvalidOpeningTime = "Please select an opening time";
        public const string InvalidClosingTime = "Please select a closing time";
        public const string ClosingTimeAfterOpeningTime = "Please select a closing time later than the opening time";
    }
}
