using System;
using System.Collections.Generic;
using Data.CodeChunks;
using Models;

namespace ActionHandlers.UserPasses
{
    public interface IUserPassAppender
    {
        void AddPassToUser(User user, string paymentStatus, PassTemplate passTemplate);
    }
    public class UserPassAppender : IUserPassAppender
    {
        private readonly IPassCreatorFactory _passCreatorFactory;

        public UserPassAppender(IPassCreatorFactory passCreatorFactory)
        {
            _passCreatorFactory = passCreatorFactory;
        }

        public void AddPassToUser(User user, string paymentStatus, PassTemplate passTemplate)
        {
            if (user.Passes == null)
                user.Passes = new List<Pass>();

            var startDate = new GetStartDateForUsersPurchasedPass(user)
                .Do();
            var createdPass = _passCreatorFactory
                .Get(passTemplate.PassType)
                .CreatePass(startDate, passTemplate);

            createdPass.PaymentStatus = paymentStatus;
            createdPass.CreatedDateTime = DateTime.Now;

            user.Passes.Add(createdPass);
        }
    }
}