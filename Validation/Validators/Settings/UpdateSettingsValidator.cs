using System.Linq;
using Action.Settings;
using FluentValidation;
using Models.Settings;

namespace Validation.Validators.Settings
{
    public class UpdateSettingsValidator : AbstractValidator<CompleteSettings>, IActionValidator<UpdateSettings, CompleteSettings>
    {
        public UpdateSettingsValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x.Settings)
                .NotEmpty()
                .Must(x => new AreAllSettingNamesValid(x).IsValid())
                .Must(x => new AreNoDuplicateSettings(x).IsValid())
                .Must(x => new IsLogoSettingAValidUrl(x).IsValid()).When(x => x.Settings.Any(y => y.Name == SettingTypes.Logo))
                .Must(x => new IsSettingValueANumber(x, SettingTypes.MinutesPerClass).IsValid()).When(x => x.Settings.Any(y => y.Name == SettingTypes.MinutesPerClass))
                .Must(x => new IsSettingValueANumber(x, SettingTypes.NumberOfClasses).IsValid()).When(x => x.Settings.Any(y => y.Name == SettingTypes.NumberOfClasses))
                .Must(x => new IsSettingValueANumber(x, SettingTypes.ClassCapacity).IsValid()).When(x => x.Settings.Any(y => y.Name == SettingTypes.ClassCapacity))
                .Must(x => new IsSettingValueADecimal(x, SettingTypes.TeacherRateSolo).IsValid()).When(x => x.Settings.Any(y => y.Name == SettingTypes.TeacherRateSolo))
                .Must(x => new IsSettingValueADecimal(x, SettingTypes.TeacherRatePartnered).IsValid()).When(x => x.Settings.Any(y => y.Name == SettingTypes.TeacherRatePartnered));
        }
    }
}