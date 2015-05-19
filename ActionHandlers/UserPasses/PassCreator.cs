using System;
using System.Linq;
using Data.Repositories;
using Models;

namespace ActionHandlers.UserPasses
{
    public interface IPassCreatorFactory
    {
        IPassCreator Get(string passType);
    }

    public class PassCreatorFactory : IPassCreatorFactory
    {
        private readonly IRepository<Class> _classRepository;

        public PassCreatorFactory(IRepository<Class> classRepository)
        {
            _classRepository = classRepository;
        }

        public IPassCreator Get(string passType)
        {
            if (passType.ToLower() == PassType.Unlimited.ToString().ToLower())
                return new UnlimitedPassCreator(_classRepository);

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
            pass.Description = passTemplate.Description;
            pass.PassStatistic = new PassStatistic{ CreatedDateTime = DateTime.Now };
        }
    }

    public class UnlimitedPassCreator : PassCreator
    {
        private readonly IRepository<Class> _repository;

        public UnlimitedPassCreator(IRepository<Class> repository)
        {
            _repository = repository;
        }

        public override IPass CreatePass(DateTime startDate, PassTemplate passTemplate)
        {
            var pass = new Pass();

            PopulatePass(pass, startDate, passTemplate);

            if (pass.Cost > 0)
            {
                var numberOfClassesAvailableForPass = _repository.GetAll().Where(x => pass.StartDate <= x.StartTime && x.StartTime <= pass.EndDate).Count();
                if (numberOfClassesAvailableForPass == 0)
                    pass.PassStatistic.CostPerClass = 0;
                else
                    pass.PassStatistic.CostPerClass = pass.Cost / numberOfClassesAvailableForPass;   
            }

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
