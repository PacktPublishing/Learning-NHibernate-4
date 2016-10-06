using Domain;
using NHibernate.Mapping.ByCode.Conformist;

namespace Persistence.Mappings.ByCode.TPT
{
    public class SeasonTicketLoanMappings : JoinedSubclassMapping<SeasonTicketLoan>
    {
        public SeasonTicketLoanMappings()
        {
            Key(k => k.Column("Id"));
            Property(s => s.Amount);
            Property(s => s.MonthlyInstalment);
            Property(s => s.StartDate);
            Property(s => s.EndDate);
        }
    }
}
