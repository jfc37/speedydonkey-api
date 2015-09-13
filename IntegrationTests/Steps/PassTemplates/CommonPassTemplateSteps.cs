using TechTalk.SpecFlow;

namespace IntegrationTests.Steps.PassTemplates
{
    [Binding]
    public class CommonPassTemplateSteps
    {
        [Given(@"a pass template exists")]
        public void GivenAPassTemplateExists()
        {
            var createPassTemplateSteps = new CreatePassTemplateSteps();
            createPassTemplateSteps.GivenAValidPassTemplateIsReadyToBeSubmitted();
            createPassTemplateSteps.WhenThePassTemplateIsAttemptedToBeCreated();
            createPassTemplateSteps.ThenPassTemplateCanBeRetrieved();
        }   
    }
}