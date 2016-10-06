using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class SeasonTicketLoan : Benefit
    {
        public virtual int Amount { get; set; }
        public virtual double MonthlyInstalment { get; set; }
        public virtual DateTime StartDate { get; set; }
        public virtual DateTime EndDate { get; set; }
    }
}
