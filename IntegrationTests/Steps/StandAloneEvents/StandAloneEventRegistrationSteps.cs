using System.Linq;
using System.Net;
using ActionHandlers;
using Contracts.Events;
using Contracts.Users;
using IntegrationTests.Utilities;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace IntegrationTests.Steps.StandAloneEvents
{
    [Binding]
    public class StandAloneEventRegistrationSteps
    {
        [When(@"the user registers for the stand alone event")]
        public void WhenTheUserRegistersForTheStandAloneEvent()
        {
            var response = ApiCaller.Post<ActionReponse<UserModel>>(new EventRegistrationModel(ScenarioCache.GetId(ModelIdKeys.StandAloneEvent)),
                Routes.GetRegisterUserInEvent(ScenarioCache.GetUserId()));

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Then(@"the user is registered in the stand alone event")]
        public void ThenTheUserIsRegisteredInTheStandAloneEvent()
        {
            var response = ApiCaller.Get<StandAloneEventModel>(Routes.GetById(Routes.StandAloneEvent, ScenarioCache.GetId(ModelIdKeys.StandAloneEvent)));

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var user = response.Data.RegisteredStudents.Single();
            Assert.AreEqual(ScenarioCache.GetId(ModelIdKeys.User), user.Id);
        }
    }
}
