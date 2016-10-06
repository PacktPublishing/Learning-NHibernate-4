using Chapter10.CompositeKeyWithoutComponent.Entities;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Chapter10.CompositeKeyWithoutComponent.Mapping
{
    public class EmployeeMapping : ClassMapping<Employee>
    {
        public EmployeeMapping()
        {
            ComposedId(cid =>
            {
                cid.Property(e => e.Firstname);
                cid.Property(e => e.Lastname);
            });
            Property(e => e.DateOfJoining);
        }
    }
}