using System;
using IntegrationTests.Utilities;
using Models.Settings;
using SpeedyDonkeyApi.Models.Settings;
using TechTalk.SpecFlow;

namespace IntegrationTests.Steps.Settings
{
    [Binding]
    public class CreateSettingSteps
    {
        [Given(@"a valid logo url is ready to be submitted")]
        public void GivenAValidLogoUrlIsReadyToBeSubmitted()
        {
            var validUri = new Uri("www.google.com");
            var completeSettings = new CompleteSettingsModel(new SettingItemModel(SettingTypes.Logo.ToString(), validUri.OriginalString));

            ScenarioCache.Store(ModelKeys.RoomModelKey, completeSettings);
        }

        [When(@"the settings are attempted to be set")]
        public void WhenTheSettingsAreAttemptedToBeSet()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"logo setting is retrieved")]
        public void ThenLogoSettingIsRetrieved()
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"an invalid logo url is ready to be submitted")]
        public void GivenAnInvalidLogoUrlIsReadyToBeSubmitted()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"logo setting is not retrieved")]
        public void ThenLogoSettingIsNotRetrieved()
        {
            ScenarioContext.Current.Pending();
        }

    }
}
