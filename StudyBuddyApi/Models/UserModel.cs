﻿using System;
using System.Collections.Generic;
using Common;
using Common.Extensions;

namespace SpeedyDonkeyApi.Models
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
        public string FullName { get { return String.Format("{0} {1}", FirstName, Surname); } }
        public List<EventModel> Schedule { get; set; }
        public List<BlockModel> EnroledBlocks { get; set; }
        public List<PassModel> Passes { get; set; }
        public string Email { get; set; }
        public bool DoNotEmail { get; set; }
        public string Note { get; set; }

        public override string ToString()
        {
            return this.ToDebugString(nameof(Id), nameof(FirstName), nameof(Surname), nameof(Email));
        }
    }
}