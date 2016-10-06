using Domain;
using NHibernate.Mapping.ByCode.Conformist;

namespace Persistence.Mappings.ByCode.TPH
{
    public class SkillsEnhancementAllowanceMappings : SubclassMapping<SkillsEnhancementAllowance>
    {
        public SkillsEnhancementAllowanceMappings()
        {
            DiscriminatorValue("SEA");
            Property(s => s.Entitlement);
            Property(s => s.RemainingEntitlement);
        }
    }
}