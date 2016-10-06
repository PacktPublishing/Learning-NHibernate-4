using Domain;
using NHibernate.Mapping.ByCode.Conformist;

namespace Persistence.Mappings.ByCode.TPH
{
    public class SeasonTicketLoanMappings : SubclassMapping<SeasonTicketLoan>
    {
        public SeasonTicketLoanMappings()
        {
            DiscriminatorValue("STL");
            Property(s => s.Amount);
            Property(s => s.MonthlyInstalment);
            Property(s => s.StartDate);
            Property(s => s.EndDate);
        }
    }
}
