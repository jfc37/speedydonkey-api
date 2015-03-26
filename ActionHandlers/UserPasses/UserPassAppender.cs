using System.Collections.Generic;
using ActionHandlers.EnrolmentProcess;
using Models;

namespace ActionHandlers.UserPasses
{
    public interface IUserPassAppender
    {
        void AddPassToUser(User user, IPass pass);
    }
    public class UserPassAppender : IUserPassAppender
    {
        private readonly IPassCreatorFactory _passCreatorFactory;

        public UserPassAppender(IPassCreatorFactory passCreatorFactory)
        {
            _passCreatorFactory = passCreatorFactory;
        }

        public void AddPassToUser(User user, IPass pass)
        {
            if (user.Passes == null)
                user.Passes = new List<IPass>();

            var createdPass = _passCreatorFactory.Get(pass.PassType).CreatePass();
            createdPass.PaymentStatus = pass.PaymentStatus;
            user.Passes.Add(createdPass);
        }
    }
}