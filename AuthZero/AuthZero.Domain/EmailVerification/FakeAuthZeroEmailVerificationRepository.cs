using System;
using AuthZero.Interfaces;

namespace AuthZero.Domain.EmailVerification
{
    public class FakeAuthZeroEmailVerificationRepository : IAuthZeroEmailVerificationRepository
    {
        public string Create(string globalId)
        {
            return Guid.NewGuid().ToString();
        }
    }
}