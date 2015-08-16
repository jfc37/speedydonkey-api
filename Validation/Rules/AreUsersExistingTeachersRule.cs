using System.Collections.Generic;
using Common.Extensions;
using Data.Repositories;
using Models;

namespace Validation.Rules
{
    public class AreUsersExistingTeachersRule : IRule
    {
        private readonly IEnumerable<ITeacher> _teachers;
        private readonly IRepository<Teacher> _repository;

        public AreUsersExistingTeachersRule(IEnumerable<ITeacher> teachers, IRepository<Teacher> repository)
        {
            _teachers = teachers;
            _repository = repository;
        }

        public bool IsValid()
        {
            foreach (var teacher in _teachers)
            {
                var savedTeacher = _repository.Get(teacher.Id);
                if (savedTeacher.IsNull() || !new DoesUserHaveClaimRule(savedTeacher, Claim.Teacher).IsValid())
                    return false;
            }
            return true;
        }
    }
}