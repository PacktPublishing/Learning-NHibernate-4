using Domain;
using NHibernate.Mapping.ByCode.Conformist;

namespace Persistence.Mappings.ByCode.TPH
{
    public class LeaveMappings : SubclassMapping<Leave>
    {
        public LeaveMappings()
        {
            DiscriminatorValue("LVE");
            Property(l => l.Type);
            Property(l => l.AvailableEntitlement);
            Property(l => l.RemainingEntitlement);
        }
    }
}