using System;
using System.Collections.Generic;
using System.Linq;
using Actions;
using Common.Calculations;
using Common.Extensions;
using Data.Repositories;
using Models;
using Models.OnlinePayments;

namespace ActionHandlers.CreateHandlers
{
    public class CreateRegistrationHandler : CreateEntityHandler<CreateRegistration, Registration>
    {
        public CreateRegistrationHandler(IRepository<Registration> repository)
            : base(repository)
        {
        }

        protected override void PreHandle(ICrudAction<Registration> action)
        {
            action.ActionAgainst.RegistationId = Guid.NewGuid();
            action.ActionAgainst.PaymentStatus = OnlinePaymentStatus.Pending;
            action.ActionAgainst.Amount = new WindyLindyPriceCalculation(action.ActionAgainst)
                .Calculate()
                .Result();
        }
    }

    /// <summary>
    /// The price for a windy lindy pass
    /// </summary>
    public class WindyLindyPriceCalculation : ICalculation<decimal>
    {
        private readonly Registration _registration;

        public WindyLindyPriceCalculation(Registration registration)
        {
            _registration = registration;
        }

        public CalculationResult<decimal> Calculate()
        {
            var actualCalculation = GetCalculation();

            return actualCalculation.Calculate();
        }

        private ICalculation<decimal> GetCalculation()
        {
            if (_registration.FullPass.GetValueOrDefault())
                return new WindyLindyFullPassPriceCalculation();
            else
                return new WindyLindyPartialPassPriceCalculation(_registration);
        }
    }

    /// <summary>
    /// The price for a full windy lindy pass
    /// </summary>
    public class WindyLindyFullPassPriceCalculation : ICalculation<decimal>
    {
        private const decimal FullPassPrice = 390m;

        public CalculationResult<decimal> Calculate()
        {
            return new CalculationResult<decimal>(FullPassPrice);
        }
    }

    /// <summary>
    /// The price for a full windy lindy pass
    /// </summary>
    public class WindyLindyPartialPassPriceCalculation : ICalculation<decimal>
    {
        private readonly Registration _registration;

        public WindyLindyPartialPassPriceCalculation(Registration registration)
        {
            _registration = registration;
        }

        public CalculationResult<decimal> Calculate()
        {
            var partialPassPrice = GetPartialPassPrice();
            var fullPassPrice = new WindyLindyFullPassPriceCalculation()
                .Calculate()
                .Result();

            var actualPrice = Math.Min(partialPassPrice, fullPassPrice);
            return new CalculationResult<decimal>(actualPrice);
        }

        private decimal GetPartialPassPrice()
        {
            var classesPrice = new WindyLindyClassesPriceCalculation(_registration.Classes).Calculate();
            var eventsPrice = new WindyLindyEventsPriceCalculation(_registration.Events).Calculate();

            var totalPrice = classesPrice.Result() + eventsPrice.Result();
            return totalPrice;
        }
    }

    /// <summary>
    /// The price for attending n number of windy lindy classes
    /// </summary>
    public class WindyLindyClassesPriceCalculation : ICalculation<decimal>
    {
        private const decimal PerClassPrice = 40;
        private readonly IEnumerable<string> _classes;

        public WindyLindyClassesPriceCalculation(IEnumerable<string> classes)
        {
            _classes = classes;
        }

        public CalculationResult<decimal> Calculate()
        {
            if (_classes.IsNull())
                return new CalculationResult<decimal>(0);

            var result = _classes.Count()* PerClassPrice;
            return new CalculationResult<decimal>(result);
        }
    }

    /// <summary>
    /// The price for attending n number of windy lindy events
    /// </summary>
    public class WindyLindyEventsPriceCalculation : ICalculation<decimal>
    {
        private readonly IEnumerable<WindyLindyEvents> _events;

        public WindyLindyEventsPriceCalculation(IEnumerable<WindyLindyEvents> events)
        {
            _events = events;
        }

        public CalculationResult<decimal> Calculate()
        {
            if (_events.IsNull())
                return new CalculationResult<decimal>(0);

            var result = _events.Sum(windyLindyEvent => windyLindyEvent.GetPrice());

            return new CalculationResult<decimal>(result);
        }
    }

}
