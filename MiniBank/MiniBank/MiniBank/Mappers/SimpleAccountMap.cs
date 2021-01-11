using FluentNHibernate.Mapping;
using MiniBank.Models;

namespace MiniBank.Mappers
{
    public class SimpleAccountMap : SubclassMap<SimpleAccount>
    {
        public SimpleAccountMap()
        {
            DiscriminatorValue("SimpleAccount");
        }
    }
}
