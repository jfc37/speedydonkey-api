﻿using System.Linq;
using System.Net;
using ActionHandlers;
using IntegrationTests.Utilities;
using NUnit.Framework;
using SpeedyDonkeyApi.Models;
using TechTalk.SpecFlow;

namespace IntegrationTests.Steps.StandAloneEvents
{
    [Binding]
    public class StandAloneEventRegistrationSteps
    {
        [When(@"the user registers for the stand alone event")]
        public void WhenTheUserRegistersForTheStandAloneEvent()
        {
            var response = ApiCaller.Post<ActionReponse<UserModel>>(new EventRegistrationModel(ScenarioCache.GetId(ModelIdKeys.StandAloneEventKeyId)),
                Routes.GetRegisterUserInEvent(ScenarioCache.GetUserId()));

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Then(@"the user is registered in the stand alone event")]
        public void ThenTheUserIsRegisteredInTheStandAloneEvent()
        {
            var response = ApiCaller.Get<StandAloneEventModel>(Routes.GetById(Routes.StandAloneEvent, ScenarioCache.GetId(ModelIdKeys.StandAloneEventKeyId)));

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var user = response.Data.RegisteredStudents.Single();
            Assert.AreEqual(ScenarioCache.GetId(ModelIdKeys.UserIdKey), user.Id);
        }
    }
}