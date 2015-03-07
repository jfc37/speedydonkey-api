using FluentNHibernate.Mapping;
using Models;

namespace Data.Mappings
{
    public class AccountMap : ClassMap<Account>
    {
        public AccountMap()
        {
            Id(x => x.Id);
            Map(x => x.Email);
            Map(x => x.Password);
            References(x => x.User)
                .Cascade.SaveUpdate()
                //.Constrained()
                .LazyLoad(Laziness.Proxy) // or .NoProxy, .False
                //.PropertyRef(x => x.Account)
                .Access.Property()
                .Class<User>()
                .Fetch.Join() // or .Select(), .Subselect()
                //.ForeignKey()
                ;
        }
    }

    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Id(x => x.Id);
            Map(x => x.FirstName);
            Map(x => x.Surname);
            HasOne(x => x.Account)
                .Cascade.SaveUpdate()
                .Constrained()
                .LazyLoad(Laziness.Proxy) // or .NoProxy, .False
                //.PropertyRef(x => x.User)
                .Access.Property()
                .Class<Account>()
                .Fetch.Join() // or .Select(), .Subselect()
                .ForeignKey()
                ;
        }
    }
}
