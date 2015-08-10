using System;
using System.Collections.Generic;
using Common;
using Models.OnlinePayments;

namespace Models
{
    public enum CompetitionType
    {
        Novice,
        SoloJazz,
        Balboa,
        JackAndJill,
        Hellzapoppin,
        StrictlyLindy,
        ShowcaseTeam,
        ShowcaseCouple
    }

    public enum DanceLevel
    {
        Beginner,
        Panda,
        Avocado,
        Expert,
        SuperGod
    }

    public enum Style
    {
        Lead,
        Follow
    }

    public interface IRegistration : IEntity
    {
        Guid RegistationId { get; set; }
        decimal Amount { get; set; }
        OnlinePaymentStatus PaymentStatus { get; }

        string FirstName { get; set; }
        string Surname { get; set; }
        string Email { get; set; }
        string Phone { get; set; }
        string CountryOfResidence { get; set; }
        string EmergancyContactName { get; set; }
        string EmergancyContactNumber { get; set; }
        bool Over18 { get; set; }
        bool FullPass { get; set; }
        Style Style { get; set; }
        DanceLevel LindyLevel { get; set; }
        DanceLevel BalboaLevel { get; set; }
        DanceLevel BluesLevel { get; set; }
        List<string> Classes { get; set; }
        List<string> Events { get; set; }
        List<ICompetitionRegistration> Competitions { get; set; }


    }

    public interface ICompetitionRegistration : IEntity
    {
        CompetitionType Type { get; set; }
        string PartnerName { get; set; }
    }

    public class CompetitionRegistration : ICompetitionRegistration, IDatabaseEntity
    {
        public virtual int Id { get; set; }
        public virtual DateTime CreatedDateTime { get; set; }
        public virtual DateTime? LastUpdatedDateTime { get; set; }
        public virtual CompetitionType Type { get; set; }
        public virtual string PartnerName { get; set; }
        public virtual bool Deleted { get; set; }
    }

    public class Registration : IRegistration, IDatabaseEntity
    {
        public virtual int Id { get; set; }
        public virtual DateTime CreatedDateTime { get; set; }
        public virtual DateTime? LastUpdatedDateTime { get; set; }
        public virtual Guid RegistationId { get; set; }
        public virtual decimal Amount { get; set; }

        public virtual bool Deleted { get; set; }

        public virtual OnlinePaymentStatus PaymentStatus { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string Surname { get; set; }
        public virtual string Email { get; set; }
        public virtual string Phone { get; set; }
        public virtual string CountryOfResidence { get; set; }
        public virtual string EmergancyContactName { get; set; }
        public virtual string EmergancyContactNumber { get; set; }
        public virtual bool Over18 { get; set; }
        public virtual bool FullPass { get; set; }
        public virtual Style Style { get; set; }
        public virtual DanceLevel LindyLevel { get; set; }
        public virtual DanceLevel BalboaLevel { get; set; }
        public virtual DanceLevel BluesLevel { get; set; }
        public virtual List<string> Classes { get; set; }
        public virtual List<string> Events { get; set; }
        public virtual List<ICompetitionRegistration> Competitions { get; set; }
    }

}
