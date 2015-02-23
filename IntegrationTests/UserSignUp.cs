using System;
using ActionHandlers;
using Models;
using Newtonsoft.Json;
using NUnit.Framework;
using SpeedyDonkeyApi.Models;

namespace IntegrationTests
{
    [TestFixture]
    public class UserSignUp
    {
        private ApiCaller _apiCaller;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _apiCaller = new ApiCaller();
        }

        [Test]
        public void User_successfully_signs_up()
        {
            //Post to Users
            var newUser = new User { Username = "usertest" + new Random().Next(1000), Password = "password"};
            var returnedUser = _apiCaller.POST<ActionReponse<User>>("users", JsonConvert.SerializeObject(newUser));
            Assert.IsTrue(returnedUser.ValidationResult.IsValid);

            //Get User, ensure is created
            var retrievedUser = _apiCaller.GET<User>(String.Format("users/{0}", returnedUser.ActionResult.Id)).Item2;
            Assert.AreEqual(returnedUser.ActionResult.Username, retrievedUser.Username);

            //Post to Students
            var newStudent = new Student {FirstName = "John", Surname = "Snow"};
            var returnedStudent =
                _apiCaller.POST<ActionReponse<PersonModel>>(
                    String.Format("users/{0}/student", returnedUser.ActionResult.Id),
                    JsonConvert.SerializeObject(newStudent));
            Assert.IsTrue(returnedStudent.ValidationResult.IsValid);

            //Get Student, ensure is created
            var retrievedStudent = _apiCaller.GET<PersonModel>(String.Format("students/{0}", returnedStudent.ActionResult.Id)).Item2;
            Assert.AreEqual(returnedStudent.ActionResult.FirstName, retrievedStudent.FirstName);
        }

        [Test]
        public void User_unsuccessfully_signs_up()
        {
            //Post to Users
            var newUser = new User { Username = "usertest" + new Random().Next(1000) };
            var returnedUser = _apiCaller.POST<ActionReponse<User>>("users", JsonConvert.SerializeObject(newUser));
            Assert.IsFalse(returnedUser.ValidationResult.IsValid);
        }
    }
}
