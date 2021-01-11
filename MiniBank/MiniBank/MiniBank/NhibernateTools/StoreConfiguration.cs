using System;
using FluentNHibernate.Automapping;

namespace MiniBank.NhibernateTools
{
    public class StoreConfiguration : DefaultAutomappingConfiguration
    {
        public override bool ShouldMap(Type type)
        {
            return type.FullName == "MiniBank.Models.User";
        }
    }
}
