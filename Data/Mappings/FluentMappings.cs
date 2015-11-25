using FluentNHibernate.Mapping;
using Models;
using Models.OnlinePayments;

namespace Data.Mappings
{
    public class OnlinePaymentMap : ClassMap<OnlinePayment>
    {
        public OnlinePaymentMap()
        {
            Id(x => x.Id);
            Map(x => x.ItemType);
            Map(x => x.CreatedDateTime);
            Map(x => x.LastUpdatedDateTime);
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
            Id(x => x.Id);
            Map(x => x.CreatedDateTime);
            Map(x => x.LastUpdatedDateTime);
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
            Id(x => x.Id);
            Map(x => x.CreatedDateTime);
            Map(x => x.LastUpdatedDateTime);
            Map(x => x.Message);
            Map(x => x.NotifyAll);
            HasManyToMany<Block>(x => x.Receivers)
                .Table("BlockToAnnouncement")
                .AsSet();
        }
    }

    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Id(x => x.Id);
            Map(x => x.GlobalId);
            Map(x => x.CreatedDateTime);
            Map(x => x.LastUpdatedDateTime);
            Map(x => x.FirstName);
            Map(x => x.Surname);
            Map(x => x.Email);
            Map(x => x.Password);
            Map(x => x.Claims);
            Map(x => x.Status);
            Map(x => x.ActivationKey);
            Map(x => x.Note);
            HasManyToMany<Block>(x => x.EnroledBlocks)
                .Table("UsersEnroledBlocks")
                .AsSet();
            HasMany<Pass>(x => x.Passes)
                .Cascade.SaveUpdate();
            HasMany(x => x.Schedule);
        }
    }

    public class TeacherMap : ClassMap<Teacher>
    {
        public TeacherMap()
        {
            Id(x => x.Id);
            Map(x => x.CreatedDateTime);
            Map(x => x.LastUpdatedDateTime);
            HasManyToMany<Class>(x => x.Classes)
                .AsSet();
            References<User>(x => x.User);
        }
    }
    public class ClassMap : SubclassMap<Class>
    {
        public ClassMap()
        {
            References(x => x.Block)
                .Class(typeof(Block));
            HasManyToMany<User>(x => x.ActualStudents)
                .Table("ClassAttendance")
                .AsSet();
            HasManyToMany<PassStatistic>(x => x.PassStatistics)
                .Table("ClassPassStatistic")
                .AsSet();
        }
    }

    public class EventMap : ClassMap<Event>
    {
        public EventMap()
        {
            Id(x => x.Id);
            Map(x => x.CreatedDateTime);
            Map(x => x.LastUpdatedDateTime);
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
        }
    }

    public class BlockMap : ClassMap<Block>
    {
        public BlockMap()
        {
            Id(x => x.Id);
            Map(x => x.CreatedDateTime);
            Map(x => x.LastUpdatedDateTime);
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
            Id(x => x.Id);
            Map(x => x.CreatedDateTime);
            Map(x => x.LastUpdatedDateTime);
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
            Id(x => x.Id);
            Map(x => x.CreatedDateTime);
            Map(x => x.LastUpdatedDateTime);
            Map(x => x.Name);
            Map(x => x.Location);
            HasMany(x => x.Events);
        }
    }

    public class PassMap : ClassMap<Pass>
    {
        public PassMap()
        {
            Id(x => x.Id);
            Map(x => x.CreatedDateTime);
            Map(x => x.LastUpdatedDateTime);
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
            Id(x => x.Id);
            Map(x => x.CreatedDateTime);
            Map(x => x.LastUpdatedDateTime);
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
            Id(x => x.Id);
            Map(x => x.CreatedDateTime);
            Map(x => x.LastUpdatedDateTime);
            Map(x => x.ClassesValidFor);
            Map(x => x.Cost);
            Map(x => x.Description);
            Map(x => x.PassType);
            Map(x => x.WeeksValidFor);
            Map(x => x.AvailableForPurchase);
        }
    }
}
