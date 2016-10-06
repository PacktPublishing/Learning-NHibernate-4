using System;
using System.Collections.Generic;
using Chapter10.CompositeKey.Entities;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using Employee = Chapter10.StoredProcedure.Entities.Employee;

namespace Chapter10.CompositeKey.Mapping
{
    public class BenefitMapping : ClassMapping<Benefit>
    {
        public BenefitMapping()
        {
            Id(b => b.Id, mapper =>
            {
                mapper.Generator(Generators.HighLow, hilomapper =>
                {
                    hilomapper.Params(new Dictionary<string, object>
                    {
                        { "max_lo", 10},
                        { "next_hi", 200}
                    });
                });
            });

            ManyToOne(b => b.Employee, mapper =>
            {
                mapper.Columns(colMapper => colMapper.Name("Firstname"), colMapper => colMapper.Name("Lastname"));
            });
        }
    }
}