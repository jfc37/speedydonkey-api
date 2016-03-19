using Common.Extensions;
using Contracts.Passes;
using Models;

namespace Contracts.MappingExtensions
{
    public static class PassMappingExtensions
    {
        #region Model to Entity

        public static Pass ToEntity(this PassModel instance)
        {
            if (instance.IsNull())
                return null;

            var pass = new Pass();
            PopulateEntity(pass, instance);

            return pass;
        }

        public static ClipPass ToEntity(this ClipPassModel instance)
        {
            if (instance.IsNull())
                return null;

            var pass = new ClipPass();
            PopulateEntity(pass, instance);
            pass.ClipsRemaining = instance.ClipsRemaining;

            return pass;
        }

        private static void PopulateEntity(Pass entity, PassModel instance)
        {
            entity.Id = instance.Id;
            entity.Cost = instance.Cost;
            entity.Description = instance.Description;
            entity.EndDate = instance.EndDate;
            entity.PassType = instance.PassType;
            entity.PaymentStatus = instance.PaymentStatus;
            entity.Note = instance.Note;
            entity.Owner = instance.Owner.ToEntity();
            entity.PassStatistic = instance.PassStatistic.ToEntity();
            entity.StartDate = instance.StartDate;
        }

        #endregion

        #region Entity to Model

        public static PassModel ToModel(this Pass instance)
        {
            var clipPass = instance as ClipPass;
            if (clipPass != null)
                return clipPass.ToModel();

            if (instance.IsNull())
                return null;

            return new PassModel
            {
                Id = instance.Id,
                Cost = instance.Cost,
                Description = instance.Description,
                EndDate = instance.EndDate,
                PassType = instance.PassType,
                PaymentStatus = instance.PaymentStatus,
                Note = instance.Note,
                Owner = instance.Owner.ToStripedModel(),
                StartDate = instance.StartDate,
                CreatedDateTime = instance.CreatedDateTime
            };
        }

        public static PassModel ToStripedModel(this Pass instance)
        {
            var clipPass = instance as ClipPass;
            if (clipPass != null)
                return clipPass.ToStripedModel();

            if (instance.IsNull())
                return null;

            return new PassModel
            {
                Id = instance.Id,
                Cost = instance.Cost,
                Description = instance.Description,
                EndDate = instance.EndDate,
                PassType = instance.PassType,
                PaymentStatus = instance.PaymentStatus,
                Note = instance.Note,
                StartDate = instance.StartDate,
                CreatedDateTime = instance.CreatedDateTime
            };
        }

        public static ClipPassModel ToModel(this ClipPass instance)
        {
            if (instance.IsNull())
                return null;

            return new ClipPassModel
            {
                Id = instance.Id,
                Cost = instance.Cost,
                Description = instance.Description,
                EndDate = instance.EndDate,
                PassType = instance.PassType,
                PaymentStatus = instance.PaymentStatus,
                Note = instance.Note,
                Owner = instance.Owner.ToStripedModel(),
                StartDate = instance.StartDate,
                ClipsRemaining = instance.ClipsRemaining,
                CreatedDateTime = instance.CreatedDateTime
            };
        }

        public static ClipPassModel ToStripedModel(this ClipPass instance)
        {
            if (instance.IsNull())
                return null;

            return new ClipPassModel
            {
                Id = instance.Id,
                Cost = instance.Cost,
                Description = instance.Description,
                EndDate = instance.EndDate,
                PassType = instance.PassType,
                PaymentStatus = instance.PaymentStatus,
                Note = instance.Note,
                StartDate = instance.StartDate,
                ClipsRemaining = instance.ClipsRemaining,
                CreatedDateTime = instance.CreatedDateTime
            };
        }

        #endregion

    }
}