using Domain;
using FluentNHibernate.Mapping;

namespace Persistence.Mappings.Fluent.TPT
{
    public class SeasonTicketLoanMappings : SubclassMap<SeasonTicketLoan>
    {
        public SeasonTicketLoanMappings()
        {
            Map(s => s.Amount);
            Map(s => s.MonthlyInstalment);
            Map(s => s.StartDate);
            Map(s => s.EndDate);
        }
    }
}