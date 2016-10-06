using Domain;
using NHibernate.Mapping.ByCode.Conformist;

namespace Persistence.Mappings.ByCode.TPC
{
    public class SeasonTicketLoanMappings : UnionSubclassMapping<SeasonTicketLoan>
    {
        public SeasonTicketLoanMappings()
        {
            Property(s => s.Amount);
            Property(s => s.MonthlyInstalment);
            Property(s => s.StartDate);
            Property(s => s.EndDate);
        }
    }
}
