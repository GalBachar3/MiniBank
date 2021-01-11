using FluentNHibernate.Mapping;
using MiniBank.Models;

namespace MiniBank.Mappers
{
    public class VipAccountMap : SubclassMap<VipAccount>
    {
        public VipAccountMap()
        {
            DiscriminatorValue("VipAccount");
        }
    }
}
