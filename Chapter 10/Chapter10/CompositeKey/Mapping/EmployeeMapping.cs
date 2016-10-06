using System;
using Chapter10.CompositeKey.Entities;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Chapter10.CompositeKey.Mapping
{
    public class EmployeeMapping : ClassMapping<Employee>
    {
        public EmployeeMapping()
        {
            ComponentAsId(e => e.Id, idMapper =>
            {
                idMapper.Property(e => e.Firstname);
                idMapper.Property(e => e.Lastname);
            });
            Property(e => e.DateOfJoining);

            Set(e => e.Benefits,
                mapper =>
            {
                mapper.Key(k =>
                {
                    k.Columns(colMapper => colMapper.Name("Firstname"), 
                              colMapper => colMapper.Name("Lastname"));
                });
                mapper.Cascade(Cascade.All.Include(Cascade.DeleteOrphans));
                mapper.Inverse(true);
                mapper.Lazy(CollectionLazy.Extra);
            },
            relation => relation.OneToMany(mapping => mapping.Class(typeof(Benefit))));
        }
    }
}