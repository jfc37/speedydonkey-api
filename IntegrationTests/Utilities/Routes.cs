using Common.Extensions;

namespace IntegrationTests.Utilities
{
    public static class Routes
    {
        public const string Users = "users";
        public const string Teachers = "teachers";
        public const string Database = "database";
        public const string Levels = "levels";
        public const string Blocks = "blocks";
        public const string PassTemplate = "pass-templates";
        public const string CurrentUserClaims = "users/current/claims";
        public const string CurrentUserSchedule = "users/current/schedules";
        public const string GetCurrentUser = "users/current";

        public static string GetTeacherById(int userId)
        {
            return "{0}/{1}".FormatWith(Teachers, userId);
        }

        public static string GetLevelById(int id)
        {
            return "{0}/{1}".FormatWith(Levels, id);
        }

        public static string GetPassTemplateById(int id)
        {
            return "{0}/{1}".FormatWith(PassTemplate, id);
        }

        public static string GetCreateBlock(int levelId)
        {
            return "levels/{0}/blocks".FormatWith(levelId);
        }

        public static string GetEnrolUserInBlock(int userId)
        {
            return "users/{0}/enrolment".FormatWith(userId);
        }
    }
}
