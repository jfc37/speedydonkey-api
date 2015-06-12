﻿using System;
using System.Collections.Generic;
using Models;

namespace SpeedyDonkeyApi.Models
{
    public class TeacherModel : ApiModel<Teacher, TeacherModel>, ITeacher
    {
        protected override string RouteName
        {
            get { return "TeacherApi"; }
        }

        public ICollection<IClass> Classes { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string FullName { get { return String.Format("{0} {1}", FirstName, Surname); } }
        public IList<IBooking> Schedule { get; set; }
        public ICollection<IBlock> EnroledBlocks { get; set; }
        public IList<IPass> Passes { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime? LastUpdatedDateTime { get; set; }
        public string Note { get; set; }
    }
}