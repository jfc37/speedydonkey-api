using System;
using System.Collections.Generic;
using Common;
using Common.Extensions;
using Contracts.Blocks;
using Contracts.Events;
using Contracts.Passes;

namespace Contracts.Users
{
    public class UserModel : IEntity
    {
        public UserModel(int id)
        {
            Id = id;
        }

        public UserModel() { }

        public int Id { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string FullName => $"{FirstName} {Surname}";
        public List<EventModel> Schedule { get; set; }
        public List<BlockModel> EnroledBlocks { get; set; }
        public List<PassModel> Passes { get; set; }
        public string Email { get; set; }
        public bool AgreesToTerms { get; set; }

        public string Note { get; set; }

        public override string ToString()
        {
            return this.ToDebugString(nameof(Id), nameof(FirstName), nameof(Surname), nameof(Email));
        }
    }
}