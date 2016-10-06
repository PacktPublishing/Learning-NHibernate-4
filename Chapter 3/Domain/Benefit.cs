﻿namespace Domain
{
    public class Benefit : EntityBase
    {
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
