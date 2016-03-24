using Common.Extensions;
using Contracts.Users;
using Models;

namespace Contracts.MappingExtensions
{
    public static class UserMappingExtensions
    {
        public static User ToEntity(this AuthZeroUserModel instance)
        {
            return new User
            {
                GlobalId = instance.UserId
            };
        }

        public static User ToEntity(this UserModel instance)
        {
            if (instance.IsNull())
                return null;

            return new User
            {
                Note = instance.Note,
                Email = instance.Email,
                EnroledBlocks = instance.EnroledBlocks.SelectIfNotNull(x => x.ToEntity()).ToListIfNotNull(),
                FirstName = instance.FirstName,
                Passes = instance.Passes.SelectIfNotNull(x => x.ToEntity()).ToListIfNotNull(),
                Schedule = instance.Schedule.SelectIfNotNull(x => x.ToEntity()).ToListIfNotNull(),
                Surname = instance.Surname,
                Password = instance.Password,
                Id = instance.Id,
                AgreesToTerms = instance.AgreesToTerms
            };
        }

        public static UserModel ToModel(this User instance)
        {
            if (instance.IsNull())
                return null;

            return new UserModel
            {
                Note = instance.Note,
                Email = instance.Email,
                EnroledBlocks = instance.EnroledBlocks.SelectIfNotNull(x => x.ToStripedModel()).ToListIfNotNull(),
                FirstName = instance.FirstName,
                Passes = instance.Passes.SelectIfNotNull(x => x.ToStripedModel()).ToListIfNotNull(),
                Surname = instance.Surname,
                Id = instance.Id,
                AgreesToTerms = instance.AgreesToTerms
            };
        }

        public static UserModel ToStripedModel(this User instance)
        {
            if (instance.IsNull())
                return null;

            return new UserModel
            {
                Email = instance.Email,
                FirstName = instance.FirstName,
                Surname = instance.Surname,
                Id = instance.Id
            };
        }
    }
}
