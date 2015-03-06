using System.Collections.Generic;
using ActionHandlers;
using Actions;
using Common.Tests.Builders.MockBuilders;
using Moq;
using Validation;

namespace ActionHandlersTests.Builders.MockBuilders
{
    public class MockActionHandlerOverlordBuilder : MockBuilder<IActionHandlerOverlord>
    {
        public MockActionHandlerOverlordBuilder WithNoErrorsOnHandling<TAction, TObject>() where TAction : IAction<TObject>
        {
            Mock.Setup(x => x.HandleAction<TAction, TObject>(It.IsAny<TAction>()))
                .Returns<TAction>(t => new ActionReponse<TObject>{ActionResult = t.ActionAgainst, ValidationResult = new ValidationResult()});
            return this;
        }

        public MockActionHandlerOverlordBuilder WithErrorsOnHandling<TAction, TObject>() where TAction : IAction<TObject>
        {
            var validationResult = new ValidationResult
            {
                ValidationErrors = new List<ValidationError>
                {
                    new ValidationError("error", "error")
                }
            };

            Mock.Setup(x => x.HandleAction<TAction, TObject>(It.IsAny<TAction>()))
                .Returns<TAction>(t => new ActionReponse<TObject> { ActionResult = t.ActionAgainst, ValidationResult = validationResult });
            return this;
        }
    }
}
