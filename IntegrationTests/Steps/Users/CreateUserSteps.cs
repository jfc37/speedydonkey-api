using System.Collections.Generic;
using System.Linq;
using System.Net;
using ActionHandlers;
using IntegrationTests.Utilities;
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
                Email = "joe@email.com",
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

            ScenarioCache.StoreId(userResponse.Data.ActionResult.Id);
        }

        [When(@"user is attempted to be created")]
        public void WhenUserIsAttemptedToBeCreated()
        {
            var expectedUser = ScenarioCache.Get<UserModel>(ExpectedUserKey);

            var userResponse = ApiCaller.Post<ActionReponse<UserModel>>(expectedUser, Routes.Users);

            ScenarioCache.StoreResponse(userResponse);
        }

        #endregion

        #region Then

        [Then(@"that user's details can be retrieved")]
        public void ThenThatUserSDetailsCanBeRetrieved()
        {
            var userResponse = ApiCaller.Get<List<UserModel>>(Routes.Users);

            Assert.AreEqual(userResponse.StatusCode, HttpStatusCode.OK);

            var createdUser = userResponse.Data.SingleWithId(ScenarioCache.GetUserId());
            var expectedUser = ScenarioCache.Get<UserModel>(ExpectedUserKey);
            Assert.AreEqual(expectedUser.FullName, createdUser.FullName);
            Assert.AreEqual(expectedUser.Email, createdUser.Email);
            Assert.IsNullOrEmpty(createdUser.Password);
        }

        [Then(@"validation errors are returned")]
        public void ThenValidationErrorsAreReturned()
        {
            var response = ScenarioCache.GetResponse<ActionReponse<UserModel>>();

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.IsFalse(response.Data.ValidationResult.IsValid);
        }

        [Then(@"user is not created")]
        public void ThenUserIsNotCreated()
        {
            var allUsersResponse = ApiCaller.Get<List<UserModel>>(Routes.Users);

            Assert.AreEqual(allUsersResponse.StatusCode, HttpStatusCode.OK);

            var response = ScenarioCache.GetResponse<ActionReponse<UserModel>>();
            Assert.IsFalse(allUsersResponse.Data.WithFullName(response.Data.ActionResult.FullName).Any());
        }

        #endregion
    }
}
