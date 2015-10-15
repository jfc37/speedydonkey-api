using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Extensions;
using Data.Repositories;

namespace Validation.Rules
{
    public class DoesIdExist<T> : IRule where T : IEntity
    {
        private readonly IRepository<T> _repository;
        private readonly int _id;

        public DoesIdExist(IRepository<T> repository, int id)
        {
            _repository = repository;
            _id = id;
        }

        public bool IsValid()
        {
            var item = _repository.Get(_id);
            return item.IsNotNull();
        }
    }
    public class DoAllIdExists<T> : IRule where T : IEntity
    {
        private readonly IRepository<T> _repository;
        private readonly IEnumerable<T> _entities;

        public DoAllIdExists(IRepository<T> repository, IEnumerable<T> entities)
        {
            _repository = repository;
            _entities = entities;
        }

        public bool IsValid()
        {
            var allExistingEntityIds = _repository.GetAll().Select(x => x.Id);
            return _entities.All(x => allExistingEntityIds.Contains(x.Id));
        }
    }
}