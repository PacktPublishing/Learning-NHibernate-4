using Domain;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Persistence.Mappings.ByCode
{
    public class EmployeeMappings : ClassMapping<Employee>
    {
        public EmployeeMappings()
        {
            Id(e => e.Id, mapper =>mapper.Generator(Generators.HighLow));
            Property(e => e.EmployeeNumber);
            Property(e => e.Firstname);
            Property(e => e.Lastname);
            Property(e => e.EmailAddress);
            Property(e => e.DateOfBirth);
            Property(e => e.DateOfJoining);
            Property(e => e.IsAdmin);
            Property(e => e.Password);

            OneToOne(e => e.ResidentialAddress, mapper =>
            {
                mapper.PropertyReference(a => a.Employee);
                mapper.Cascade(Cascade.All);
            });

            Set(e => e.Benefits,
                mapper => { 
                    mapper.Key(k => k.Column("Employee_Id")); 
                    mapper.Cascade(Cascade.All);
                },
                relation => relation.OneToMany(mapping => mapping.Class(typeof(Benefit))));

            Set(e => e.Communities,
                mapper =>
                {
                    mapper.Table("Employee_Community");
                    mapper.Cascade(Cascade.All);
                    mapper.Key(k => k.Column("Employee_Id"));
                },
                relation => relation.ManyToMany(mtm =>
                {
                    mtm.Class(typeof(Community));
                    mtm.Column("Community_Id");
                }));
            
        }
    }
}
