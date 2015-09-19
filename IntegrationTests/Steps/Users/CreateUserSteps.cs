using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using ActionHandlers;
using Common.Extensions;
using IntegrationTests.Utilities;
using IntegrationTests.Utilities.ModelVerfication;
using Models.QueryExtensions;
using NUnit.Framework;
using SpeedyDonkeyApi.Models;
using TechTalk.SpecFlow;

namespace IntegrationTests.Steps.Users
{
    [Binding]
    public class CreateUserSteps
    {
        private const string ExpectedUserKey = "expectedUser";

        #region Given

        [Given(@"a user ready to sign up")]
        public void GivenAUserReadyToSignUp()
        {
            var expectedUser = new UserModel
            {
                Surname = "Chapman",
                Email = "joe{0}@email.com".FormatWith(Guid.NewGuid()),
                Password = "password",
                FirstName = "Joe"
            };
            ScenarioCache.Store(ExpectedUserKey, expectedUser);
        }


        [Given(@"required fields are missing")]
        public void GivenRequiredFieldsAreMissing()
        {
            var expectedUser = ScenarioCache.Get<UserModel>(ExpectedUserKey);

            expectedUser.Email = string.Empty;
            expectedUser.Password = string.Empty;

            ScenarioCache.Store(ExpectedUserKey, expectedUser);

        }

        [Given(@"an exitings user has the email address '(.*)'")]
        public void GivenAnExitingsUserHasTheEmailAddress(string email)
        {
            var expectedUser = new UserModel
            {
                Surname = "Peterson",
                Email = email,
                Password = "password",
                FirstName = "Peter"
            };
            ScenarioCache.Store(ExpectedUserKey, expectedUser);
            WhenAUserIsCreated();
        }

        [Given(@"they provide the email address '(.*)'")]
        public void GivenTheyProvideTheEmailAddress(string email)
        {
            var expectedUser = ScenarioCache.Get<UserModel>(ExpectedUserKey);
            expectedUser.Email = email;

            ScenarioCache.Store(ExpectedUserKey, expectedUser);
        }

        #endregion

        #region When

        [When(@"a user is created")]
        public void WhenAUserIsCreated()
        {
            var expectedUser = ScenarioCache.Get<UserModel>(ExpectedUserKey);

            var userResponse = ApiCaller.Post<ActionReponse<UserModel>>(expectedUser, Routes.Users);

            Assert.AreEqual(userResponse.StatusCode, HttpStatusCode.Created);

            ScenarioCache.StoreUserId(userResponse.Data.ActionResult.Id);
            ScenarioCache.Store(ModelKeys.CurrentUserEmail, userResponse.Data.ActionResult.Email);
        }

        [When(@"user is attempted to be created")]
        public void WhenUserIsAttemptedToBeCreated()
        {
            var expectedUser = ScenarioCache.Get<UserModel>(ExpectedUserKey);

            var userResponse = ApiCaller.Post<ActionReponse<UserModel>>(expectedUser, Routes.Users);

            ScenarioCache.StoreActionResponse(userResponse);
        }

        #endregion

        #region Then

        [Then(@"the user's details can be retrieved")]
        public void ThenTheUserSDetailsCanBeRetrieved()
        {
            var userResponse = ApiCaller.Get<List<UserModel>>(Routes.Users);

            Assert.AreEqual(userResponse.StatusCode, HttpStatusCode.OK);

            var createdUser = userResponse.Data.SingleWithId(ScenarioCache.GetUserId());
            var expectedUser = ScenarioCache.Get<UserModel>(ExpectedUserKey);

            new VerifyUserProperties(expectedUser, createdUser)
                .Verify();
        }

        [Then(@"validation errors are returned")]
        public void ThenValidationErrorsAreReturned()
        {
            Assert.AreEqual(HttpStatusCode.BadRequest, ScenarioCache.GetResponseStatus());
            Assert.IsFalse(ScenarioCache.GetValidationResult().IsValid);
        }

        [Then(@"user is not created")]
        public void ThenUserIsNotCreated()
        {
            var allUsersResponse = ApiCaller.Get<List<UserModel>>(Routes.Users);

            Assert.AreEqual(allUsersResponse.StatusCode, HttpStatusCode.OK);

            var user = ScenarioCache.GetActionResponse<UserModel>();
            Assert.IsFalse(allUsersResponse.Data.Any(x => x.FullName == user.FullName));
        }

        #endregion
    }
}
