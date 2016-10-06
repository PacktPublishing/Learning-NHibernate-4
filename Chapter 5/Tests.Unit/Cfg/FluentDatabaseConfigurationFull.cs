using System;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cache;
using NHibernate.Cfg;
using NHibernate.Context;
using NHibernate.Tool.hbm2ddl;
using Persistence.Mappings.Fluent;
using Persistence.Mappings.Fluent.TPT;
using Environment = NHibernate.Cfg.Environment;

namespace Tests.Unit.Cfg
{
    public class FluentDatabaseConfigurationFull
    {
        private readonly ISession session;
        private Configuration configuration;

        public FluentDatabaseConfigurationFull()
        {
            var config = Fluently.Configure()
                .Database(MsSqlConfiguration
                                .MsSql2012
                                .ConnectionString(c => c.FromConnectionStringWithKey("EmployeeBenefits"))
                                .ShowSql()
                                .AdoNetBatchSize(50))
                .CurrentSessionContext<ThreadLocalSessionContext>()
                .Mappings(mapper =>
                {
                    mapper.FluentMappings.AddFromAssemblyOf<EmployeeMappings>();
                })
                .Cache(cacheBuilder =>
                {
                    cacheBuilder.UseQueryCache();
                    cacheBuilder.UseSecondLevelCache();
                    cacheBuilder.ProviderClass<HashtableCacheProvider>();
                })
                .ExposeConfiguration(cfg =>
                {
                    configuration = cfg;
                    cfg.SetProperty(Environment.CommandTimeout, "30");
                });

            var sessionFactory = config.BuildSessionFactory();
            session = sessionFactory.OpenSession();
            using (var tx = session.BeginTransaction())
            {
                new SchemaExport(configuration).Execute(true, true, false, session.Connection, Console.Out);
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