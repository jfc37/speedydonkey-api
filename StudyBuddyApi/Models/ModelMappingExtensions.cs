using System.Linq;
using Models;

namespace SpeedyDonkeyApi.Models
{
    public static class ModelMappingExtensions
    {
        public static RegistrationModel ToModel(this Registration instance)
        {
            return new RegistrationModel
            {
                Aerials = instance.Aerials,
                Classes = instance.Classes.ToList(),
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
                Events = instance.Events.ToList(),
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
            return new PassModel
            {
                Id = instance.Id,
                Cost = instance.Cost,
                Description = instance.Description,
                EndDate = instance.EndDate,
                PassType = instance.PassType,
                PaymentStatus = instance.PaymentStatus,
                Note = instance.Note,
                Owner = instance.Owner.ToModel(),
                PassStatistic = instance.PassStatistic.ToModel(),
                StartDate = instance.StartDate
            };
        }

        public static UserModel ToModel(this User instance)
        {
            return new UserModel
            {
                Note = instance.Note,
                Email = instance.Email,
                EnroledBlocks = instance.EnroledBlocks.Select(x => x.ToModel()).ToList(),
                FirstName = instance.FirstName,
                Passes = instance.Passes.Select(x => x.ToModel()).ToList(),
                Schedule = instance.Schedule.Select(x => x.ToModel()).ToList(),
                Surname = instance.Surname
            };
        }

        public static BookingModel ToModel(this Booking instance)
        {
            return new BookingModel
            {
                Room = instance.Room,
                Event = instance.Event
            };
        }

        public static BlockModel ToModel(this Block instance)
        {
            return new BlockModel
            {
                Announcements = instance.Announcements.Select(x => x.ToModel()).ToList(),
                Teachers = instance.Teachers.Select(x => x.ToModel()).ToList(),
                EndDate = instance.EndDate,
                StartDate = instance.StartDate,
                Classes = instance.Classes.Select(x => x.ToModel()).ToList(),
                EnroledStudents = instance.EnroledStudents.Select(x => x.ToModel()).ToList(),
                Level = instance.Level.ToModel(),
                Name = instance.Name
            };
        }

        public static AnnouncementModel ToModel(this Announcement instance)
        {
            return new AnnouncementModel
            {
                Id = instance.Id,
                Message = instance.Message,
                NotifyAll = instance.NotifyAll,
                Receivers = instance.Receivers.Select(x => x.ToModel()).ToList(),
                ShowFrom = instance.ShowFrom,
                ShowUntil = instance.ShowUntil,
                Type = instance.Type
            };
        }

        public static LevelModel ToModel(this Level instance)
        {
            return new LevelModel
            {
                Teachers = instance.Teachers.Select(x => x.ToModel()).ToList(),
                Blocks = instance.Blocks.Select(x => x.ToModel()).ToList(),
                Name = instance.Name,
                ClassMinutes = instance.ClassMinutes,
                ClassesInBlock = instance.ClassesInBlock,
                EndTime = instance.EndTime,
                Room = instance.Room,
                StartTime = instance.StartTime
            };
        }


        public static TeacherModel ToModel(this Teacher instance)
        {
            return new TeacherModel
            {
                User = instance.User.ToModel(),
                Classes = instance.Classes.Select(x => x.ToModel()).ToList()
            };
        }

        public static ClassModel ToModel(this Class instance)
        {
            return new ClassModel
            {
                Teachers = instance.Teachers.Select(x => x.ToModel()).ToList(),
                Name = instance.Name,
                EndTime = instance.EndTime,
                StartTime = instance.StartTime,
                ActualStudents = instance.ActualStudents.Select(x => x.ToModel()).ToList(),
                Block = instance.Block.ToModel(),
                PassStatistics = instance.PassStatistics.Select(x => x.ToModel()).ToList(),
                RegisteredStudents = instance.RegisteredStudents.Select(x => x.ToModel()).ToList()
            };
        }

        public static PassStatisticModel ToModel(this PassStatistic instance)
        {
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
            return new Registration
            {
                Aerials = instance.Aerials,
                Classes = instance.Classes.ToList(),
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
                Events = instance.Events.ToList(),
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
            return new User
            {
                Note = instance.Note,
                Email = instance.Email,
                EnroledBlocks = instance.EnroledBlocks.Select(x => x.ToEntity()).ToList(),
                FirstName = instance.FirstName,
                Passes = instance.Passes.Select(x => x.ToEntity()).ToList(),
                Schedule = instance.Schedule.Select(x => x.ToEntity()).ToList(),
                Surname = instance.Surname
            };
        }

        public static Booking ToEntity(this BookingModel instance)
        {
            return new Booking
            {
                Room = instance.Room,
                Event = instance.Event
            };
        }

        public static Block ToEntity(this BlockModel instance)
        {
            return new Block
            {
                Announcements = instance.Announcements.Select(x => x.ToEntity()).ToList(),
                Teachers = instance.Teachers.Select(x => x.ToEntity()).ToList(),
                EndDate = instance.EndDate,
                StartDate = instance.StartDate,
                Classes = instance.Classes.Select(x => x.ToEntity()).ToList(),
                EnroledStudents = instance.EnroledStudents.Select(x => x.ToEntity()).ToList(),
                Level = instance.Level.ToEntity(),
                Name = instance.Name
            };
        }

        public static Announcement ToEntity(this AnnouncementModel instance)
        {
            return new Announcement
            {
                Id = instance.Id,
                Message = instance.Message,
                NotifyAll = instance.NotifyAll,
                Receivers = instance.Receivers.Select(x => x.ToEntity()).ToList(),
                ShowFrom = instance.ShowFrom,
                ShowUntil = instance.ShowUntil,
                Type = instance.Type
            };
        }

        public static Level ToEntity(this LevelModel instance)
        {
            return new Level
            {
                Teachers = instance.Teachers.Select(x => x.ToEntity()).ToList(),
                Blocks = instance.Blocks.Select(x => x.ToEntity()).ToList(),
                Name = instance.Name,
                ClassMinutes = instance.ClassMinutes,
                ClassesInBlock = instance.ClassesInBlock,
                EndTime = instance.EndTime,
                Room = instance.Room,
                StartTime = instance.StartTime
            };
        }


        public static Teacher ToEntity(this TeacherModel instance)
        {
            return new Teacher
            {
                User = instance.User.ToEntity(),
                Classes = instance.Classes.Select(x => x.ToEntity()).ToList()
            };
        }

        public static Class ToEntity(this ClassModel instance)
        {
            return new Class
            {
                Teachers = instance.Teachers.Select(x => x.ToEntity()).ToList(),
                Name = instance.Name,
                EndTime = instance.EndTime,
                StartTime = instance.StartTime,
                ActualStudents = instance.ActualStudents.Select(x => x.ToEntity()).ToList(),
                Block = instance.Block.ToEntity(),
                PassStatistics = instance.PassStatistics.Select(x => x.ToEntity()).ToList(),
                RegisteredStudents = instance.RegisteredStudents.Select(x => x.ToEntity()).ToList()
            };
        }

        public static PassStatistic ToEntity(this PassStatisticModel instance)
        {
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