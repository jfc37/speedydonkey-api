using Common.Extensions;
using Models.Settings;

namespace IntegrationTests.Utilities
{
    public static class Routes
    {
        public const string Users = "users";
        public const string Teachers = "teachers";
        public const string Database = "database";
        public const string Announcements = "announcements";
        public const string Blocks = "blocks";
        public const string StandAloneEvent = "stand-alone-events";
        public const string Classes = "classes";
        public const string Room = "rooms";
        public const string PassTemplate = "pass-templates";
        public const string CurrentUserNames = "users/current/names";
        public const string CurrentUserSchedule = "users/current/schedules";
        public const string GetCurrentUser = "users/current";
        public const string BlocksForEnrolment = "blocks/for-enrolment";
        public const string StandAloneEventsForRegistration = "stand-alone-events/for-registration";
        public const string OpeningHours = "opening-hours";
        public const string TeacherAvailability = "teacher-availabilities";
        public const string Settings = "settings";
        public const string TermsAndConditions = "users/current/terms-and-conditions";

        public static string GetBlocksForEnrolment(int userId)
        {
            return $"{BlocksForEnrolment}/{userId}";
        }

        public static string GetCurrentTeacherAvailabilities()
        {
            return $"{TeacherAvailability}/current";
        }
        public static string GetSettingsByType(SettingTypes settingType)
        {
            return $"{Settings}/{settingType}";
        }
        public static string GetRoomUpcomingSchedule(int roomId)
        {
            return "rooms/{0}/upcoming-schedule".FormatWith(roomId);
        }
        public static string GetUserSchedule(int userId)
        {
            return "users/{0}/schedules".FormatWith(userId);
        }
        public static string GetUserClaims(int userId)
        {
            return "users/{0}/claims".FormatWith(userId);
        }
        public static string GetClassAttendance(int classId)
        {
            return "classes/{0}/attendance".FormatWith(classId);
        }
        public static string GetEventAttendance(int eventId)
        {
            return "stand-alone-events/{0}/attendance".FormatWith(eventId);
        }

        public static string GetChangeClassTeachers(int classId)
        {
            return "classes/{0}/teachers".FormatWith(classId);
        }

        public static string GetTeacherById(int userId)
        {
            return "{0}/{1}".FormatWith(Teachers, userId);
        }

        public static string GetPassTemplateById(int id)
        {
            return "{0}/{1}".FormatWith(PassTemplate, id);
        }

        public static string GetCreateNextBlock(int id)
        {
            return "blocks/{0}".FormatWith(id);
        }

        public static string GetEnrolUserInBlock(int userId)
        {
            return "users/{0}/enrolment".FormatWith(userId);
        }

        public static string GetDoNotEmailUser(int userId)
        {
            return "users/{0}/do-not-email".FormatWith(userId);
        }

        public static string GetRegisterUserInEvent(int userId)
        {
            return "users/{0}/registration/event".FormatWith(userId);
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

        public static string GetAttendEvent(int eventId, int userId)
        {
            return "stand-alone-events/{0}/attendance/{1}".FormatWith(eventId, userId);
        }

        public static string GetClassRoom(int classId, int roomId)
        {
            return "classes/{0}/rooms/{1}".FormatWith(classId, roomId);
        }

        public static string GetClassRoom(int classId)
        {
            return "classes/{0}/rooms".FormatWith(classId);
        }

        public static string GetBlockRoom(int blockId, int roomId)
        {
            return "blocks/{0}/rooms/{1}".FormatWith(blockId, roomId);
        }

        public static string GetBlockRoom(int blockId)
        {
            return "blocks/{0}/rooms".FormatWith(blockId);
        }
    }
}
