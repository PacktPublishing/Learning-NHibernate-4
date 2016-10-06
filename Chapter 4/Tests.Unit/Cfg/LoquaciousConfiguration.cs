using System;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;
using Persistence.Mappings.ByCode;
using Persistence.Mappings.ByCode.TPT;

namespace Tests.Unit.Cfg
{
    public class LoquaciousConfiguration
    {
        private readonly ISession session;

        public LoquaciousConfiguration()
        {
            var config = new Configuration();
            config.DataBaseIntegration(db =>
            {
                db.Dialect<SQLiteDialect>();
                db.Driver<SQLite20Driver>();
                db.ConnectionString = "data source=:memory:";
                db.ConnectionReleaseMode = ConnectionReleaseMode.OnClose;
                //db.SchemaAction = SchemaAutoAction.Create;
                db.LogSqlInConsole = true;
                db.LogFormattedSql = true;
                db.Timeout = 30;
                db.BatchSize = 20;
            })
            .AddMapping(GetMappings());

            var sessionFactory = config.BuildSessionFactory();
            session = sessionFactory.OpenSession();

            using (var tx = session.BeginTransaction())
            {
                new SchemaExport(config).Execute(useStdOut:true, execute:true, justDrop:false, connection:session.Connection, exportOutput:Console.Out);

                tx.Commit();
            }
            session.Clear();
        }

        private HbmMapping GetMappings()
        {
            var mapper = new ModelMapper();
            mapper.AddMapping<EmployeeMappings>();
            mapper.AddMapping<AddressMappings>();
            mapper.AddMapping<BenefitMappings>();
            mapper.AddMapping<LeaveMappings>();
            mapper.AddMapping<SkillsEnhancementAllowanceMappings>();
            mapper.AddMapping<SeasonTicketLoanMappings>();
            mapper.AddMapping<CommunityMappings>();
            return mapper.CompileMappingForAllExplicitlyAddedEntities();
        }

        public ISession Session
        {
            get { return session; }
        }
    }
}