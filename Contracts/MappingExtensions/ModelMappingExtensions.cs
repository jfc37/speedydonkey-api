using System;
using Common.Extensions;
using Contracts.Announcements;
using Contracts.Blocks;
using Contracts.Classes;
using Contracts.Events;
using Contracts.Passes;
using Contracts.Rooms;
using Contracts.Teachers;
using Contracts.Users;
using Contracts.WindyLindy;
using Models;

namespace Contracts.MappingExtensions
{
    public static class ModelMappingExtensions
    {
        public static RegistrationModel ToModel(this Registration instance)
        {
            if (instance.IsNull())
                return null;

            return new RegistrationModel
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

        public static PassTemplateModel ToModel(this PassTemplate instance)
        {
            if (instance.IsNull())
                return null;

            return new PassTemplateModel
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

        public static RoomModel ToModel(this Room instance)
        {
            if (instance.IsNull())
                return null;

            return new RoomModel
            {
                Id = instance.Id,
                Location = instance.Location,
                Name = instance.Name
            };
        }

        public static RoomModel ToStripedModel(this Room instance)
        {
            if (instance.IsNull())
                return null;

            return new RoomModel
            {
                Id = instance.Id,
                Location = instance.Location,
                Name = instance.Name
            };
        }

        public static AnnouncementModel ToModel(this Announcement instance)
        {
            if (instance.IsNull())
                return null;

            return new AnnouncementModel
            {
                Id = instance.Id,
                Message = instance.Message,
                NotifyAll = instance.NotifyAll,
                Subject = instance.Subject,
                Receivers = instance.Receivers.SelectIfNotNull(x => x.ToStripedModel()).ToListIfNotNull(),
            };
        }

        public static TeacherModel ToModel(this Teacher instance)
        {
            if (instance.IsNull())
                return null;

            return new TeacherModel
            {
                User = instance.User.ToStripedModel(),
                Classes = instance.Classes.SelectIfNotNull(x => x.ToStripedModel()).ToListIfNotNull(),
                Id = instance.Id,
                FirstName = instance.User.IsNull() ? null : instance.User.FirstName,
                Surname = instance.User.IsNull() ? null : instance.User.Surname
            };
        }

        public static TeacherModel ToStripedModel(this Teacher instance)
        {
            if (instance.IsNull())
                return null;

            return new TeacherModel
            {
                Id = instance.Id,
                FirstName = instance.User.IsNull() ? null : instance.User.FirstName,
                Surname = instance.User.IsNull() ? null : instance.User.Surname
            };
        }

        public static PassStatisticModel ToModel(this PassStatistic instance)
        {
            if (instance.IsNull())
                return null;

            return new PassStatisticModel
            {
                CostPerClass = instance.CostPerClass,
                NumberOfClassesAttended = instance.NumberOfClassesAttended,
                Pass = instance.Pass.ToModel(),
                Id = instance.Id
            };
        }
    }
}