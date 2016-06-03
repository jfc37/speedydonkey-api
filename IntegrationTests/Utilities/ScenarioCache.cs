using System.Net;
using ActionHandlers;
using Common.Extensions;
using NUnit.Framework;
using RestSharp;
using TechTalk.SpecFlow;
using Validation;
using System.Collections.Generic;

namespace IntegrationTests.Utilities
{
    public enum ModelKeys
    {
        Pass,
        PassTemplate,
        Block,
        StandAloneEvent,
        Announcement,
        Class,
        Room,
        UserNames,
        OpeningHours,
        TeacherAvailability,
        SettingItem,
        CompleteSettings,
        TeacherRate,
        TeacherInvoiceReport,
        PassSalesReport,
        BlockSummaryReport,
        BlockDetailsReport,

        Response
    }

    public enum ModelIdKeys
    {
        UserId,
        TeacherId,
        BlockId,
        StandAloneEventId,
        PassTemplateId,
        ClassId,
        RoomId,
        StudentIds,
        BlockIds
    }

    public static class ScenarioCache
    {
        private const string ValidationResultKey = "validationResult";
        private const string ActionResultKey = "actionResultKey";
        private const string ResponseStatusKey = "responseStatusKey";

        public static void AddToSet<T>(ModelIdKeys key, T value)
        {
            var valueSet = ScenarioCache.Get<List<T>>(key);
            valueSet.Add(value);
            ScenarioCache.Store(key, valueSet);
        }

        public static void Store(ModelIdKeys key, object item)
        {
            Store(key.ToString(), item);
        }
        
        public static void Store(ModelKeys key, object item)
        {
            Store(key.ToString(), item);
        }

        public static void Store(string key, object item)
        {
            Clear(key);
            ScenarioContext.Current.Add(key, item);
        }

        public static T Get<T>(ModelIdKeys key)
        {
            return Get<T>(key.ToString());
        }

        public static T Get<T>(ModelKeys key)
        {
            return Get<T>(key.ToString());
        }

        public static T Get<T>(string key)
        {
            AssertKeyExists(key);
            return ScenarioContext.Current.Get<T>(key);
        }

        public static int GetUserId()
        {
            return Get<int>(ModelIdKeys.UserId);
        }

        public static int GetTeacherId()
        {
            return Get<int>(ModelIdKeys.TeacherId);
        }

        private static void AssertKeyExists(string key)
        {
            Assert.IsTrue(ScenarioContext.Current.ContainsKey(key),
                "Expected to have found the key {0} in the scenario context".FormatWith(key));   
        }

        private static void Clear(string key)
        {
            ScenarioContext.Current.Remove(key);
        }

        public static void StoreActionResponse<T>(IRestResponse<ActionReponse<T>> response)
        {
            Store(ValidationResultKey, response.Data.ValidationResult);
            Store(ActionResultKey, response.Data.ActionResult);
            Store(ResponseStatusKey, response.StatusCode);
        }

        public static void StoreResponse<T>(IRestResponse<T> response)
        {
            Store(ResponseStatusKey, response.StatusCode);

            if (response.Data.IsNotNull())
                Store(ModelKeys.Response, response.Data);
        }

        public static HttpStatusCode GetResponseStatus()
        {
            return Get<HttpStatusCode>(ResponseStatusKey);
        }

        public static T GetActionResponse<T>()
        {
            return Get<T>(ActionResultKey);
        }

        public static T GetResponse<T>()
        {
            return Get<T>(ModelKeys.Response);
        }

        public static ValidationResult GetValidationResult()
        {
            return Get<ValidationResult>(ValidationResultKey);
        }

        public static int GetId(ModelIdKeys key)
        {
            return Get<int>(key);
        }
    }
}
