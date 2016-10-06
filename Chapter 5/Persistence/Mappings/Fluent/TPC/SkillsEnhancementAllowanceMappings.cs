using Domain;
using FluentNHibernate.Mapping;

namespace Persistence.Mappings.Fluent.TPC
{
    public class SkillsEnhancementAllowanceMappings : SubclassMap<SkillsEnhancementAllowance>
    {
        public SkillsEnhancementAllowanceMappings()
        {
            Map(s => s.Entitlement);
            Map(s => s.RemainingEntitlement);
        }
    }
}