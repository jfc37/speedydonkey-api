using Contracts.Users;
using IntegrationTests.Utilities;
using TechTalk.SpecFlow;

namespace IntegrationTests.Steps.Users
{
    [Binding]
    public class CurrentUserSteps
    {
        private const string ExpectedUserKey = "expectedUser";

        [When(@"the current user is retrieved")]
        public void WhenTheCurrentUserIsRetrieved()
        {
            var response = ApiCaller.Get<UserModel>(Routes.GetCurrentUser);

            ScenarioCache.Store(ExpectedUserKey, response.Data);
            ScenarioCache.Store(ModelIdKeys.UserId, response.Data.Id);
        }
    }
}