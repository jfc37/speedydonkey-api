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
                .Class(typeof(User));
        }
    }

    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Id(x => x.Id);
            Map(x => x.FirstName);
            Map(x => x.Surname);
            References(x => x.Account)
                .Class(typeof(Account));
        }
    }
}
