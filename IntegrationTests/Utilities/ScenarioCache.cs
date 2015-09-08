using Common.Extensions;
using NUnit.Framework;
using RestSharp;
using TechTalk.SpecFlow;

namespace IntegrationTests.Utilities
{
    public static class ScenarioCache
    {
        private const string UserIdKey = "id";
        private const string ResponseKey = "response";

        public static void StoreId(int id)
        {
            ScenarioContext.Current.Add(UserIdKey, id);
        }

        public static void Store(string key, object item)
        {
            Clear(key);
            ScenarioContext.Current.Add(key, item);
        }

        public static T Get<T>(string key)
        {
            AssertKeyExists(key);
            return ScenarioContext.Current.Get<T>(key);
        }

        public static int GetUserId()
        {
            AssertKeyExists(UserIdKey);
            return ScenarioContext.Current.Get<int>(UserIdKey);
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

        public static void StoreResponse<T>(IRestResponse<T> response)
        {
            Clear(ResponseKey);
            ScenarioContext.Current.Add(ResponseKey, response);
        }

        public static IRestResponse<T> GetResponse<T>()
        {
            AssertKeyExists(ResponseKey);
            return ScenarioContext.Current.Get<IRestResponse<T>>(ResponseKey);
        }

        public static int GetTeacherId()
        {
            return 1;
        }
    }
}
