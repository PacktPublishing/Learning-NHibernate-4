using System;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using Persistence.Mappings.Fluent;
using Persistence.Mappings.Fluent.TPT;

namespace Tests.Unit.Cfg
{
    public class FluentDatabaseConfiguration
    {
        private readonly ISession session;

        public FluentDatabaseConfiguration()
        {
            var fluentConfig = Fluently.Configure()
                .Database(SQLiteConfiguration.Standard.InMemory().ShowSql().FormatSql())
                .Mappings(mapper =>
                {
                    mapper.FluentMappings.Add<EmployeeMappings>();
                    mapper.FluentMappings.Add<CommunityMappings>();
                    mapper.FluentMappings.Add<BenefitMappings>();
                    mapper.FluentMappings.Add<LeaveMappings>();
                    mapper.FluentMappings.Add<SkillsEnhancementAllowanceMappings>();
                    mapper.FluentMappings.Add<SeasonTicketLoanMappings>();
                    mapper.FluentMappings.Add<AddressMappings>();
                });

            var nhConfiguration = fluentConfig.BuildConfiguration();
            var sessionFactory = nhConfiguration.BuildSessionFactory();
            session = sessionFactory.OpenSession();
            using (var tx = session.BeginTransaction())
            {
                new SchemaExport(nhConfiguration).Execute(true, true, false, session.Connection, Console.Out);
                tx.Commit();
            }
            session.Clear();
        }

        public ISession Session
        {
            get { return session; }
        }
    }
}