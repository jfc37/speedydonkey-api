using System;
using System.Net;
using ActionHandlers;
using Contracts.Settings;
using IntegrationTests.Utilities;
using Models.Settings;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace IntegrationTests.Steps.Settings
{
    [Binding]
    public class CreateSettingSteps
    {
        [Given(@"the logo setting is already set")]
        public void GivenTheLogoSettingIsAlreadySet()
        {
            GivenAValidLogoUrlIsReadyToBeSubmitted();
            WhenTheSettingsAreAttemptedToBeSet();
            ThenLogoSettingIsRetrieved();
        }

        [Given(@"the logo setting needs to be changed")]
        public void GivenTheLogoSettingNeedsToBeChanged()
        {
            var validUri = new Uri("http://www.bing.com");
            var logoSetting = new SettingItemModel(SettingTypes.Logo.ToString(), validUri.OriginalString);
            var completeSettings = new CompleteSettingsModel(logoSetting);

            ScenarioCache.Store(ModelKeys.SettingItem, logoSetting);
            ScenarioCache.Store(ModelKeys.CompleteSettings, completeSettings);
        }
        
        [Given(@"a valid logo url is ready to be submitted")]
        public void GivenAValidLogoUrlIsReadyToBeSubmitted()
        {
            var validUri = new Uri("http://www.google.com");
            var logoSetting = new SettingItemModel(SettingTypes.Logo.ToString(), validUri.OriginalString);
            var completeSettings = new CompleteSettingsModel(logoSetting);

            ScenarioCache.Store(ModelKeys.SettingItem, logoSetting);
            ScenarioCache.Store(ModelKeys.CompleteSettings, completeSettings);
        }

        [When(@"the settings are attempted to be set")]
        public void WhenTheSettingsAreAttemptedToBeSet()
        {
            var response = ApiCaller.Post<ActionReponse<CompleteSettingsModel>>(ScenarioCache.Get<CompleteSettingsModel>(ModelKeys.CompleteSettings), Routes.Settings);
            ScenarioCache.StoreActionResponse(response);
        }

        [Then(@"logo setting is retrieved")]
        public void ThenLogoSettingIsRetrieved()
        {
            var response = ApiCaller.Get<SettingItemModel>(Routes.GetSettingsByType(SettingTypes.Logo));

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var expectedLogoSetting = ScenarioCache.Get<SettingItemModel>(ModelKeys.SettingItem);

            Assert.AreEqual(expectedLogoSetting.Value, response.Data.Value);
        }

        [Given(@"an invalid logo url is ready to be submitted")]
        public void GivenAnInvalidLogoUrlIsReadyToBeSubmitted()
        {
            var completeSettings = new CompleteSettingsModel(new SettingItemModel(SettingTypes.Logo.ToString(), "notaurl"));

            ScenarioCache.Store(ModelKeys.CompleteSettings, completeSettings);
        }

        [Then(@"logo setting is not retrieved")]
        public void ThenLogoSettingIsNotRetrieved()
        {
            var response = ApiCaller.Get<SettingItemModel>(Routes.GetSettingsByType(SettingTypes.Logo));

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

    }
}
