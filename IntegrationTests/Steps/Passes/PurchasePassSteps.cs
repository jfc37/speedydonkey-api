using System.Collections.Generic;
using System.Linq;
using System.Net;
using ActionHandlers;
using Common.Extensions;
using IntegrationTests.Utilities;
using Models;
using NUnit.Framework;
using SpeedyDonkeyApi.Models;
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
                Routes.GetPassPurchase(ScenarioCache.GetUserId(), ScenarioCache.GetId(ModelIdKeys.PassTemplateKeyId)));

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        [Then(@"the user has a pass")]
        public void ThenTheUserHasAPass()
        {
            var response = ApiCaller.Get<List<PassModel>>(Routes.GetUserPasses(ScenarioCache.GetId(ModelIdKeys.UserIdKey)));

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotEmpty(response.Data);

            ScenarioCache.Store(ModelKeys.PassModelKey, response.Data.Single());
        }

        [Then(@"the pass is paid")]
        public void ThenThePassIsPaid()
        {
            var passModel = ScenarioCache.Get<PassModel>(ModelKeys.PassModelKey);
            Assert.AreEqual(PassPaymentStatus.Paid, passModel.PaymentStatus.Parse<PassPaymentStatus>());
        }

        [Then(@"the pass is valid")]
        public void ThenThePassIsValid()
        {
            var passModel = ScenarioCache.Get<PassModel>(ModelKeys.PassModelKey);
            Assert.IsTrue(passModel.IsValid());
        }


    }
}
