using Actions;
using FluentValidation;

namespace Validation.Validators
{
    public interface IActionValidator<in TAction, TObject> : IValidator<TObject> where TAction : IAction<TObject>
    {
    }
}
