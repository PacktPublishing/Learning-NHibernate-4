using System;
using System.Collections.Generic;
using NHibernate.Mapping.ByCode;
using Persistence.Mappings.ByCode;
using EmployeeMappings = Persistence.Mappings.ByCode.Component.EmployeeMappings;

namespace Tests.Unit
{
    public class InMemoryDatabaseForCodeMappings : InMemoryDatabase
    {
        public InMemoryDatabaseForCodeMappings(bool isAddressMappedAsComponent, string benefitMapppingStrategy) : base(isAddressMappedAsComponent, benefitMapppingStrategy)
        {
            var mapper = new ModelMapper();

            if (IsAddressMappedAsComponent)
            {
                mapper.AddMappings(new List<Type>
                {
                    typeof(EmployeeMappings)
                });
            }
            else
            {
                mapper.AddMappings(new List<Type>
                {
                    typeof(CommunityMappings),
                    typeof (AddressMappings),
                    typeof (Persistence.Mappings.ByCode.EmployeeMappings),
                });

                if (BenefitMappingStrategy == "TPC")
                {
                    mapper.AddMappings(new List<Type>
                    {
                        typeof (Persistence.Mappings.ByCode.TPC.BenefitMappings),
                        typeof (Persistence.Mappings.ByCode.TPC.LeaveMappings),
                        typeof (Persistence.Mappings.ByCode.TPC.SeasonTicketLoanMappings),
                        typeof (Persistence.Mappings.ByCode.TPC.SkillsEnhancementAllowanceMappings)
                    });
                }
                else if (BenefitMappingStrategy == "TPH")
                {
                    mapper.AddMappings(new List<Type>
                    {
                        typeof (Persistence.Mappings.ByCode.TPH.BenefitMappings),
                        typeof (Persistence.Mappings.ByCode.TPH.LeaveMappings),
                        typeof (Persistence.Mappings.ByCode.TPH.SeasonTicketLoanMappings),
                        typeof (Persistence.Mappings.ByCode.TPH.SkillsEnhancementAllowanceMappings)
                    });
                }
                else
                {
                    mapper.AddMappings(new List<Type>
                    {
                        typeof (Persistence.Mappings.ByCode.TPT.BenefitMappings),
                        typeof (Persistence.Mappings.ByCode.TPT.LeaveMappings),
                        typeof (Persistence.Mappings.ByCode.TPT.SeasonTicketLoanMappings),
                        typeof (Persistence.Mappings.ByCode.TPT.SkillsEnhancementAllowanceMappings)
                    });
                }
            }


            Configuration.AddMapping(mapper.CompileMappingForAllExplicitlyAddedEntities());
        }
    }
}