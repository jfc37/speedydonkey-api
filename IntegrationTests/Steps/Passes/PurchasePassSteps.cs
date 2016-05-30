using System.Collections.Generic;
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
                Routes.GetPassPurchase(ScenarioCache.GetUserId(), ScenarioCache.GetId(ModelIdKeys.PassTemplateId)));

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        [When(@"the user purchases '(.*)' passes from a teacher")]
        public void WhenTheUserPurchasesPassesFromATeacher(int numberOfPasses)
        {
            numberOfPasses.ToNumberRange()
                .Each(x => WhenTheUserPurchasesAPassFromATeacher());
        }

        [Then(@"the user has a pass")]
        public void ThenTheUserHasAPass()
        {
            var response = ApiCaller.Get<List<PassModel>>(Routes.GetUserPasses(ScenarioCache.GetId(ModelIdKeys.UserId)));

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotEmpty(response.Data);

            ScenarioCache.Store(ModelKeys.Pass, response.Data.Single());
        }

        [Then(@"all passes expire on the same day")]
        public void ThenAllPassesExpireOnTheSameDay()
        {
            var response = ApiCaller.Get<List<PassModel>>(Routes.GetUserPasses(ScenarioCache.GetId(ModelIdKeys.UserId)));

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.IsNotEmpty(response.Data);

            var allPasses = response.Data;

            Assert.Greater(allPasses.Count, 1);

            var distinctEndDates = allPasses.Select(x => x.EndDate.Date.Date)
                .Distinct()
                .Count();

            Assert.AreEqual(1, distinctEndDates);
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
