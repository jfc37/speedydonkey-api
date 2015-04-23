using FluentNHibernate.Mapping;
using Models;

namespace Data.Mappings
{

    public class ActivityLogMap : ClassMap<ActivityLog>
    {
        public ActivityLogMap()
        {
            Id(x => x.Id);
            Map(x => x.PerformingUserId);
            Map(x => x.Session);
            Map(x => x.DateTimeStamp);
            Map(x => x.ActivityText);
            Map(x => x.ActivityType);
            Map(x => x.ActivityGroup);
        }
    }

    public class TeachingConcernsMap : ClassMap<TeachingConcerns>
    {
        public TeachingConcernsMap()
        {
            Id(x => x.Id);
            Map(x => x.Deleted);
        }
    }

    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Id(x => x.Id);
            Map(x => x.Deleted);
            Map(x => x.FirstName);
            Map(x => x.Surname);
            Map(x => x.Email);
            Map(x => x.Password);
            Map(x => x.Claims);
            Map(x => x.Status);
            Map(x => x.ActivationKey);
            HasManyToMany<Block>(x => x.EnroledBlocks)
                .Cascade.All()
                .Table("UsersEnroledBlocks")
                .AsSet();
            HasMany<Pass>(x => x.Passes)
                .Cascade.All();
            HasMany<Booking>(x => x.Schedule);
            References(x => x.TeachingConcerns)
                .Class(typeof(TeachingConcerns))
                .Cascade.All()
                .Not.LazyLoad();
        }
    }
    public class ClassMap : ClassMap<Class>
    {
        public ClassMap()
        {
            Id(x => x.Id);
            Map(x => x.Deleted);
            Map(x => x.StartTime);
            Map(x => x.EndTime);
            Map(x => x.Name);
            References(x => x.Block)
                .Class(typeof (Block));
            HasManyToMany<User>(x => x.RegisteredStudents)
                .Cascade.All()
                .Table("ClassRoll")
                .AsSet();
            HasManyToMany<User>(x => x.ActualStudents)
                .Cascade.All()
                .Table("ClassAttendance")
                .AsSet();
        }
    }

    public class BookingMap : ClassMap<Booking>
    {
        public BookingMap()
        {
            Id(x => x.Id);
            Map(x => x.Deleted);
            References(x => x.Event)
                .Class(typeof (Class))
                .Cascade.All()
                .Not.LazyLoad();
        }
    }

    public class LevelMap : ClassMap<Level>
    {
        public LevelMap()
        {
            Id(x => x.Id);
            Map(x => x.Deleted);
            Map(x => x.ClassesInBlock);
            Map(x => x.ClassMinutes);
            Map(x => x.EndTime);
            Map(x => x.Name);
            Map(x => x.StartTime);
            HasMany<Block>(x => x.Blocks);
        }
    }

    public class BlockMap : ClassMap<Block>
    {
        public BlockMap()
        {
            Id(x => x.Id);
            Map(x => x.Deleted);
            Map(x => x.StartDate);
            Map(x => x.EndDate);
            Map(x => x.Name);
            References(x => x.Level)
                .Class(typeof (Level));
            HasMany<Class>(x => x.Classes)
                .Cascade.All();
            HasManyToMany<User>(x => x.EnroledStudents)
                .Cascade.All()
                .Inverse()
                .Table("UsersEnroledBlocks")
                .AsSet();
        }
    }

    public class PassMap : ClassMap<Pass>
    {
        public PassMap()
        {
            Id(x => x.Id);
            Map(x => x.Deleted);
            Map(x => x.StartDate);
            Map(x => x.EndDate);
            Map(x => x.PassType);
            Map(x => x.PaymentStatus);
            Map(x => x.Cost);
            References(x => x.Owner)
                .Column("User_id")
                .Class(typeof (User));
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
            Map(x => x.Deleted);
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
            Map(x => x.Deleted);
            Map(x => x.ClassesValidFor);
            Map(x => x.Cost);
            Map(x => x.Description);
            Map(x => x.PassType);
            Map(x => x.WeeksValidFor);
            Map(x => x.AvailableForPurchase);
        }
    }
}
