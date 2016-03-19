using System.Collections.Generic;
using Contracts;
using Contracts.Classes;
using IntegrationTests.Utilities;
using TechTalk.SpecFlow;

namespace IntegrationTests.Steps.Classes
{
    [Binding]
    public class RetrieveClassSteps
    {
        [When(@"all classes are retreived")]
        public void WhenAllClassesAreRetreived()
        {
            var response = ApiCaller.Get<ClassModel>(Routes.Classes);
            ScenarioCache.Store(ModelKeys.Response, response.StatusCode);
        }

        [When(@"a class search is performed")]
        public void WhenAClassSearchIsPerformed()
        {
            var response = ApiCaller.Get<List<ClassModel>>(Routes.GetSearch(Routes.Classes, "id_=_1"));
            ScenarioCache.Store(ModelKeys.Response, response.StatusCode);
        }

        [When(@"a class is retrieved by id")]
        public void WhenAClassIsRetrievedById()
        {
            var response = ApiCaller.Get<ClassModel>(Routes.GetById(Routes.Classes, 1));
            ScenarioCache.Store(ModelKeys.Response, response.StatusCode);
        }

    }
}
