using System;
using System.Collections.Generic;
using Common;

namespace SpeedyDonkeyApi.Models
{
    public class UserModel : IEntity
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string FullName { get { return String.Format("{0} {1}", FirstName, Surname); } }
        public List<BookingModel> Schedule { get; set; }
        public List<BlockModel> EnroledBlocks { get; set; }
        public List<PassModel> Passes { get; set; }
        public string Email { get; set; }

        public string Note { get; set; }
    }
}