using Domain;
using FluentNHibernate.Mapping;

namespace Persistence.Mappings.Fluent.TPH
{
    public class SeasonTicketLoanMappings : SubclassMap<SeasonTicketLoan>
    {
        public SeasonTicketLoanMappings()
        {
            DiscriminatorValue("STL");
            Map(s => s.Amount);
            Map(s => s.MonthlyInstalment);
            Map(s => s.StartDate);
            Map(s => s.EndDate);
        }
    }
}