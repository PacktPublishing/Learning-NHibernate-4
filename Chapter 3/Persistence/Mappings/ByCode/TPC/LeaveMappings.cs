using Domain;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Persistence.Mappings.ByCode.TPC
{
    public class LeaveMappings : UnionSubclassMapping<Leave>
    {
        public LeaveMappings()
        {
            Property(l => l.Type);
            Property(l => l.AvailableEntitlement);
            Property(l => l.RemainingEntitlement);
        }
    }
}