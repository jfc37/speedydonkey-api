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
        }
    }
}
