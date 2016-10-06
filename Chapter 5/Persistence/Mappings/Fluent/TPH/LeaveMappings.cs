using Domain;
using FluentNHibernate.Mapping;

namespace Persistence.Mappings.Fluent.TPH
{
    public class LeaveMappings : SubclassMap<Leave>
    {
        public LeaveMappings()
        {
            DiscriminatorValue("LVE");
            Map(l => l.AvailableEntitlement);
            Map(l => l.RemainingEntitlement);
            Map(l => l.Type);
        }
    }
}