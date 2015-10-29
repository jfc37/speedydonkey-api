using Action.Classes;
using Data.Repositories;
using FluentValidation;
using Models;
using Validation.Rules;

namespace Validation.Validators.Classes
{
    public class UnassignClassRoomValidator : AbstractValidator<Class>, IActionValidator<UnassignClassRoom, Class>
    {
        public UnassignClassRoomValidator(IRepository<Event> eventRepository)
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Id)
                .Must(x => new DoesIdExist<Event>(eventRepository, x).IsValid()).WithMessage(ValidationMessages.InvalidClass);
        }
    }
}