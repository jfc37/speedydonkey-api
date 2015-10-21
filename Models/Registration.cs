using System;
using System.Collections.Generic;
using Common;
using Common.Extensions;
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
        Intermediate,
        Advanced,
        AdvancedPlus
    }

    public enum DanceRole
    {
        Lead,
        Follow
    }

    public interface IRegistration : IEntity
    {
        Guid RegistationId { get; set; }
        decimal Amount { get; set; }
        OnlinePaymentStatus PaymentStatus { get; set; }

        string FirstName { get; set; }
        string Surname { get; set; }
        string Email { get; set; }
        string PhoneNumber { get; set; }
        string CountryOfResidence { get; set; }
        string City { get; set; }
        string EmergencyContactPerson { get; set; }
        string EmergencyContactNumber { get; set; }
        bool? Over18 { get; set; }
        bool? FullPass { get; set; }
        DanceRole? DanceRole { get; set; }
        DanceLevel? LindyLevel { get; set; }
        DanceLevel? BalboaLevel { get; set; }
        DanceLevel? BluesLevel { get; set; }
        ICollection<string> Classes { get; set; }
        ICollection<WindyLindyEvents> Events { get; set; }

        bool Novice { get; set; }
        string NovicePartner { get; set; }
        bool Balboa { get; set; }
        string BalboaPartner { get; set; }
        bool Hellzapoppin { get; set; }
        string HellzapoppinPartner { get; set; }
        bool JackAndJill { get; set; }
        bool ShowcaseCouple { get; set; }
        string ShowcaseCouplePartner { get; set; }
        bool ShowcaseTeam { get; set; }
        string ShowcaseTeamPartner { get; set; }
        bool SoloJazz { get; set; }
        bool StrictlyLindy { get; set; }
        string StrictlyLindyPartner { get; set; }

        bool PerformAtGrammy { get; set; }
        bool Aerials { get; set; }
        string AerialsPartner { get; set; }
        string AerialsTeachers { get; set; }
        bool TermsAndConditions { get; set; }
    }

    public enum WindyLindyEvents
    {
        Heats,
        GrammyAwards,
        RockStarsBall,
        GroupiesPrivateParty,
        HardRockBusTour,
        BackStageParty,
        SundayNightAfterParty,
        GroupiesGoodbyeJam
    }

    public class Registration : IRegistration, IDatabaseEntity
    {
        public virtual int Id { get; set; }
        public virtual DateTime CreatedDateTime { get; set; }
        public virtual DateTime? LastUpdatedDateTime { get; set; }
        public virtual Guid RegistationId { get; set; }
        public virtual decimal Amount { get; set; }


        public virtual OnlinePaymentStatus PaymentStatus { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string Surname { get; set; }
        public virtual string Email { get; set; }
        public virtual string PhoneNumber { get; set; }
        public virtual string CountryOfResidence { get; set; }
        public virtual string City { get; set; }
        public virtual string EmergencyContactPerson { get; set; }
        public virtual string EmergencyContactNumber { get; set; }
        public virtual bool? Over18 { get; set; }
        public virtual bool? FullPass { get; set; }
        public virtual DanceRole? DanceRole { get; set; }
        public virtual DanceLevel? LindyLevel { get; set; }
        public virtual DanceLevel? BalboaLevel { get; set; }
        public virtual DanceLevel? BluesLevel { get; set; }
        public virtual ICollection<string> Classes { get; set; }
        public virtual ICollection<WindyLindyEvents> Events { get; set; }
        public virtual bool Novice { get; set; }
        public virtual string NovicePartner { get; set; }
        public virtual bool Balboa { get; set; }
        public virtual string BalboaPartner { get; set; }
        public virtual bool Hellzapoppin { get; set; }
        public virtual string HellzapoppinPartner { get; set; }
        public virtual bool JackAndJill { get; set; }
        public virtual bool ShowcaseCouple { get; set; }
        public virtual string ShowcaseCouplePartner { get; set; }
        public virtual bool ShowcaseTeam { get; set; }
        public virtual string ShowcaseTeamPartner { get; set; }
        public virtual bool SoloJazz { get; set; }
        public virtual bool StrictlyLindy { get; set; }
        public virtual string StrictlyLindyPartner { get; set; }
        public virtual bool PerformAtGrammy { get; set; }
        public virtual bool Aerials { get; set; }
        public virtual string AerialsPartner { get; set; }
        public virtual string AerialsTeachers { get; set; }
        public virtual bool TermsAndConditions { get; set; }
    }

    public static class RegistrationExtensions
    {
        public static string GetDescription(this IRegistration instance)
        {
            return instance.FullPass.GetValueOrDefault()
                ? "Full Pass"
                : "Individual Event Pass";
        }
    }

    public static class WindyLindyEventExtensions
    {
        public static decimal GetPrice(this WindyLindyEvents instance)
        {
            switch (instance)
            {
                case WindyLindyEvents.Heats:
                    return 10;

                case WindyLindyEvents.GrammyAwards:
                    return 30;

                case WindyLindyEvents.RockStarsBall:
                    return 60;

                case WindyLindyEvents.GroupiesPrivateParty:
                    return 25;

                case WindyLindyEvents.HardRockBusTour:
                    return 30;

                case WindyLindyEvents.BackStageParty:
                    return 60;

                case WindyLindyEvents.SundayNightAfterParty:
                    return 30;

                case WindyLindyEvents.GroupiesGoodbyeJam:
                    return 25;

                default:
                    throw new ArgumentException("Don't have a price for windy lindy event: {0}".FormatWith(instance));

            }
        } 
    }

}
