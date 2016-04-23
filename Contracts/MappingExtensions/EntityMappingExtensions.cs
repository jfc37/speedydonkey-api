using Common.Extensions;
using Contracts.Announcements;
using Contracts.Passes;
using Contracts.Rooms;
using Contracts.Teachers;
using Contracts.Users;
using Contracts.WindyLindy;
using Models;

namespace Contracts.MappingExtensions
{
    public static class EntityMappingExtensions
    {
        public static Registration ToEntity(this RegistrationModel instance)
        {
            if (instance.IsNull())
                return null;

            return new Registration
            {
                Aerials = instance.Aerials,
                Classes = instance.Classes.ToListIfNotNull(),
                FirstName = instance.FirstName,
                Email = instance.Email,
                PaymentStatus = instance.PaymentStatus,
                Surname = instance.Surname,
                AerialsPartner = instance.AerialsPartner,
                AerialsTeachers = instance.AerialsTeachers,
                Amount = instance.Amount,
                Balboa = instance.Balboa,
                BalboaLevel = instance.BalboaLevel,
                BalboaPartner = instance.BalboaPartner,
                BluesLevel = instance.BluesLevel,
                City = instance.City,
                CountryOfResidence = instance.CountryOfResidence,
                Id = instance.Id,
                DanceRole = instance.DanceRole,
                EmergencyContactNumber = instance.EmergencyContactNumber,
                RegistationId = instance.RegistationId,
                EmergencyContactPerson = instance.EmergencyContactPerson,
                Events = instance.Events.ToListIfNotNull(),
                FullPass = instance.FullPass,
                Hellzapoppin = instance.Hellzapoppin,
                HellzapoppinPartner = instance.HellzapoppinPartner,
                JackAndJill = instance.JackAndJill,
                LindyLevel = instance.LindyLevel,
                Novice = instance.Novice,
                NovicePartner = instance.NovicePartner,
                Over18 = instance.Over18,
                PerformAtGrammy = instance.PerformAtGrammy,
                PhoneNumber = instance.PhoneNumber,
                ShowcaseCouple = instance.ShowcaseCouple,
                ShowcaseCouplePartner = instance.ShowcaseCouplePartner,
                ShowcaseTeam = instance.ShowcaseTeam,
                ShowcaseTeamPartner = instance.ShowcaseTeamPartner,
                SoloJazz = instance.SoloJazz,
                StrictlyLindy = instance.StrictlyLindy,
                StrictlyLindyPartner = instance.StrictlyLindyPartner,
                TermsAndConditions = instance.TermsAndConditions
            };
        }
        
        public static PassTemplate ToEntity(this PassTemplateModel instance)
        {
            if (instance.IsNull())
                return null;

            return new PassTemplate
            {
                Id = instance.Id,
                Cost = instance.Cost,
                Description = instance.Description,
                PassType = instance.PassType,
                AvailableForPurchase = instance.AvailableForPurchase,
                ClassesValidFor = instance.ClassesValidFor,
                WeeksValidFor = instance.WeeksValidFor
            };
        }

        public static Room ToEntity(this RoomModel instance)
        {
            if (instance.IsNull())
                return null;

            return new Room
            {
                Id = instance.Id,
                Location = instance.Location,
                Name = instance.Name
            };
        }
        
        public static Announcement ToEntity(this AnnouncementModel instance)
        {
            if (instance.IsNull())
                return null;

            return new Announcement
            {
                Id = instance.Id,
                Message = instance.Message,
                NotifyAll = instance.NotifyAll,
                Subject = instance.Subject,
                Receivers = instance.Receivers.SelectIfNotNull(x => x.ToEntity()).ToListIfNotNull()
            };
        }
        
        public static Teacher ToEntity(this TeacherModel instance)
        {
            if (instance.IsNull())
                return null;

            return new Teacher
            {
                User = instance.User.ToEntity(),
                Classes = instance.Classes.SelectIfNotNull(x => x.ToEntity()).ToListIfNotNull(),
                Id = instance.Id
            };
        }
        
        public static PassStatistic ToEntity(this PassStatisticModel instance)
        {
            if (instance.IsNull())
                return null;

            return new PassStatistic
            {
                CostPerClass = instance.CostPerClass,
                NumberOfClassesAttended = instance.NumberOfClassesAttended,
                Pass = instance.Pass.ToEntity(),
                Id = instance.Id
            };
        }
    }
}