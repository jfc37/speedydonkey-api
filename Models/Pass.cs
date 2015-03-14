using System;

namespace Models
{
    public interface IPass
    {
        int Id { get; set; }
        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }
        string PassType { get; set; }
        IUser Owner { get; set; }
        bool IsValid();
        //pending passes for when paying in class
        //all passes will have expiry date

    }

    public class Pass : IPass, IEntity
    {
        public virtual int Id { get; set; }
        public virtual DateTime StartDate { get; set; }
        public virtual DateTime EndDate { get; set; }

        public virtual string PassType
        {
            get { return _passType.ToString(); }
            set { _passType = (PassType) Enum.Parse(typeof (PassType), value, true); }
        }

        public virtual IUser Owner { get; set; }
        public virtual bool IsValid()
        {
            var today = DateTime.Now.Date;
            return StartDate >= today && today <= EndDate;
        }

        private PassType _passType;
    }

    public enum PassType
    {
        Invalid,
        Unlimited,
        Clip
    }
}