using System.Collections.Generic;
using IntegrationTests.Utilities;
using SpeedyDonkeyApi.Models;
using TechTalk.SpecFlow;

namespace IntegrationTests.Steps.PassTemplates
{
    [Binding]
    public class RetrievePassTemplateSteps
    {
        [When(@"all pass templates are retreived")]
        public void WhenAllPassTemplatesAreRetreived()
        {
            var response = ApiCaller.Get<PassTemplateModel>(Routes.PassTemplate);
            ScenarioCache.Store(ModelKeys.ResponseKey, response.StatusCode);
        }

        [When(@"a pass template search is performed")]
        public void WhenAPassTemplateSearchIsPerformed()
        {
            var response = ApiCaller.Get<List<PassTemplateModel>>(Routes.GetPassTemplateSearch("id_=_1"));
            ScenarioCache.Store(ModelKeys.ResponseKey, response.StatusCode);
        }

        [When(@"a pass template is retrieved by id")]
        public void WhenAPassTemplateIsRetrievedById()
        {
            var response = ApiCaller.Get<PassTemplateModel>(Routes.GetPassTemplateById(ScenarioCache.GetActionResponse<PassTemplateModel>().Id));
            ScenarioCache.Store(ModelKeys.ResponseKey, response.StatusCode);
        }
    }
}
