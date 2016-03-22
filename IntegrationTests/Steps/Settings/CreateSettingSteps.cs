﻿using System;
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
            var validUri = new Uri("http://mastiffpartyrentals.com/wp-content/uploads/2015/12/foo-fighters-logo-vector-5230292300-7bff484bd0-b.jpg");
            var logoSetting = new SettingItemModel(SettingTypes.Logo.ToString().ToLower(), validUri.OriginalString);
            var completeSettings = new CompleteSettingsModel(logoSetting);

            ScenarioCache.Store(ModelKeys.SettingItem, logoSetting);
            ScenarioCache.Store(ModelKeys.CompleteSettings, completeSettings);
        }

        [Given(@"a valid terms and conditions is ready to be submitted")]
        public void GivenAValidTermsAndConditionsIsReadyToBeSubmitted()
        {
            var validTerms = "<p>something</p>";
            var termsAndConditionsSetting = new SettingItemModel("termsAndConditions", validTerms);
            var completeSettings = new CompleteSettingsModel(termsAndConditionsSetting);

            ScenarioCache.Store(ModelKeys.SettingItem, termsAndConditionsSetting);
            ScenarioCache.Store(ModelKeys.CompleteSettings, completeSettings);
        }

        [Given(@"a valid logo url is ready to be submitted")]
        public void GivenAValidLogoUrlIsReadyToBeSubmitted()
        {
            var validUri = new Uri("http://www.allaccess.com/assets/img/editorial/raw/kr/KRfoundlogo.jpg");
            var logoSetting = new SettingItemModel(SettingTypes.Logo.ToString().ToLower(), validUri.OriginalString);
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

        [Then(@"terms and conditions setting is retrieved")]
        public void ThenTermsAndConditionsSettingIsRetrieved()
        {
            var response = ApiCaller.Get<SettingItemModel>(Routes.GetSettingsByType(SettingTypes.TermsAndConditions));

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var expectedSetting = ScenarioCache.Get<SettingItemModel>(ModelKeys.SettingItem);

            Assert.AreEqual(expectedSetting.Value, response.Data.Value);
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
            var completeSettings = new CompleteSettingsModel(new SettingItemModel(SettingTypes.Logo.ToString().ToLower(), "notaurl"));

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
