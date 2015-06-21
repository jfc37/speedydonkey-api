using System.Web.Http.Results;
using ActionHandlers;
using Common;
using Common.Tests.Builders.MockBuilders;
using Models;
using NUnit.Framework;
using SpeedyDonkeyApi.Controllers;
using SpeedyDonkeyApi.Models;
using Validation;

namespace StudyBuddyApi.Tests.Controllers
{
    [TestFixture]
    public class PayPalApiControllerTests
    {
        public class PostBegin : PayPalApiControllerTests
        {
            [Test]
            public void It_should_return_bad_request_when_required_parameters_are_missing()
            {
                var actionHandlerOverlord = new MockBuilder<IActionHandlerOverlord>();

                var controller = new PayPalApiController(actionHandlerOverlord.BuildObject(), new CurrentUser());
                controller.ModelState.AddModelError("1", "1");

                var response = controller.Begin(new PayPalBeginViewModel());

                Assert.IsInstanceOf<InvalidModelStateResult>(response);
            }

            [Test]
            public void It_should_return_bad_request_when_valiation_errors_exist()
            {
                var actionHandlerOverlord = new MockBuilder<IActionHandlerOverlord>();
                actionHandlerOverlord.Mock.SetReturnsDefault(new ActionReponse<PendingOnlinePayment>{ ValidationResult = new ValidationResult
                {
                    ValidationErrors = new []
                    {
                        new ValidationError("1", "1") 
                    }
                }});

                var controller = new PayPalApiController(actionHandlerOverlord.BuildObject(), new CurrentUser());

                var response = controller.Begin(new PayPalBeginViewModel());

                Assert.IsInstanceOf<InvalidModelStateResult>(response);
            }

            [Test]
            public void It_should_return_ok_when_no_valiation_errors_exist()
            {
                var actionHandlerOverlord = new MockBuilder<IActionHandlerOverlord>();
                actionHandlerOverlord.Mock.SetReturnsDefault(new ActionReponse<PendingOnlinePayment>());

                var controller = new PayPalApiController(actionHandlerOverlord.BuildObject(), new CurrentUser());

                var response = controller.Begin(new PayPalBeginViewModel());

                Assert.IsInstanceOf<OkNegotiatedContentResult<ActionReponse<PendingOnlinePayment>>>(response);
            }
        }
    }
}
