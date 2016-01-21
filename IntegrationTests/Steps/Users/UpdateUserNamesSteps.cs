using ActionHandlers;
using Contracts.Users;
using IntegrationTests.Utilities;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace IntegrationTests.Steps.Users
{
    [Binding]
    public class UpdateUserNamesSteps
    {
        [Given(@"the current user needs to change their name")]
        public void GivenTheCurrentUserNeedsToChangeTheirName()
        {
            var usernames = new UserNamesModel("first", "last");
            ScenarioCache.Store(ModelKeys.UserNamesModelKey, usernames);
        }

        [Given(@"the current user leaves the first name empty")]
        public void GivenTheCurrentUserLeavesTheFirstNameEmpty()
        {
            var usernames = ScenarioCache.Get<UserNamesModel>(ModelKeys.UserNamesModelKey);

            usernames.FirstName = null;

            ScenarioCache.Store(ModelKeys.UserNamesModelKey, usernames);
        }

        [When(@"the current user changes their name")]
        public void WhenTheCurrentUserChangesTheirName()
        {
            var usernames = ScenarioCache.Get<UserNamesModel>(ModelKeys.UserNamesModelKey);

            var response = ApiCaller.Put<ActionReponse<ActionReponse<UserModel>>>(usernames, Routes.CurrentUserNames);

            ScenarioCache.StoreActionResponse(response);
        }

        [Then(@"the current users name is changed")]
        public void ThenTheCurrentUsersNameIsChanged()
        {
            var response = ApiCaller.Get<UserNamesModel>(Routes.CurrentUserNames);

            var expectedNames = ScenarioCache.Get<UserNamesModel>(ModelKeys.UserNamesModelKey);

            Assert.AreEqual(expectedNames.FirstName, response.Data.FirstName);
            Assert.AreEqual(expectedNames.Surname, response.Data.Surname);
        }

        [Then(@"the current users name is unchanged")]
        public void ThenTheCurrentUsersNameIsUnchanged()
        {
            var response = ApiCaller.Get<UserNamesModel>(Routes.CurrentUserNames);

            var expectedNames = ScenarioCache.Get<UserNamesModel>(ModelKeys.UserNamesModelKey);

            Assert.IsNull(response.Data.FirstName);
            Assert.IsNull(response.Data.Surname);
        }



    }
}
