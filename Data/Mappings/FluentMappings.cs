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
}
