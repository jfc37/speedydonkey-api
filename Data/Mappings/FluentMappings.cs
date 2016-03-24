using FluentNHibernate.Mapping;
using Models;
using Models.OnlinePayments;
using Models.PrivateLessons;
using Models.Settings;

namespace Data.Mappings
{
    public static class DatabaseEntityMapExtension
    {
        public static void MapDatabaseEntity<T>(this ClassMap<T> instance)
            where T : DatabaseEntity
        {
            instance.Id(x => x.Id);
            instance.Map(x => x.CreatedDateTime);
            instance.Map(x => x.LastUpdatedDateTime);
            instance.Map(x => x.IsDeleted);
        }
    }

    public class OnlinePaymentMap : ClassMap<OnlinePayment>
    {
        public OnlinePaymentMap()
        {
            this.MapDatabaseEntity();
            Map(x => x.ItemType);
            Map(x => x.Description);
            Map(x => x.Fee);
            Map(x => x.ItemId);
            Map(x => x.PaymentMethod);
            Map(x => x.Price);
            Map(x => x.PaymentStatus);
            Map(x => x.ReferenceNumber);
            Map(x => x.InitiatedBy);
        }
    }
    public class OpeningHoursMap : ClassMap<TimeSlot>
    {
        public OpeningHoursMap()
        {
            this.MapDatabaseEntity();
            Map(x => x.Day);
            Map(x => x.OpeningTime);
            Map(x => x.ClosingTime);
        }
    }
    public class SettingItemMap : ClassMap<SettingItem>
    {
        public SettingItemMap()
        {
            this.MapDatabaseEntity();
            Map(x => x.Name);
            Map(x => x.Value);
        }
    }
    public class TeacherAvailabilityMap : ClassMap<TeacherAvailability>
    {
        public TeacherAvailabilityMap()
        {
            this.MapDatabaseEntity();
            References(x => x.Teacher);
            HasMany(x => x.Availabilities)
                .Cascade.All();
        }
    }

    public class PayPalPaymentMap : SubclassMap<PayPalPayment>
    {
        public PayPalPaymentMap()
        {
            Map(x => x.ReturnUrl);
            Map(x => x.CancelUrl);
            Map(x => x.Token);
            Map(x => x.PayerId);
        }
    }

    public class PoliPaymentMap : SubclassMap<PoliPayment>
    {
        public PoliPaymentMap()
        {
            Map(x => x.CancellationUrl);
            Map(x => x.FailureUrl);
            Map(x => x.SuccessUrl);
            Map(x => x.PoliId);
            Map(x => x.Token);
        }
    }

    public class RegistrationMap : ClassMap<Registration>
    {
        public RegistrationMap()
        {
            this.MapDatabaseEntity();
            Map(x => x.PaymentStatus);
            Map(x => x.RegistationId);
            Map(x => x.Amount);
            Map(x => x.BalboaLevel);
            Map(x => x.BluesLevel);
            Map(x => x.City);
            Map(x => x.CountryOfResidence);
            Map(x => x.Email);
            Map(x => x.EmergencyContactPerson);
            Map(x => x.EmergencyContactNumber);
            Map(x => x.FirstName);
            Map(x => x.FullPass);
            Map(x => x.LindyLevel);
            Map(x => x.Over18);
            Map(x => x.PhoneNumber);
            Map(x => x.DanceRole);
            Map(x => x.Surname);

            Map(x => x.Balboa);
            Map(x => x.BalboaPartner);
            Map(x => x.Hellzapoppin);
            Map(x => x.HellzapoppinPartner);
            Map(x => x.JackAndJill);
            Map(x => x.Novice);
            Map(x => x.NovicePartner);
            Map(x => x.ShowcaseCouple);
            Map(x => x.ShowcaseCouplePartner);
            Map(x => x.ShowcaseTeam);
            Map(x => x.ShowcaseTeamPartner);
            Map(x => x.SoloJazz);
            Map(x => x.StrictlyLindy);
            Map(x => x.StrictlyLindyPartner);

            Map(x => x.PerformAtGrammy);
            Map(x => x.Aerials);
            Map(x => x.AerialsPartner);
            Map(x => x.AerialsTeachers);
            Map(x => x.TermsAndConditions);

            HasMany(x => x.Classes)
                .KeyColumn("ClassId")
                .Table("RegistrationClass")
                .Element("ClassKey")
                .AsSet();
            HasMany(x => x.Events)
                .KeyColumn("EventId")
                .Table("RegistrationEvent")
                .Element("EventKey")
                .AsSet();
        }
    }

