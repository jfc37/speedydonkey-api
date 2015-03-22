using FluentNHibernate.Mapping;
using Models;

namespace Data.Mappings
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Id(x => x.Id);
            Map(x => x.FirstName);
            Map(x => x.Surname);
            Map(x => x.Email);
            Map(x => x.Password);
            HasManyToMany<Block>(x => x.EnroledBlocks)
                .Cascade.All()
                .Table("UsersEnroledBlocks")
                .AsSet();
            HasMany<Pass>(x => x.Passes)
                .Cascade.All();
            HasMany<Booking>(x => x.Schedule);
        }
    }
    public class ClassMap : ClassMap<Class>
    {
        public ClassMap()
        {
            Id(x => x.Id);
            Map(x => x.StartTime);
            Map(x => x.EndTime);
            Map(x => x.Name);
            References(x => x.Block)
                .Class(typeof (Block));
            HasManyToMany<User>(x => x.RegisteredStudents)
                .Cascade.All()
                .Table("ClassRoll")
                .AsSet();
                ;
        }
    }

    public class BookingMap : ClassMap<Booking>
    {
        public BookingMap()
        {
            Id(x => x.Id);
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
            Map(x => x.ClassesInBlock);
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
            Map(x => x.StartDate);
            Map(x => x.EndDate);
            Map(x => x.PassType);
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
            Map(x => x.Type);
            Map(x => x.Name);
            Map(x => x.Description);
            Map(x => x.Value);
        }
    }
}
