using System;
using FluentNHibernate.Automapping;
using MiniBank.Models;

namespace MiniBank.NhibernateTools
{
    public class StoreConfiguration : DefaultAutomappingConfiguration
    {
        public override bool ShouldMap(Type type)
        {
            return type.FullName == typeof(User).FullName;
        }
    }
}
