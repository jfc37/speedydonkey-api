﻿using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Common;
using Models;
using SpeedyDonkeyApi.Services;

namespace SpeedyDonkeyApi.Models
{
    public class UserModel : ApiModel<User, UserModel>, IUser
    {
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public IList<IBooking> Schedule { get; set; }
        public ICollection<IBlock> EnroledBlocks { get; set; }
        public IList<IPass> Passes { get; set; }
        public string Email { get; set; }

        protected override string RouteName
        {
            get { return "UserApi"; }
        }

        protected override void AddChildrenToEntity(User entity, ICommonInterfaceCloner cloner)
        {
            if (EnroledBlocks != null)
            {
                entity.EnroledBlocks = EnroledBlocks
                    .Select(x => (IBlock) ((BlockModel) x).ToEntity(cloner))
                    .ToList();
            }
            if (Passes != null)
            {
                entity.Passes = Passes
                    .Select(x => (IPass) ((PassModel) x).ToEntity(cloner))
                    .ToList();
            }
        }
    }
}