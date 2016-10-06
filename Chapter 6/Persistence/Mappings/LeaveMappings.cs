using Domain;
using NHibernate.Mapping.ByCode.Conformist;

namespace Persistence.Mappings
{
    public class LeaveMappings : JoinedSubclassMapping<Leave>
    {
        public LeaveMappings()
        {
            Key(k => k.Column("Id"));
            Property(l => l.Type);
            Property(l => l.AvailableEntitlement);
            Property(l => l.RemainingEntitlement);
        }
    }
}