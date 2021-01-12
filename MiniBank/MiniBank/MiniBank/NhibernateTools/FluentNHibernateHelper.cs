using System.Configuration;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using MiniBank.Models;
using NHibernate;

namespace MiniBank.NhibernateTools
{
    public static class FluentNHibernateHelper
    {
        public static ISession Session { get; set; }
        
        public static void OpenSession()
        {
            var cfg = new StoreConfiguration();
            var sessionFactory = Fluently.Configure()
                .Database(MySQLConfiguration.Standard
                    .ConnectionString(ConfigurationSettings.AppSettings.Get("ConnectionString")))
                .Mappings(m => m.AutoMappings
                    .Add(AutoMap.AssemblyOf<User>(cfg)
                    .Override<User>(map =>
                    {
                        map.HasManyToMany(user =>
                            user.Accounts).Cascade.All().Table("usertoaccount");
                    })))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Account>())
                .BuildSessionFactory();

            Session = sessionFactory.OpenSession();
        }
    }
}
