using System.Linq;
using Common.Extensions;
using Data.Repositories;
using Models;
using Validation.Rules;

namespace Validation.RuleRunners
{
    public class IsUserNotATeacher : IRule
    {
        private readonly IRepository<Teacher> _repository;
        private readonly int _userId;

        public IsUserNotATeacher(IRepository<Teacher> repository, int userId)
        {
            _repository = repository;
            _userId = userId;
        }

        public bool IsValid()
        {
            return _repository.Queryable()
                .Where(x => x.User.Id == _userId)
                .NotAny();
        }
    }
}