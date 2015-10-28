using Common.Extensions;
using Data.Repositories;
using Models;

namespace Validation.Rules
{
    public class HasNoClassAttandanceRule : IRule
    {
        private readonly IRepository<Class> _repository;
        private readonly int _id;

        public HasNoClassAttandanceRule(IRepository<Class> repository, int id)
        {
            _repository = repository;
            _id = id;
        }

        public bool IsValid()
        {
            return _repository.Get(_id).ActualStudents.NotAny();
        }
    }
}