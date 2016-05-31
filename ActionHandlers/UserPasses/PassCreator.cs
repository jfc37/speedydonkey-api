using System;
using Common.Extensions;
using Data.CodeChunks;
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
            if (passType.EqualsEnum(PassType.Unlimited))
                return new UnlimitedPassCreator(_classRepository);

            if (passType.EqualsEnum(PassType.Clip))
                return new ClipPassCreator();

            throw new ArgumentException(String.Format("Don't know how to create passes of type {0}", passType));
        }
    }

    public interface IPassCreator
    {
        Pass CreatePass(PassTemplate passTemplate);
    }

    public abstract class PassCreator : IPassCreator
    {
        public abstract Pass CreatePass(PassTemplate passTemplate);

        protected void PopulatePass(Pass pass, PassTemplate passTemplate)
        {
            pass = new GetPopulatedPassFromPassTemplate(pass, passTemplate).Do();

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

        public override Pass CreatePass(PassTemplate passTemplate)
        {
            var pass = new Pass();

            PopulatePass(pass, passTemplate);

            if (pass.Cost > 0)
            {
                pass.PassStatistic.CostPerClass = new GetCostPerClassForUnlimitedPass(_repository, pass).Do(); 
            }

            return pass;
        }
    }

    public class ClipPassCreator : PassCreator
    {
        public override Pass CreatePass(PassTemplate passTemplate)
        {
            var pass = new ClipPass
            {
                ClipsRemaining = passTemplate.ClassesValidFor
            };

            PopulatePass(pass, passTemplate);

            return pass;
        }
    }
}
