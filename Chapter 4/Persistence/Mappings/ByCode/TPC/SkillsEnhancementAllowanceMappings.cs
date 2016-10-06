using Domain;
using NHibernate.Mapping.ByCode.Conformist;

namespace Persistence.Mappings.ByCode.TPC
{
    public class SkillsEnhancementAllowanceMappings : UnionSubclassMapping<SkillsEnhancementAllowance>
    {
        public SkillsEnhancementAllowanceMappings()
        {
            Property(s => s.Entitlement);
            Property(s => s.RemainingEntitlement);
        }
    }
}