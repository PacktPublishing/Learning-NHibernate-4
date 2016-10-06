using System;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Driver;
using NHibernate.Tool.hbm2ddl;
using Persistence.Mappings.Fluent;
using Persistence.Mappings.Fluent.TPT;

namespace Tests.Unit.Cfg
{
    public class FluentDatabaseConfiguration
    {
        private readonly ISession session;
        private Configuration configuration;

        public FluentDatabaseConfiguration()
        {
            var config = Fluently.Configure()
                .Database(SQLiteConfiguration.Standard.InMemory().Driver<SQLite20Driver>())
                .Mappings(mapper =>
                {
                    mapper.FluentMappings.Add<EmployeeMappings>();
                    mapper.FluentMappings.Add<CommunityMappings>();
                    mapper.FluentMappings.Add<BenefitMappings>();
                    mapper.FluentMappings.Add<LeaveMappings>();
                    mapper.FluentMappings.Add<SkillsEnhancementAllowanceMappings>();
                    mapper.FluentMappings.Add<SeasonTicketLoanMappings>();
                    mapper.FluentMappings.Add<AddressMappings>();
                })
                .Cache(cacheBuilder =>
                {
                    cacheBuilder.UseQueryCache();
                    cacheBuilder.UseSecondLevelCache();
                })
                .ExposeConfiguration(cfg =>
                {
                    configuration = cfg;
                });

            var sessionFactory = config.BuildSessionFactory();
            session = sessionFactory.OpenSession();
            new SchemaExport(configuration).Create(Console.WriteLine, true);
            //using (var tx = session.BeginTransaction())
            //{
            //    new SchemaExport(configuration).Create(Console.WriteLine, true);
            //    tx.Commit();
            //}
            //session.Clear();
        }

        public ISession Session
        {
            get { return session; }
        }

        public Configuration Configuration {
            get { return configuration; }
        }
    }
}