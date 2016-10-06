using Domain.Component;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Persistence.Mappings.ByCode.Component
{
    public class EmployeeMappings : ClassMapping<Employee>
    {
        public EmployeeMappings()
        {
            Id(e => e.Id, mapper => mapper.Generator(Generators.HighLow));
            Property(e => e.EmployeeNumber);
            Property(e => e.Firstname);
            Property(e => e.Lastname);
            Property(e => e.EmailAddress);
            Property(e => e.DateOfBirth);
            Property(e => e.DateOfJoining);
            Property(e => e.IsAdmin);
            Property(e => e.Password);
            Component(e => e.ResidentialAddress,
                mapper =>
                {
                    mapper.Property(a => a.AddressLine1);
                    mapper.Property(a => a.AddressLine2);
                    mapper.Property(a => a.City);
                    mapper.Property(a => a.Postcode);
                    mapper.Property(a => a.Country);
                });
            
            
        }
    }
}
