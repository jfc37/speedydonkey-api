using System;

namespace Models
{
    public enum Claim
    {
        Invalid,
        AnyUserData,
        CreateNewBlock,
        DeleteDatabase,
        CreateLevel,
        UpdatePass,
        CreateReferenceData,
        CheckStudentIntoClass,
        EnrolOtherIntoBlock,
        Admin,
        Teacher
    }
}
