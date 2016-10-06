using Domain.Component;
using FluentNHibernate.Mapping;

namespace Persistence.Mappings.Fluent.Component
{
    public class EmployeeMappings : ClassMap<Employee>
    {
        public EmployeeMappings()
        {
            Id(e => e.Id).GeneratedBy.HiLo("1000");
            Map(e => e.EmployeeNumber);
            Map(e => e.Firstname);
            Map(e => e.Lastname);
            Map(e => e.EmailAddress);
            Map(e => e.DateOfBirth);
            Map(e => e.DateOfJoining);
            Map(e => e.IsAdmin);
            Map(e => e.Password);
            Component(e => e.ResidentialAddress,
                mapper =>
                {
                    mapper.Map(a => a.AddressLine1);
                    mapper.Map(a => a.AddressLine2);
                    mapper.Map(a => a.City);
                    mapper.Map(a => a.Postcode);
                    mapper.Map(a => a.Country);
                });
        }
         
    }
}