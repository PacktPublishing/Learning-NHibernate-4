using Domain;
using NHibernate.Mapping.ByCode.Conformist;

namespace Persistence.Mappings
{
    public class SkillsEnhancementAllowanceMappings : JoinedSubclassMapping<SkillsEnhancementAllowance>
    {
        public SkillsEnhancementAllowanceMappings()
        {
            Key(k => k.Column("Id"));
            Property(s => s.Entitlement);
            Property(s => s.RemainingEntitlement);
        }
    }
}