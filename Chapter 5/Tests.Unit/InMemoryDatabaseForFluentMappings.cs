using System;
using System.Collections.Generic;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Tool.hbm2ddl;
using Persistence.Mappings.Fluent;
using EmployeeMappings = Persistence.Mappings.Fluent.Component.EmployeeMappings;

namespace Tests.Unit
{
    public class InMemoryDatabaseForFluentMappings : InMemoryDatabase
    {
        public InMemoryDatabaseForFluentMappings(bool isAddressMappedAsComponent, string benefitMapppingStrategy) : base(isAddressMappedAsComponent, benefitMapppingStrategy)
        {
            SessionFactory = Fluently.Configure()
                .Database(SQLiteConfiguration.Standard.InMemory().ShowSql())
                .Mappings(FluentMappings)
                .ExposeConfiguration(x =>
                {
                    Configuration = x;
                })
                .BuildSessionFactory();

        }

        private void FluentMappings(MappingConfiguration configuration)
        {
            var mappings = new List<Type>();

            if (IsAddressMappedAsComponent)
            {
                mappings.Add(typeof(EmployeeMappings));
            }
            else
            {
                mappings.Add(typeof(CommunityMappings));
                mappings.Add(typeof(AddressMappings));
                mappings.Add(typeof (Persistence.Mappings.Fluent.EmployeeMappings));

                if (BenefitMappingStrategy == "TPC")
                {
                    mappings.Add(typeof(Persistence.Mappings.Fluent.TPC.BenefitMappings));
                    mappings.Add(typeof(Persistence.Mappings.Fluent.TPC.LeaveMappings));
                    mappings.Add(typeof(Persistence.Mappings.Fluent.TPC.SeasonTicketLoanMappings));
                    mappings.Add(typeof(Persistence.Mappings.Fluent.TPC.SkillsEnhancementAllowanceMappings));
                }
                else if (BenefitMappingStrategy == "TPH")
                {
                    mappings.Add(typeof(Persistence.Mappings.Fluent.TPH.BenefitMappings));
                    mappings.Add(typeof(Persistence.Mappings.Fluent.TPH.LeaveMappings));
                    mappings.Add(typeof(Persistence.Mappings.Fluent.TPH.SeasonTicketLoanMappings));
                    mappings.Add(typeof(Persistence.Mappings.Fluent.TPH.SkillsEnhancementAllowanceMappings));
                }
                else
                {
                    mappings.Add(typeof(Persistence.Mappings.Fluent.TPT.BenefitMappings));
                    mappings.Add(typeof(Persistence.Mappings.Fluent.TPT.LeaveMappings));
                    mappings.Add(typeof(Persistence.Mappings.Fluent.TPT.SeasonTicketLoanMappings));
                    mappings.Add(typeof(Persistence.Mappings.Fluent.TPT.SkillsEnhancementAllowanceMappings));
                }
            }

            foreach (var mapping in mappings)
            {
                configuration.FluentMappings.Add(mapping);
            }
            configuration.FluentMappings.ExportTo("fluentmappings");
        }

        public override void Initialize()
        {
            Session = SessionFactory.OpenSession();
            new SchemaExport(Configuration).Execute(true, true, false, Session.Connection, Console.Out);
        }
    }
}