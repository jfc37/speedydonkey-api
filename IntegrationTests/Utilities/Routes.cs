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
        public const string Classes = "classes";
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

        public static string GetLevelSearch(string search)
        {
            return "levels?q={0}".FormatWith(search);
        }

        public static string GetPassTemplateSearch(string search)
        {
            return "pass-templates?q={0}".FormatWith(search);
        }

        public static string GetTeacherSearch(string search)
        {
            return "teachers?q={0}".FormatWith(search);
        }

        public static string GetUserSearch(string search)
        {
            return "users?q={0}".FormatWith(search);
        }

        public static string GetUserById(int id)
        {
            return "users/{0}".FormatWith(id);
        }

        public static string GetSearch(string resource, string search)
        {
            return "{0}?q={1}".FormatWith(resource, search);
        }

        public static string GetById(string resource, int id)
        {
            return "{0}/{1}".FormatWith(resource, id);
        }

        public static string GetPassPurchase(int userId, int passTemplateId)
        {
            return "users/{0}/pass-templates/{1}".FormatWith(userId, passTemplateId);
        }

        public static string GetUserPasses(int userId)
        {
            return "users/{0}/passes".FormatWith(userId);
        }

        public static string GetAttendClass(int classId, int userId)
        {
            return "classes/{0}/attendance/{1}".FormatWith(classId, userId);
        }
    }
}
