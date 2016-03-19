using System.Collections.Generic;
using System.Linq;
using System.Net;
using Contracts.Users;
using IntegrationTests.Utilities;
using IntegrationTests.Utilities.ModelVerfication;
using Models.QueryExtensions;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace IntegrationTests.Steps.Users
{
    [Binding]
    public class DeleteUserSteps
    {
        [When(@"the user is deleted")]
        public void WhenTheUserIsDeleted()
        {
            var response = ApiCaller.Delete<UserModel>(Routes.GetById(Routes.Users, ScenarioCache.GetUserId()));
            ScenarioCache.StoreResponse(response);
        }

        [Then(@"the users details can not be retrieved")]
        public void ThenTheUsersDetailsCanNotBeRetrieved()
        {
            var userResponse = ApiCaller.Get<List<UserModel>>(Routes.Users);

            Assert.AreEqual(HttpStatusCode.OK, userResponse.StatusCode);

            Assert.IsEmpty(userResponse.Data.Where(x => x.Id == ScenarioCache.GetUserId()));
        }


    }
}
