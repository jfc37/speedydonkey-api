﻿using System;

namespace Models
{
    public interface IPassTemplate : IEntity
    {
        string Description { get; set; }
        string PassType { get; set; }
        decimal Cost { get; set; }
        int WeeksValidFor { get; set; }
        int ClassesValidFor { get; set; }
        bool AvailableForPurchase { get; set; }
    }

    public class PassTemplate : IPassTemplate, IDatabaseEntity
    {
        public virtual int Id { get; set; }
        public  virtual bool Deleted { get; set; }
        public virtual string Description { get; set; }
        public virtual string PassType { get; set; }
        public virtual decimal Cost { get; set; }
        public virtual int WeeksValidFor { get; set; }
        public virtual int ClassesValidFor { get; set; }
        public virtual bool AvailableForPurchase { get; set; }
    }
}