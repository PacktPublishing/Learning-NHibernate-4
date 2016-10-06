using Domain;
using FluentNHibernate.Mapping;

namespace Persistence.Mappings.Fluent.TPH
{
    public class SkillsEnhancementAllowanceMappings : SubclassMap<SkillsEnhancementAllowance>
    {
        public SkillsEnhancementAllowanceMappings()
        {
            DiscriminatorValue("SEA");
            Map(s => s.Entitlement);
            Map(s => s.RemainingEntitlement);
        }
    }
}