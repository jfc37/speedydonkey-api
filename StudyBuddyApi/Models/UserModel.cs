using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Models;

namespace SpeedyDonkeyApi.Models
{
    public class UserModel : ApiModel<User, UserModel>, IUser
    {
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string FullName { get { return String.Format("{0} {1}", FirstName, Surname); } }
        public IList<IBooking> Schedule { get; set; }
        public ICollection<IBlock> EnroledBlocks { get; set; }
        public IList<IPass> Passes { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime? LastUpdatedDateTime { get; set; }

        protected override void AddChildrenToEntity(User entity)
        {
            if (EnroledBlocks != null)
            {
                entity.EnroledBlocks = EnroledBlocks
                    .Select(x => (IBlock) ((BlockModel) x).ToEntity())
                    .ToList();
            }
            if (Passes != null)
            {
                entity.Passes = Passes
                    .Select(x => (IPass) ((PassModel) x).ToEntity())
                    .ToList();
            }
        }

        protected override void SanitiseModel(UserModel model)
        {
            model.Password = null;
        }

        public string Note { get; set; }
    }
}