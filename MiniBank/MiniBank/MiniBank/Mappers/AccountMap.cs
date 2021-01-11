using FluentNHibernate.Mapping;
using MiniBank.Models;

namespace MiniBank.Mappers
{
    public class AccountMap : ClassMap<Account>
    {
        public AccountMap()
        {
            Table("account");
            Id(account => account.Id).Not.Nullable();
            DiscriminateSubClassesOnColumn("ClassType").Not.Nullable();
            Map(account => account.Balance);
            HasManyToMany(account => account.Users)
                .Cascade.All().Inverse()
                .Table("usertoaccount");
        }
    }
}
