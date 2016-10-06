using System.Collections.Generic;

namespace Domain
{
    public class Community : EntityBase<Community>
    {
        public Community()
        {
            Members = new HashSet<Employee>();
        }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual ICollection<Employee> Members { get; set; }
    }
}