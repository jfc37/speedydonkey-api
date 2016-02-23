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
        public static Pass ToEntity(this PassModel instance)
        {
            if (instance.IsNull())
                return null;

            return new Pass
            {
                Id = instance.Id,
                Cost = instance.Cost,
                Description = instance.Description,
                EndDate = instance.EndDate,
                PassType = instance.PassType,
                PaymentStatus = instance.PaymentStatus,
                Note = instance.Note,
                Owner = instance.Owner.ToEntity(),
                PassStatistic = instance.PassStatistic.ToEntity(),
                StartDate = instance.StartDate
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

        public static User ToEntity(this UserModel instance)
        {
            if (instance.IsNull())
                return null;

            return new User
            {
                Note = instance.Note,
                Email = instance.Email,
                EnroledBlocks = instance.EnroledBlocks.SelectIfNotNull(x => x.ToEntity()).ToListIfNotNull(),
                FirstName = instance.FirstName,
                Passes = instance.Passes.SelectIfNotNull(x => x.ToEntity()).ToListIfNotNull(),
                Schedule = instance.Schedule.SelectIfNotNull(x => x.ToEntity()).ToListIfNotNull(),
                Surname = instance.Surname,
                Password = instance.Password,
                Id = instance.Id
            };
        }



        public static Block ToEntity(this BlockModel instance)
        {
            if (instance.IsNull())
                return null;

            return new Block
            {
                Announcements = instance.Announcements.SelectIfNotNull(x => x.ToEntity()).ToListIfNotNull(),
                Teachers = instance.Teachers.SelectIfNotNull(x => x.ToEntity()).ToListIfNotNull(),
                EndDate = instance.EndDate,
                StartDate = instance.StartDate,
                Classes = instance.Classes.SelectIfNotNull(x => x.ToEntity()).ToListIfNotNull(),
                EnroledStudents = instance.EnroledStudents.SelectIfNotNull(x => x.ToEntity()).ToListIfNotNull(),
                Name = instance.Name,
                Id = instance.Id,
                MinutesPerClass = instance.MinutesPerClass,
                NumberOfClasses = instance.NumberOfClasses,
                Room = instance.Room.ToEntity(),
                IsInviteOnly = instance.IsInviteOnly
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

        public static Class ToEntity(this ClassModel instance)
        {
            if (instance.IsNull())
                return null;

            return new Class
            {
                Teachers = instance.Teachers.SelectIfNotNull(x => x.ToEntity()).ToListIfNotNull(),
                Name = instance.Name,
                EndTime = instance.EndTime,
                StartTime = instance.StartTime,
                ActualStudents = instance.ActualStudents.SelectIfNotNull(x => x.ToEntity()).ToListIfNotNull(),
                Block = instance.Block.ToEntity(),
                PassStatistics = instance.PassStatistics.SelectIfNotNull(x => x.ToEntity()).ToListIfNotNull(),
                RegisteredStudents = instance.RegisteredStudents.SelectIfNotNull(x => x.ToEntity()).ToListIfNotNull(),
                Id = instance.Id
            };
        }

        public static Event ToEntity(this EventModel instance)
        {
            if (instance.IsNull())
                return null;

            Event model = null;

            var classInstance = instance as ClassModel;
            if (classInstance.IsNotNull())
                model = classInstance.ToEntity();

            var standAloneEventInstance = instance as StandAloneEventModel;
            if (standAloneEventInstance.IsNotNull())
                model = standAloneEventInstance.ToEntity();

            if (model.IsNull())
                throw new ArgumentException(
                    "Can't handle converting to entity for type {0}".FormatWith(instance.GetType()), "instance");

            model.Teachers = instance.Teachers.SelectIfNotNull(x => x.ToEntity()).ToListIfNotNull();
            model.Name = instance.Name;
            model.EndTime = instance.EndTime;
            model.StartTime = instance.StartTime;
            model.RegisteredStudents = instance.RegisteredStudents.SelectIfNotNull(x => x.ToEntity()).ToListIfNotNull();
            model.ActualStudents = instance.ActualStudents.SelectIfNotNull(x => x.ToEntity()).ToListIfNotNull();

            return model;
        }

        public static StandAloneEvent ToEntity(this StandAloneEventModel instance)
        {
            if (instance.IsNull())
                return null;

            return new StandAloneEvent
            {
                Id = instance.Id,
                Teachers = instance.Teachers.SelectIfNotNull(x => x.ToEntity()).ToListIfNotNull(),
                Name = instance.Name,
                EndTime = instance.EndTime,
                StartTime = instance.StartTime,
                RegisteredStudents = instance.RegisteredStudents.SelectIfNotNull(x => x.ToEntity()).ToListIfNotNull(),
                ActualStudents = instance.ActualStudents.SelectIfNotNull(x => x.ToEntity()).ToListIfNotNull(),
                Price = instance.Price,
                IsPrivate = instance.IsPrivate
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