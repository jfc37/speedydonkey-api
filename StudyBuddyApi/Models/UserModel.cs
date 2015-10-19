using System;
using System.Collections.Generic;
using Common;

namespace SpeedyDonkeyApi.Models
{
    public class EntityModel
    {
        public int Id { get; set; }
        public virtual List<TeacherModel> Teachers { get; set; }
        public virtual List<UserModel> RegisteredStudents { get; set; }
        public virtual DateTimeOffset StartTime { get; set; }
        public virtual DateTimeOffset EndTime { get; set; }
        public virtual string Name { get; set; }
    }

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

        public string Note { get; set; }
    }
}