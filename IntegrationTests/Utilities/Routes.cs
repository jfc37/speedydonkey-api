using Common.Extensions;

namespace IntegrationTests.Utilities
{
    public static class Routes
    {
        public const string Users = "users";
        public const string Teachers = "teachers";
        public const string Database = "database";

        public static string GetTeacherById(int userId)
        {
            return "{0}/{1}".FormatWith(Teachers, userId);
        }
    }
}
