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
            HasMany<Block>(x => x.Blocks)
                .Not.LazyLoad();
        }
    }
    public class BlockMap : ClassMap<Block>
    {
        public BlockMap()
        {
            Id(x => x.Id);
            Map(x => x.StartDate);
            Map(x => x.EndDate);
            References(x => x.Level)
                .Class(typeof(Level))
                .Not.LazyLoad();
            HasManyToMany<User>(x => x.EnroledStudents)
                .Cascade.All()
                .Inverse()
                .Table("UsersEnroledBlocks")
                .Not.LazyLoad();
        }
    }
}
