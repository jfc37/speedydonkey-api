using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Common;
using Models;
using Models.OnlinePayments;

namespace SpeedyDonkeyApi.Models
{
    public class RegistrationModel : IEntity
    {
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

        public string PhoneNumber { get; set; }

        public string CountryOfResidence { get; set; }

        public string City { get; set; }

        public string EmergencyContactPerson { get; set; }

        public string EmergencyContactNumber { get; set; }

        public bool? Over18 { get; set; }

        [Required]
        public bool? FullPass { get; set; }
        public DanceRole? DanceRole { get; set; }
        public DanceLevel? LindyLevel { get; set; }
        public DanceLevel? BalboaLevel { get; set; }
        public DanceLevel? BluesLevel { get; set; }

        public List<string> Classes { get; set; }
        public List<WindyLindyEvents> Events { get; set; }

        public bool Novice { get; set; }
        public string NovicePartner { get; set; }

        public bool Balboa { get; set; }
        public string BalboaPartner { get; set; }

        public bool Hellzapoppin { get; set; }
        public string HellzapoppinPartner { get; set; }

        public bool JackAndJill { get; set; }

        public bool ShowcaseCouple { get; set; }
        public string ShowcaseCouplePartner { get; set; }

        public bool ShowcaseTeam { get; set; }
        public string ShowcaseTeamPartner { get; set; }

        public bool SoloJazz { get; set; }

        public bool StrictlyLindy { get; set; }
        public string StrictlyLindyPartner { get; set; }
        public bool PerformAtGrammy { get; set; }
        public bool Aerials { get; set; }
        public string AerialsPartner { get; set; }
        public string AerialsTeachers { get; set; }
        public bool TermsAndConditions { get; set; }
        public int Id { get; set; }
    }
}