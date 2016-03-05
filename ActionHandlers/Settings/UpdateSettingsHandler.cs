using System;
using System.Linq;
using Action.Settings;
using Data.Repositories;
using Models.Settings;

namespace ActionHandlers.Settings
{
    public class UpdateSettingsHandler : IActionHandler<UpdateSettings, CompleteSettings>
    {
        private readonly ISettingItemRepositoryFactory _repositoryFactory;

        public UpdateSettingsHandler(ISettingItemRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public CompleteSettings Handle(UpdateSettings action)
        {
            action.ActionAgainst.Settings
                .Select(x => new Tuple<ISettingItemRepository, SettingItem>(_repositoryFactory.Create(x), x))
                .ToList()
                .ForEach(x => x.Item1.Save(x.Item2));

            return action.ActionAgainst;
        }
    }

    public interface ISettingItemRepositoryFactory
    {
        ISettingItemRepository Create(SettingItem settingItem);
    }

    public class SettingItemRepositoryFactory : ISettingItemRepositoryFactory
    {
        private readonly IRepository<SettingItem> _repository;

        public SettingItemRepositoryFactory(IRepository<SettingItem> repository)
        {
            _repository = repository;
        }

        public ISettingItemRepository Create(SettingItem settingItem)
        {
            return _repository.Queryable().Any(x => x.Name == settingItem.Name)
                ? (ISettingItemRepository) new UpdateSettingItemRepository(_repository)
                : new CreateSettingItemRepository(_repository);
        }
    }

    public interface ISettingItemRepository
    {
        void Save(SettingItem settingItem);
    }

    public class CreateSettingItemRepository : ISettingItemRepository
    {
        private readonly IRepository<SettingItem> _repository;

        public CreateSettingItemRepository(IRepository<SettingItem> repository)
        {
            _repository = repository;
        }

        public void Save(SettingItem settingItem)
        {
            _repository.Create(settingItem);
        }
    }

    public class UpdateSettingItemRepository : ISettingItemRepository
    {
        private readonly IRepository<SettingItem> _repository;

        public UpdateSettingItemRepository(IRepository<SettingItem> repository)
        {
            _repository = repository;
        }

        public void Save(SettingItem settingItem)
        {
            var savedSetting = _repository.Queryable()
                .Single(x => x.Name == settingItem.Name);

            savedSetting.Value = settingItem.Value;

            _repository.Update(savedSetting);
        }
    }
}
