using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public string Phone { get; set; }

        [Required]
        public string CountryOfResidence { get; set; }

        [Required]
        public string EmergancyContactName { get; set; }

        [Required]
        public string EmergancyContactNumber { get; set; }
        public bool Over18 { get; set; }
        public bool FullPass { get; set; }
        [Required]
        public Style Style { get; set; }
        [Required]
        public DanceLevel LindyLevel { get; set; }
        [Required]
        public DanceLevel BalboaLevel { get; set; }
        [Required]
        public DanceLevel BluesLevel { get; set; }
        public List<string> Classes { get; set; }
        public List<string> Events { get; set; }
        public List<ICompetitionRegistration> Competitions { get; set; }
    }
}