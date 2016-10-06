using NHibernate.Mapping.ByCode.Conformist;

namespace Chapter10.Views
{
    public class EmployeeMapping : ClassMapping<Employee>
    {
        public EmployeeMapping()
        {
            Table("EmployeeView");
            Id(e => e.Id);
            Property(e => e.Firstname);
            Property(e => e.Lastname);
            Property(e => e.DateOfJoining);
            Property(e => e.AddressLine1);
            Property(e => e.AddressLine2);
            Mutable(false);
        }
    }
}