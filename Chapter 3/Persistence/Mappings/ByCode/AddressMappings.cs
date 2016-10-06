using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using Domain;

namespace Persistence.Mappings.ByCode
{
    public class AddressMappings : ClassMapping<Address>
    {
        public AddressMappings()
        {
            Id(a => a.Id, mapper => mapper.Generator(Generators.HighLow));
            Property(a => a.AddressLine1);
            Property(a => a.AddressLine2);
            Property(a => a.Postcode);
            Property(a => a.City);
            Property(a => a.Country);
            ManyToOne(a => a.Employee, mapper =>
            {
                mapper.Class(typeof (Employee));
                mapper.Column("Employee_Id");
                mapper.Unique(true);
            });
        }
    }
}
