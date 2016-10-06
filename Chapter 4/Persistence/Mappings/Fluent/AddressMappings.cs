using Domain;
using FluentNHibernate.Mapping;

namespace Persistence.Mappings.Fluent
{
    public class AddressMappings : ClassMap<Address>
    {
        public AddressMappings()
        {
            Id(a => a.Id).GeneratedBy.HiLo("1000");
            Map(a => a.AddressLine1);
            Map(a => a.AddressLine2);
            Map(a => a.City);
            Map(a => a.Postcode);
            Map(a => a.Country);
            References(a => a.Employee).Column("Employee_Id");
        }
    }
}