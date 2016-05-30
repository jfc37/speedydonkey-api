using IntegrationTests.Steps.PassTemplates;
using TechTalk.SpecFlow;

namespace IntegrationTests.Steps.Passes
{
    [Binding]
    public class CommonPassSteps
    {
        [Given(@"the user has a valid clip pass")]
        public void GivenTheUserHasAValidClipPass()
        {
            var commonPassTemplateSteps = new CommonPassTemplateSteps();
            commonPassTemplateSteps.GivenAPassTemplateExists();

            var purchasePassSteps = new PurchasePassSteps();
            purchasePassSteps.WhenTheUserPurchasesAPassFromATeacher();
            purchasePassSteps.ThenTheUserHasAClipPass();
            purchasePassSteps.ThenThePassIsPaid();
            purchasePassSteps.ThenThePassIsValid();
        }

        [Given(@"the user doesn't have a pass")]
        public void GivenTheUserDoesnTHaveAPass()
        {
        }
    }
}
