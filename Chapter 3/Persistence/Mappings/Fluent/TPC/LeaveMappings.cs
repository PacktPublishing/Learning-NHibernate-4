using Domain;
using FluentNHibernate.Mapping;

namespace Persistence.Mappings.Fluent.TPC
{
    public class LeaveMappings : SubclassMap<Leave>
    {
        public LeaveMappings()
        {
            Map(l => l.AvailableEntitlement);
            Map(l => l.RemainingEntitlement);
            Map(l => l.Type);
        }
    }
}