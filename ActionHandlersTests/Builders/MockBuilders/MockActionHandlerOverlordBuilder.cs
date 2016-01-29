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
        public object PassedInAction { get; private set; }

        public MockActionHandlerOverlordBuilder WithNoErrorsOnHandling<TAction, TObject>() where TAction : SystemAction<TObject>
        {
            Mock.Setup(x => x.HandleAction<TAction, TObject>(It.IsAny<TAction>()))
                .Callback<TAction>(x => PassedInAction = x)
                .Returns<TAction>(t => new ActionReponse<TObject>{ActionResult = t.ActionAgainst, ValidationResult = new ValidationResult()});
            return this;
        }

        public MockActionHandlerOverlordBuilder WithErrorsOnHandling<TAction, TObject>() where TAction : SystemAction<TObject>
        {
            var validationResult = new ValidationResult
            {
                ValidationErrors = new List<ValidationError>
                {
                    new ValidationError("error", "error")
                }
            };

            Mock.Setup(x => x.HandleAction<TAction, TObject>(It.IsAny<TAction>()))
                .Callback<TAction>(x => PassedInAction = x)
                .Returns<TAction>(t => new ActionReponse<TObject> { ActionResult = t.ActionAgainst, ValidationResult = validationResult });
            return this;
        }
    }
}
