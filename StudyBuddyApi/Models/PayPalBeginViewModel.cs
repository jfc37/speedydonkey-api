using System.ComponentModel.DataAnnotations;
using Action.OnlinePayment;
using Common;
using Models;

namespace SpeedyDonkeyApi.Models
{
    public class PayPalBeginViewModel
    {
        [Required]
        public int TemplateId { get; set; }

        //[Required]
        public string ItemType { get; set; }

        [Required]
        public string ReturnUrl { get; set; }

        [Required]
        public string CancelUrl { get; set; }

        public BeginOnlinePayment ToAction(ICurrentUser currentUser)
        {
            var pendingOnlinePayment = new PendingOnlinePayment
            {
                ItemType = ItemType,
                TemplateId = TemplateId,
                UserId = currentUser.Id
            };
            return new BeginOnlinePayment(pendingOnlinePayment, ReturnUrl, CancelUrl);
        }
    }
}