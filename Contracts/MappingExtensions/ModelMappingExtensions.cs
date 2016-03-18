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

        public static UserModel ToModel(this User instance)
        {
            if (instance.IsNull())
                return null;

            return new UserModel
            {
                Note = instance.Note,
                Email = instance.Email,
                EnroledBlocks = instance.EnroledBlocks.SelectIfNotNull(x => x.ToStripedModel()).ToListIfNotNull(),
                FirstName = instance.FirstName,
                Passes = instance.Passes.SelectIfNotNull(x => x.ToStripedModel()).ToListIfNotNull(),
                Surname = instance.Surname,
                Id = instance.Id
            };
        }

        public static UserModel ToStripedModel(this User instance)
        {
            if (instance.IsNull())
                return null;

            return new UserModel
            {
                Email = instance.Email,
                FirstName = instance.FirstName,
                Surname = instance.Surname,
                Id = instance.Id
            };
        }

        public static BlockModel ToModel(this Block instance)
        {
            if (instance.IsNull())
                return null;

            return new BlockModel
            {
                Teachers = instance.Teachers.SelectIfNotNull(x => x.ToStripedModel()).ToListIfNotNull(),
                EndDate = instance.EndDate,
                StartDate = instance.StartDate,
                Classes = instance.Classes.SelectIfNotNull(x => x.ToStripedModel()).ToListIfNotNull(),
                EnroledStudents = instance.EnroledStudents.SelectIfNotNull(x => x.ToStripedModel()).ToListIfNotNull(),
                Name = instance.Name,
                Id = instance.Id,
                MinutesPerClass = instance.MinutesPerClass,
                NumberOfClasses = instance.NumberOfClasses,
                Room = instance.Room.ToStripedModel(),
                IsInviteOnly = instance.IsInviteOnly
            };
        }

        public static BlockModel ToStripedModel(this Block instance)
        {
            if (instance.IsNull())
                return null;

            return new BlockModel
            {
                Teachers = instance.Teachers.SelectIfNotNull(x => x.ToStripedModel()).ToListIfNotNull(),
                EndDate = instance.EndDate,
                StartDate = instance.StartDate,
                Name = instance.Name,
                Id = instance.Id,
                MinutesPerClass = instance.MinutesPerClass,
                NumberOfClasses = instance.NumberOfClasses,
                Room = instance.Room.ToStripedModel(),
                IsInviteOnly = instance.IsInviteOnly
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

        public static EventModel ToModel(this Event instance)
        {
            if (instance.IsNull())
                return null;

            EventModel model = null;

            var classInstance = instance as Class;
            if (classInstance.IsNotNull())
                model = classInstance.ToModel();

            var standAloneEventInstance = instance as StandAloneEvent;
            if (standAloneEventInstance.IsNotNull())
                model = standAloneEventInstance.ToModel();

            if (model.IsNull())
                throw new ArgumentException(
                    "Can't handle converting to model for type {0}".FormatWith(instance.GetType()), "instance");

            model.Id = instance.Id;
            model.Teachers = instance.Teachers.SelectIfNotNull(x => x.ToStripedModel()).ToListIfNotNull();
            model.Name = instance.Name;
            model.EndTime = instance.EndTime;
            model.StartTime = instance.StartTime;
            model.Room = instance.Room.ToStripedModel();
            model.RegisteredStudents = instance.RegisteredStudents.SelectIfNotNull(x => x.ToStripedModel()).ToListIfNotNull();
            model.ActualStudents = instance.ActualStudents.SelectIfNotNull(x => x.ToStripedModel()).ToListIfNotNull();

            return model;

        }

        private static StandAloneEventModel ToModel(this StandAloneEvent instance)
        {
            if (instance.IsNull())
                return null;

            return new StandAloneEventModel
            {
                Id = instance.Id,
                Teachers = instance.Teachers.SelectIfNotNull(x => x.ToStripedModel()).ToListIfNotNull(),
                Name = instance.Name,
                EndTime = instance.EndTime,
                StartTime = instance.StartTime,
                Room = instance.Room.ToStripedModel(),
                Price = instance.Price,
                IsPrivate = instance.IsPrivate
            };
        }

        private static ClassModel ToModel(this Class instance)
        {
            if (instance.IsNull())
                return null;

            return new ClassModel
            {
                Block = instance.Block.ToStripedModel(),
                ActualStudents = instance.ActualStudents.SelectIfNotNull(x => x.ToStripedModel()).ToListIfNotNull()
            };
        }

        private static ClassModel ToStripedModel(this Class instance)
        {
            if (instance.IsNull())
                return null;

            return new ClassModel
            {
                Name = instance.Name,
                EndTime = instance.EndTime,
                StartTime = instance.StartTime,
                Room = instance.Room.ToStripedModel(),
                Id = instance.Id
            };
        }

        private static StandAloneEventModel ToStripedModel(this StandAloneEvent instance)
        {
            if (instance.IsNull())
                return null;

            return new StandAloneEventModel
            {
                Price = instance.Price,
                IsPrivate = instance.IsPrivate
            };
        }

        public static EventModel ToStripedModel(this Event instance)
        {
            if (instance.IsNull())
                return null;

            EventModel model = null;

            var classInstance = instance as Class;
            if (classInstance.IsNotNull())
                model = classInstance.ToStripedModel();

            var standAloneEventInstance = instance as StandAloneEvent;
            if (standAloneEventInstance.IsNotNull())
                model = standAloneEventInstance.ToStripedModel();

            if (model.IsNull())
                throw new ArgumentException(
                    "Can't handle converting to striped model for type {0}".FormatWith(instance.GetType()), "instance");

            model.Id = instance.Id;
            model.Name = instance.Name;
            model.EndTime = instance.EndTime;
            model.StartTime = instance.StartTime;

            return model;
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