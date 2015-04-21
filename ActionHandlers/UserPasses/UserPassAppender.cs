using System;
using System.Collections.Generic;
using System.Linq;
using ActionHandlers.EnrolmentProcess;
using Models;

namespace ActionHandlers.UserPasses
{
    public interface IUserPassAppender
    {
        void AddPassToUser(User user, IPass pass, PassTemplate passTemplate);
    }
    public class UserPassAppender : IUserPassAppender
    {
        private readonly IPassCreatorFactory _passCreatorFactory;

        public UserPassAppender(IPassCreatorFactory passCreatorFactory)
        {
            _passCreatorFactory = passCreatorFactory;
        }

        public void AddPassToUser(User user, IPass pass, PassTemplate passTemplate)
        {
            if (user.Passes == null)
                user.Passes = new List<IPass>();

            var validPasses = user.Passes.OfType<Pass>().Where(x => x.IsValid() || x.IsFuturePass()).ToList();
            var startDate = validPasses.Any() ? validPasses.Max(x => x.EndDate).AddDays(1).Date : DateTime.Now.Date;
            var createdPass = _passCreatorFactory.Get(passTemplate.PassType).CreatePass(startDate, passTemplate);
            createdPass.PaymentStatus = pass.PaymentStatus;


            user.Passes.Add(createdPass);
        }
    }
}