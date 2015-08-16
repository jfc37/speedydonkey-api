using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Common;
using Models;
using Models.OnlinePayments;

namespace SpeedyDonkeyApi.Models
{
    public class RegistrationModel : ApiModel<Registration, RegistrationModel>, IRegistration
    {

        protected override string RouteName
        {
            get { return "RegistrationApi"; }
        }

        public DateTime CreatedDateTime { get; set; }
        public DateTime? LastUpdatedDateTime { get; set; }
        public Guid RegistationId { get; set; }
        public decimal Amount { get; set; }
        public OnlinePaymentStatus PaymentStatus { get; set; }
        
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string CountryOfResidence { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string EmergencyContactPerson { get; set; }

        [Required]
        public string EmergencyContactNumber { get; set; }

        [Required]
        public bool? Over18 { get; set; }

        [Required]
        public bool? FullPass { get; set; }
        [Required]
        public DanceRole? DanceRole { get; set; }
        [Required]
        public DanceLevel? LindyLevel { get; set; }
        [Required]
        public DanceLevel? BalboaLevel { get; set; }
        [Required]
        public DanceLevel? BluesLevel { get; set; }

        public ICollection<string> Classes { get; set; }
        public ICollection<WindyLindyEvents> Events { get; set; }

        [Required]
        public bool Novice { get; set; }
        public string NovicePartner { get; set; }

        [Required]
        public bool Balboa { get; set; }
        public string BalboaPartner { get; set; }

        [Required]
        public bool Hellzapoppin { get; set; }
        public string HellzapoppinPartner { get; set; }

        [Required]
        public bool JackAndJill { get; set; }

        [Required]
        public bool ShowcaseCouple { get; set; }
        public string ShowcaseCouplePartner { get; set; }

        [Required]
        public bool ShowcaseTeam { get; set; }
        public string ShowcaseTeamPartner { get; set; }

        [Required]
        public bool SoloJazz { get; set; }

        [Required]
        public bool StrictlyLindy { get; set; }
        public string StrictlyLindyPartner { get; set; }
        public bool PerformAtGrammy { get; set; }
        public bool Aerials { get; set; }
        public string AerialsPartner { get; set; }
        public string AerialsTeachers { get; set; }
        public bool TermsAndConditions { get; set; }

        protected override void AddChildrenToEntity(Registration entity, ICommonInterfaceCloner cloner)
        {
            entity.Classes = Classes;
            entity.Events = Events;
        }
    }
}