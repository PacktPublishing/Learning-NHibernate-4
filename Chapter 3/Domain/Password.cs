using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Password
    {
        public string Hash { get; set; }
        public bool IsActive { get; set; }
    }
}
