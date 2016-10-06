using System;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;
using Persistence.Mappings.ByCode;
using Persistence.Mappings.ByCode.TPT;
using Environment = NHibernate.Cfg.Environment;

namespace Tests.Unit.Cfg
{
    public class ProgrammaticDatabaseConfiguration
    {
        private ISession session;
        private readonly ISessionFactory sessionFactory;

        public ProgrammaticDatabaseConfiguration()
        {
            var config = new Configuration()
                .SetProperty(Environment.ReleaseConnections, "on_close")
                .SetProperty(Environment.Dialect, typeof (SQLiteDialect).AssemblyQualifiedName)
                .SetProperty(Environment.ConnectionDriver, typeof (SQLite20Driver).AssemblyQualifiedName)
                .SetProperty(Environment.ConnectionString, "data source=:memory:")
                .SetProperty(Environment.ShowSql, "true")
                .SetProperty(Environment.FormatSql, "true");

            var modelMapper = new ModelMapper();
            modelMapper.AddMapping<EmployeeMappings>();
            modelMapper.AddMapping<AddressMappings>();
            modelMapper.AddMapping<BenefitMappings>();
            modelMapper.AddMapping<LeaveMappings>();
            modelMapper.AddMapping<SkillsEnhancementAllowanceMappings>();
            modelMapper.AddMapping<SeasonTicketLoanMappings>();
            modelMapper.AddMapping<CommunityMappings>();

            config.AddMapping(modelMapper.CompileMappingForAllExplicitlyAddedEntities());

            sessionFactory = config.BuildSessionFactory();
            session = sessionFactory.OpenSession();
            using (var tx = session.BeginTransaction())
            {
                new SchemaExport(config).Execute(true, true, false, session.Connection, Console.Out);
                tx.Commit();
            }
            session.Clear();
        }

        public ISession Session
        {
            get { return session; }
        }

        public ISession OpenSession()
        {
            session = sessionFactory.OpenSession();
            return session;
        }
    }
}