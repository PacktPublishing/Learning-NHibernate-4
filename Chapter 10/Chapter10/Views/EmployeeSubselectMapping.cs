using NHibernate.Mapping.ByCode.Conformist;

namespace Chapter10.Views
{
    public class EmployeeSubselectMapping : ClassMapping<Employee>
    {
        public EmployeeSubselectMapping()
        {
            Subselect(@"SELECT dbo.Employee.Id, 
                                dbo.Employee.Firstname, 
                                dbo.Employee.Lastname, 
                                dbo.Employee.DateOfJoining, 
                                dbo.Address.AddressLine1, 
                                dbo.Address.AddressLine2
                            FROM dbo.Address INNER JOIN
                                 dbo.Employee ON dbo.Address.Employee_Id = dbo.Employee.Id
                            ");
            Mutable(false);
            Synchronize("Employee", "Address");
            //Id(e => e.Id, x => x.Column("Id"));
            //Property(e => e.Firstname, x => x.Column("Firstname"));
        }
    }
}