using Common.Extensions;
using Models;

namespace SpeedyDonkeyApi.Models
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

        public static BookingModel ToModel(this Booking instance)
        {
            if (instance.IsNull())
                return null;

            return new BookingModel
            {
                Room = instance.Room,
                Event = instance.Event
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
                Level = instance.Level.ToStripedModel(),
                Name = instance.Name,
                Id = instance.Id
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
                Level = instance.Level.ToStripedModel(),
                Name = instance.Name,
                Id = instance.Id
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
                Receivers = instance.Receivers.SelectIfNotNull(x => x.ToModel()).ToListIfNotNull(),
                ShowFrom = instance.ShowFrom,
                ShowUntil = instance.ShowUntil,
                Type = instance.Type
            };
        }

        public static LevelModel ToModel(this Level instance)
        {
            if (instance.IsNull())
                return null;

            return new LevelModel
            {
                Teachers = instance.Teachers.SelectIfNotNull(x => x.ToModel()).ToListIfNotNull(),
                Blocks = instance.Blocks.SelectIfNotNull(x => x.ToModel()).ToListIfNotNull(),
                Name = instance.Name,
                ClassMinutes = instance.ClassMinutes,
                ClassesInBlock = instance.ClassesInBlock,
                EndTime = instance.EndTime,
                Room = instance.Room,
                StartTime = instance.StartTime,
                Id = instance.Id
            };
        }

        public static LevelModel ToStripedModel(this Level instance)
        {
            if (instance.IsNull())
                return null;

            return new LevelModel
            {
                Name = instance.Name,
                ClassMinutes = instance.ClassMinutes,
                ClassesInBlock = instance.ClassesInBlock,
                EndTime = instance.EndTime,
                StartTime = instance.StartTime,
                Id = instance.Id
            };
        }

        public static TeacherModel ToModel(this Teacher instance)
        {
            if (instance.IsNull())
                return null;

            return new TeacherModel
            {
                User = instance.User.ToModel(),
                Classes = instance.Classes.SelectIfNotNull(x => x.ToModel()).ToListIfNotNull(),
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

            return new EventModel
            {
                Teachers = instance.Teachers.SelectIfNotNull(x => x.ToStripedModel()).ToListIfNotNull(),
                Name = instance.Name,
                EndTime = instance.EndTime,
                StartTime = instance.StartTime,
                RegisteredStudents = instance.RegisteredStudents.SelectIfNotNull(x => x.ToStripedModel()).ToListIfNotNull()
            };
        }

        public static ClassModel ToModel(this Class instance)
        {
            if (instance.IsNull())
                return null;

            return new ClassModel
            {
                Teachers = instance.Teachers.SelectIfNotNull(x => x.ToStripedModel()).ToListIfNotNull(),
                Name = instance.Name,
                EndTime = instance.EndTime,
                StartTime = instance.StartTime,
                ActualStudents = instance.ActualStudents.SelectIfNotNull(x => x.ToStripedModel()).ToListIfNotNull(),
                Block = instance.Block.ToModel(),
                RegisteredStudents = instance.RegisteredStudents.SelectIfNotNull(x => x.ToStripedModel()).ToListIfNotNull(),
                Id = instance.Id
            };
        }

        public static ClassModel ToStripedModel(this Class instance)
        {
            if (instance.IsNull())
                return null;

            return new ClassModel
            {
                Name = instance.Name,
                EndTime = instance.EndTime,
                StartTime = instance.StartTime,
                Id = instance.Id
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

        public static Booking ToEntity(this BookingModel instance)
        {
            if (instance.IsNull())
                return null;

            return new Booking
            {
                Room = instance.Room,
                Event = instance.Event
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
                Level = instance.Level.ToEntity(),
                Name = instance.Name,
                Id = instance.Id
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
                Receivers = instance.Receivers.SelectIfNotNull(x => x.ToEntity()).ToListIfNotNull(),
                ShowFrom = instance.ShowFrom,
                ShowUntil = instance.ShowUntil,
                Type = instance.Type
            };
        }

        public static Level ToEntity(this LevelModel instance)
        {
            if (instance.IsNull())
                return null;

            return new Level
            {
                Teachers = instance.Teachers.SelectIfNotNull(x => x.ToEntity()).ToListIfNotNull(),
                Blocks = instance.Blocks.SelectIfNotNull(x => x.ToEntity()).ToListIfNotNull(),
                Name = instance.Name,
                ClassMinutes = instance.ClassMinutes,
                ClassesInBlock = instance.ClassesInBlock,
                EndTime = instance.EndTime,
                Room = instance.Room,
                StartTime = instance.StartTime,
                Id = instance.Id
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