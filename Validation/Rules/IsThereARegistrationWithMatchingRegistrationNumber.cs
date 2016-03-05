using System;
using System.Linq;
using Data.Repositories;
using Models;

namespace Validation.Rules
{
    public class IsThereARegistrationWithMatchingRegistrationNumber : IRule
    {
        private readonly IRepository<Registration> _repository;
        private readonly string _registrationNumber;

        public IsThereARegistrationWithMatchingRegistrationNumber(
            IRepository<Registration> repository, 
            string registrationNumber)
        {
            _repository = repository;
            _registrationNumber = registrationNumber;
        }

        public bool IsValid()
        {
            if (!new IsStringAValidGuidRule(_registrationNumber).IsValid())
                return false;

            var registrationGuid = Guid.Parse(_registrationNumber);

            return _repository
                .Queryable()
                .Any(x => x.RegistationId == registrationGuid);
        }
    }
}
