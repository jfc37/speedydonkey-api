using System.Collections.Generic;
using System.Linq;
using Common.Extensions;
using Data.Repositories;
using Models;

namespace Validation.Rules
{
    public class AreTeachersValidRule : IRule
    {
        private readonly IEnumerable<Teacher> _teachers;
        private readonly IRepository<Teacher> _repository;

        public AreTeachersValidRule(IEnumerable<Teacher> teachers, IRepository<Teacher> repository)
        {
            _teachers = teachers;
            _repository = repository;
        }

        public bool IsValid()
        {
            return _teachers.All(x => new DoesIdExist<Teacher>(_repository, x.Id).IsValid());
        }
    }
}