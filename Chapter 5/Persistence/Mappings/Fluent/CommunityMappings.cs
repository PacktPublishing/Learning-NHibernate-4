using Domain;
using FluentNHibernate.Mapping;

namespace Persistence.Mappings.Fluent
{
    public class CommunityMappings : ClassMap<Community>
    {
        public CommunityMappings()
        {
            Id(c => c.Id).GeneratedBy.HiLo("1000");
            Map(c => c.Name);
            Map(c => c.Description);
            HasManyToMany(c => c.Members)
                .Table("Employee_Community")
                .ParentKeyColumn("Community_Id")
                .ChildKeyColumn("Employee_Id")
                .Cascade.AllDeleteOrphan();
        }
    }
}