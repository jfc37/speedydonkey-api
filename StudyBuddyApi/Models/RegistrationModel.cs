using System;
using System.Net.Http;
using Common;
using Models;
using SpeedyDonkeyApi.Services;

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
        public string PaymentStatus { get; set; }

    }
}