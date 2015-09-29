//using System.Collections.Generic;
//using IntegrationTests.Utilities;
//using SpeedyDonkeyApi.Models;
//using TechTalk.SpecFlow;

//namespace IntegrationTests.Steps.Levels
//{
//    [Binding]
//    public class RetrieveLevelSteps
//    {
//        [When(@"all levels are retreived")]
//        public void WhenAllLevelsAreRetreived()
//        {
//            var response = ApiCaller.Get<LevelModel>(Routes.Levels);
//            ScenarioCache.Store(ModelKeys.ResponseKey, response.StatusCode);
//        }

//        [When(@"a level search is performed")]
//        public void WhenALevelSearchIsPerformed()
//        {
//            var response = ApiCaller.Get<List<LevelModel>>(Routes.GetLevelSearch("id_=_1"));
//            ScenarioCache.Store(ModelKeys.ResponseKey, response.StatusCode);
//        }

//        [When(@"a level is retrieved by id")]
//        public void WhenALevelIsRetrievedById()
//        {
//            var response = ApiCaller.Get<LevelModel>(Routes.GetLevelById(ScenarioCache.GetActionResponse<LevelModel>().Id));
//            ScenarioCache.Store(ModelKeys.ResponseKey, response.StatusCode);

//        }

//    }
//}
