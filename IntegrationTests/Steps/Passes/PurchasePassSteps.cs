﻿using System.Collections.Generic;
using System.Linq;
using System.Net;
using ActionHandlers;
using Common.Extensions;
using Contracts.Passes;
using Contracts.Users;
using IntegrationTests.Utilities;
using Models;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace IntegrationTests.Steps.Passes
{
    [Binding]
    public class PurchasePassSteps
    {
        [When(@"the user purchases a pass from a teacher")]
        public void WhenTheUserPurchasesAPassFromATeacher()
        {
            var pass = new PassModel
            {
                PaymentStatus = PassPaymentStatus.Paid.ToString()
            };
            var response = ApiCaller.Post<ActionReponse<UserModel>>(pass,
                Routes.GetPassPurchase(ScenarioCache.GetUserId(), ScenarioCache.GetId(ModelIdKeys.PassTemplate)));

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        [Then(@"the user has a pass")]
        public void ThenTheUserHasAPass()
        {
            var response = ApiCaller.Get<List<PassModel>>(Routes.GetUserPasses(ScenarioCache.GetId(ModelIdKeys.User)));

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotEmpty(response.Data);

            ScenarioCache.Store(ModelKeys.Pass, response.Data.Single());
        }

        [Then(@"the pass is paid")]
        public void ThenThePassIsPaid()
        {
            var passModel = ScenarioCache.Get<PassModel>(ModelKeys.Pass);
            Assert.AreEqual(PassPaymentStatus.Paid, passModel.PaymentStatus.Parse<PassPaymentStatus>());
        }

        [Then(@"the pass is valid")]
        public void ThenThePassIsValid()
        {
            var passModel = ScenarioCache.Get<PassModel>(ModelKeys.Pass);
            Assert.IsTrue(passModel.IsValid());
        }


    }
}
