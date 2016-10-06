using System;
using NHibernate;
using NHibernate.Cache;
using NHibernate.Cfg;
using NHibernate.Context;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;
using Persistence.Mappings;
using Environment = NHibernate.Cfg.Environment;

namespace Tests.Unit
{
    public class InMemoryDatabaseConfiguration : Configuration
    {
        private readonly ISession session;

        public InMemoryDatabaseConfiguration()
        {
            this.DataBaseIntegration(db =>
            {
                db.ConnectionReleaseMode = ConnectionReleaseMode.OnClose;
                //db.Dialect<SQLiteDialect>();
                //db.Driver<SQLite20Driver>();
                //db.ConnectionString = "data source=:memory";
                db.LogSqlInConsole = true;
                db.LogFormattedSql = true;
                db.BatchSize = 20;
                db.Timeout = 30;
            });

            SetProperty(Environment.Dialect, typeof(SQLiteDialect).AssemblyQualifiedName);
            SetProperty(Environment.ConnectionDriver, typeof(SQLite20Driver).AssemblyQualifiedName);
            SetProperty(Environment.ConnectionString, "data source=:memory:");

            this.Cache(cache =>
            {
                cache.UseQueryCache = true;
                //cache.QueryCache<StandardQueryCache>();
                cache.Provider<HashtableCacheProvider>();
            });

            this.CurrentSessionContext<ThreadLocalSessionContext>();

            var modelMapper = new ModelMapper();
            modelMapper.AddMapping<EmployeeMappings>();
            modelMapper.AddMapping<AddressMappings>();
            modelMapper.AddMapping<BenefitMappings>();
            modelMapper.AddMapping<LeaveMappings>();
            modelMapper.AddMapping<SkillsEnhancementAllowanceMappings>();
            modelMapper.AddMapping<SeasonTicketLoanMappings>();
            modelMapper.AddMapping<CommunityMappings>();

            AddMapping(modelMapper.CompileMappingForAllExplicitlyAddedEntities());

            var sessionFactory = BuildSessionFactory();
            session = sessionFactory.OpenSession();
            using (var tx = session.BeginTransaction())
            {
                new SchemaExport(this).Execute(true, true, false, session.Connection, Console.Out);
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