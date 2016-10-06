using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class SkillsEnhancementAllowance : Benefit
    {
        public virtual int RemainingEntitlement { get; set; }
        public virtual int Entitlement { get; set; }
    }
}
