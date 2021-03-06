﻿using System;
using System.Collections.Generic;
using Common;
using Common.Extensions;

namespace Models
{
    public class Block : DatabaseEntity
    {
        public Block(int id) : this()
        {
            Id = id;
        }

        public Block()
        {
            Classes = new List<Class>();
        }
        
        public virtual ICollection<Teacher> Teachers { get; set; }
        public virtual ICollection<User> EnroledStudents { get; set; }
        public virtual ICollection<Class> Classes { get; set; }
        public virtual ICollection<Announcement> Announcements { get; set; }
        public virtual Room Room { get; set; }
        public virtual DateTimeOffset StartDate { get; set; }
        public virtual DateTimeOffset EndDate { get; set; }
        public virtual string Name { get; set; }
        public virtual int NumberOfClasses { get; set; }
        public virtual int MinutesPerClass { get; set; }
        public virtual int ClassCapacity { get; set; }
        public virtual bool IsInviteOnly { get; set; }

        public override string ToString()
        {
            return this.ToDebugString(nameof(Id), nameof(Name), nameof(StartDate), nameof(EndDate));
        }
    }
}