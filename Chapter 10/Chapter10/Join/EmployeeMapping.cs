using NHibernate.Mapping.ByCode.Conformist;

namespace Chapter10.Join
{
    public class EmployeeMapping : ClassMapping<Employee>
    {
        public EmployeeMapping()
        {
            Id(e => e.Id);
            Join("tableName", joinMapper =>
            {
                joinMapper.Table("Address");
                joinMapper.Key(keyMapper => keyMapper.Column("Id"));
                joinMapper.Property(e => e.AddressLine1);
                joinMapper.Property(e => e.AddressLine2);
            });
        }
    }
}