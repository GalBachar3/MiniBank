using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using MiniBank.Models;
using NHibernate;

namespace MiniBank.NhibernateTools
{
    public static class FluentNHibernateHelper
    {
        public static ISession OpenSession()

        {
            var cfg = new StoreConfiguration();
            var sessionFactory = Fluently.Configure()
                .Database(MySQLConfiguration.Standard
                    .ConnectionString("Server=127.0.0.1;Port=3306;Database=minibank;Uid=root;Pwd=sqlpass;"))
                .Mappings(m =>
                    m.AutoMappings
                        .Add(AutoMap.AssemblyOf<User>(cfg).Override<User>(map =>
                        {
                            map.HasManyToMany(user => user.Accounts)
                                .Cascade.All()
                                .Table("usertoaccount");
                        })))
                .Mappings(m=>m.FluentMappings.AddFromAssemblyOf<Account>())
                .BuildSessionFactory();

            return sessionFactory.OpenSession();
        }
    }
}
