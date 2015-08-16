using System;
using System.Linq;
using Data.Repositories;
using Models;

namespace Data.CodeChunks
{
    /// <summary>
    /// Gets the registration with a given registration number
    /// </summary>
    public class GetRegistrationFromRegistrationNumber : ICodeChunk<Registration>
    {
        private readonly IRepository<Registration> _repository;
        private readonly Guid _registrationNumber;

        public GetRegistrationFromRegistrationNumber(IRepository<Registration> repository, Guid registrationNumber)
        {
            _repository = repository;
            _registrationNumber = registrationNumber;
        }

        public Registration Do()
        {
            return _repository.GetAll()
                .Single(x => x.RegistationId == _registrationNumber);
        }
    }
}
