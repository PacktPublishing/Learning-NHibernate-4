using Domain;
using FluentNHibernate.Mapping;

namespace Persistence.Mappings.Fluent
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

            HasOne(e => e.ResidentialAddress).Cascade.All().PropertyRef(a => a.Employee);
            HasMany(e => e.Benefits).KeyColumn("Employee_Id").Cascade.AllDeleteOrphan();
            HasManyToMany(e => e.Communities)
                .Table("Employee_Community")
                .ParentKeyColumn("Employee_Id")
                .ChildKeyColumn("Community_Id")
                .Cascade.AllDeleteOrphan();
        }
         
    }
}