using System.Linq;
using System.Web.Http;
using Action.Settings;
using ActionHandlers;
using Contracts.MappingExtensions;
using Contracts.Settings;
using Data.Repositories;
using Data.Searches;
using Models;
using Models.Settings;
using SpeedyDonkeyApi.CodeChunks;
using SpeedyDonkeyApi.Filter;

namespace SpeedyDonkeyApi.Controllers.Settings
{
    [RoutePrefix("api/settings")]
    public class SettingsController : GenericApiController<CompleteSettings>
    {
        private readonly IRepository<SettingItem> _settingsRepository;

        public SettingsController(
            IActionHandlerOverlord actionHandlerOverlord, 
            IRepository<CompleteSettings> repository, 
            IEntitySearch<CompleteSettings> entitySearch,
            IRepository<SettingItem> settingsRepository) 
            : base(actionHandlerOverlord, repository, entitySearch)
        {
            _settingsRepository = settingsRepository;
        }

        [Route]
        [ClaimsAuthorise(Claim = Claim.Admin)]
        public IHttpActionResult Post([FromBody]CompleteSettingsModel model)
        {
            var result = PerformAction<UpdateSettings, CompleteSettings>(new UpdateSettings(model.ToEntity()));

            return new ActionResultToCreatedHttpActionResult<CompleteSettings, CompleteSettingsModel>(result, x => x.ToModel(), this)
                .Do();
        }

        [Route("{settingType}")]
        [AllowAnonymous]
        public IHttpActionResult Get(SettingTypes settingType)
        {
            return _settingsRepository.GetAll().Any(x => x.Name == settingType)
                ? (IHttpActionResult) Ok(_settingsRepository.GetAll().Single(x => x.Name == settingType).ToModel())
                : NotFound();
        }
        
        [Route]
        [AllowAnonymous]
        public IHttpActionResult Get()
        {
            return Ok(_settingsRepository.GetAll().Select(x => x.ToModel()));
        }
        
    }
}