    public class AnnouncementMap : ClassMap<Announcement>
    {
        public AnnouncementMap()
        {
            this.MapDatabaseEntity();
            Map(x => x.Message);
            Map(x => x.NotifyAll);
            HasManyToMany(x => x.Receivers)
                .Table("BlockToAnnouncement")
                .AsSet();
        }
    }

    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            this.MapDatabaseEntity();
            Map(x => x.GlobalId);
            Map(x => x.FirstName);
            Map(x => x.Surname);
            Map(x => x.FullName)
                .Access.ReadOnly();
            Map(x => x.Email);
            Map(x => x.Password);
            Map(x => x.Claims);
            Map(x => x.Note);
            Map(x => x.DoNotEmail);
            Map(x => x.AgreesToTerms);
            HasManyToMany(x => x.EnroledBlocks)
                .Table("UsersEnroledBlocks")
                .AsSet();
            HasMany(x => x.Passes)
                .Cascade.SaveUpdate();
            HasMany(x => x.Schedule);
        }
    }

    public class TeacherMap : ClassMap<Teacher>
    {
        public TeacherMap()
        {
            this.MapDatabaseEntity();
            HasManyToMany(x => x.Classes)
                .AsSet();
            References(x => x.User);
        }
    }
    public class ClassMap : SubclassMap<Class>
    {
        public ClassMap()
        {
            References(x => x.Block)
                .Class(typeof(Block));
            HasManyToMany(x => x.PassStatistics)
                .Table("ClassPassStatistic")
                .AsSet();
        }
    }
    public class StandAloneEventMap : SubclassMap<StandAloneEvent>
    {
        public StandAloneEventMap()
        {
            Map(x => x.IsPrivate);
            Map(x => x.Price);
        }
    }

    public class EventMap : ClassMap<Event>
    {
        public EventMap()
        {
            this.MapDatabaseEntity();
            Map(x => x.StartTime);
            Map(x => x.EndTime);
            Map(x => x.Name);
            References(x => x.Room);
            HasManyToMany(x => x.RegisteredStudents)
                .Table("EventRoll")
                .AsSet();
            HasManyToMany(x => x.Teachers)
                .Table("EventTeacher")
                .AsSet();
            HasManyToMany(x => x.ActualStudents)
                .Table("ClassAttendance")
                .AsSet();
        }
    }

    public class BlockMap : ClassMap<Block>
    {
        public BlockMap()
        {
            this.MapDatabaseEntity();
            Map(x => x.StartDate);
            Map(x => x.EndDate);
            Map(x => x.NumberOfClasses);
            Map(x => x.MinutesPerClass);
            Map(x => x.Name);
            Map(x => x.IsInviteOnly);
            References(x => x.Room);
            HasMany(x => x.Classes);
            HasManyToMany(x => x.EnroledStudents)
                .Inverse()
                .Table("UsersEnroledBlocks")
                .Cascade.SaveUpdate()
                .AsSet();
            HasManyToMany(x => x.Teachers)
                .Table("BlockTeacher")
                .AsSet();
            HasManyToMany(x => x.Announcements)
                .Table("BlockToAnnouncement")
                .AsSet();
        }
    }

    public class PassStatisticMap : ClassMap<PassStatistic>
    {
        public PassStatisticMap()
        {
            this.MapDatabaseEntity();
            Map(x => x.CostPerClass);
            Map(x => x.NumberOfClassesAttended);
            References<Pass>(x => x.Pass)
                .Column("Pass_id");
        }
    }

    public class RoomMap : ClassMap<Room>
    {
        public RoomMap()
        {
            this.MapDatabaseEntity();
            Map(x => x.Name);
            Map(x => x.Location);
            HasMany(x => x.Events);
        }
    }

    public class PassMap : ClassMap<Pass>
    {
        public PassMap()
        {
            this.MapDatabaseEntity();
            Map(x => x.StartDate);
            Map(x => x.EndDate);
            Map(x => x.PassType);
            Map(x => x.PaymentStatus);
            Map(x => x.Cost);
            Map(x => x.Description);
            Map(x => x.Note);
            References(x => x.Owner)
                .Column("User_id")
                .Class(typeof(User));
            References(x => x.PassStatistic)
                .Class(typeof(PassStatistic))
                .Cascade.All();
        }
    }

    public class ClipPassMap : SubclassMap<ClipPass>
    {
        public ClipPassMap()
        {
            Map(x => x.ClipsRemaining);
        }
    }

    public class ReferenceDataMap : ClassMap<ReferenceData>
    {
        public ReferenceDataMap()
        {
            this.MapDatabaseEntity();
            Map(x => x.Type);
            Map(x => x.Name);
            Map(x => x.Description);
            Map(x => x.Value);
        }
    }

    public class PassTemplateMap : ClassMap<PassTemplate>
    {
        public PassTemplateMap()
        {
            this.MapDatabaseEntity();
            Map(x => x.ClassesValidFor);
            Map(x => x.Cost);
            Map(x => x.Description);
            Map(x => x.PassType);
            Map(x => x.WeeksValidFor);
            Map(x => x.AvailableForPurchase);
        }
    }
}
