using Domain;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Persistence.Mappings.ByCode.TPH
{
    public class BenefitMappings : ClassMapping<Benefit>
    {
        public BenefitMappings()
        {
            Id(b => b.Id, idmapper => idmapper.Generator(Generators.HighLow));
            Property(b => b.Name);
            Property(b => b.Description);
            ManyToOne(b => b.Employee, mapping =>
            {
                mapping.Class(typeof (Employee));
                mapping.Column("Employee_Id");
            });
        }
    }
}
