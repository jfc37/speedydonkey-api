using System;
using Models;

namespace ActionHandlers.UserPasses
{
    public interface IPassCreatorFactory
    {
        IPassCreator Get(string passType);
    }

    public class PassCreatorFactory : IPassCreatorFactory
    {
        public IPassCreator Get(string passType)
        {
            if (passType.ToLower() == PassType.Unlimited.ToString().ToLower())
                return new UnlimitedPassCreator();

            if (passType.ToLower() == PassType.Clip.ToString().ToLower())
                return new ClipPassCreator();

            throw new ArgumentException(String.Format("Don't know how to create passes of type {0}", passType));
        }
    }

    public interface IPassCreator
    {
        IPass CreatePass(DateTime startDate, PassTemplate passTemplate);
    }

    public abstract class PassCreator : IPassCreator
    {
        public abstract IPass CreatePass(DateTime startDate, PassTemplate passTemplate);

        protected void PopulatePass(IPass pass, DateTime startDate, PassTemplate passTemplate)
        {
            pass.StartDate = startDate;
            pass.EndDate = startDate.AddDays(passTemplate.WeeksValidFor * 7);
            pass.Cost = passTemplate.Cost;
            pass.PassType = passTemplate.PassType;
        }
    }

    public class UnlimitedPassCreator : PassCreator
    {
        public override IPass CreatePass(DateTime startDate, PassTemplate passTemplate)
        {
            var pass = new Pass();

            PopulatePass(pass, startDate, passTemplate);

            return pass;
        }
    }

    public class ClipPassCreator : PassCreator
    {
        public override IPass CreatePass(DateTime startDate, PassTemplate passTemplate)
        {
            var pass = new ClipPass
            {
                ClipsRemaining = passTemplate.ClassesValidFor
            };

            PopulatePass(pass, startDate, passTemplate);

            return pass;
        }
    }
}
