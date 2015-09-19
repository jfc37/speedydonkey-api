using TechTalk.SpecFlow;

namespace IntegrationTests.Steps.Users
{
    [Binding]
    public class CommonUserSteps
    {
        [Given(@"a user exists")]
        public void GivenAUserExists()
        {
            var createUserSteps = new CreateUserSteps();

            createUserSteps.GivenAUserReadyToSignUp();
            createUserSteps.WhenAUserIsCreated();
        }
    }
}
