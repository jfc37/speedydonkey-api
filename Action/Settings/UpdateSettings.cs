using Actions;
using Models.Settings;

namespace Action.Settings
{
    public class UpdateSettings : SystemAction<CompleteSettings>
    {
        public UpdateSettings(CompleteSettings settings)
        {
            ActionAgainst = settings;
        }
    }
}