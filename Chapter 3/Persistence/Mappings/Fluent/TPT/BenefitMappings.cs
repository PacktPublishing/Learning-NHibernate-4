using Domain;
using FluentNHibernate.Mapping;

namespace Persistence.Mappings.Fluent.TPT
{
    public class BenefitMappings : ClassMap<Benefit>
    {
        public BenefitMappings()
        {
            Id(b => b.Id).GeneratedBy.HiLo("1000");
            Map(b => b.Name);
            Map(b => b.Description);
            References(b => b.Employee);
        }         
    }
}