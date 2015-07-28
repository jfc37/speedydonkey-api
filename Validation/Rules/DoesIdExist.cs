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
}