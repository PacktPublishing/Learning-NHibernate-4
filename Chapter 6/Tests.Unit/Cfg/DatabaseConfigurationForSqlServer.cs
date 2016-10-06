using System;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;
using Persistence.Mappings;
using Environment = NHibernate.Cfg.Environment;

namespace Tests.Unit.Cfg
{
    public class DatabaseConfigurationForSqlServer
    {
        private readonly ISessionFactory sessionFactory;

        public DatabaseConfigurationForSqlServer()
        {
            var config = new Configuration()
                .SetProperty(Environment.ReleaseConnections, "on_close")
                .SetProperty(Environment.Dialect, typeof (MsSql2012Dialect).AssemblyQualifiedName)
                .SetProperty(Environment.ConnectionDriver, typeof (SqlClientDriver).AssemblyQualifiedName)
                .SetProperty(Environment.ConnectionString, @"Server=LAPTOP-SUHAS\SQLEXPRESS;Database=EmployeeBenefits;Trusted_Connection=True;")
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
            //using (var session = sessionFactory.OpenSession())
            //{
            //    new SchemaExport(config).Execute(true, true, false, session.Connection, Console.Out);
            //}
        }

        public ISession OpenSession()
        {
            return sessionFactory.OpenSession();
        }
    }
}