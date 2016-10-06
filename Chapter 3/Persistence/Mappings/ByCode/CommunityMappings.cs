using Domain;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Persistence.Mappings.ByCode
{
    public class CommunityMappings : ClassMapping<Community>
    {
        public CommunityMappings()
        {
            Id(c => c.Id, idmapper => idmapper.Generator(Generators.HighLow));
            Property(c => c.Name);
            Property(c => c.Description);
            Set(c => c.Members,
                mapper =>
                {
                    mapper.Key(k => k.Column("Community_Id"));
                    mapper.Table("Employee_Community");
                    mapper.Cascade(Cascade.All);
                    mapper.Inverse(true);
                },
                relation => relation.ManyToMany(mapper =>
                {
                    mapper.Class(typeof (Employee));
                    mapper.Column("Employee_Id");
                }));
        }
    }
}