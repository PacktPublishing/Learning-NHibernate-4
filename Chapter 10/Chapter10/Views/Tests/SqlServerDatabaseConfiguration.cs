using NHibernate;
using NHibernate.Cache;
using NHibernate.Cfg;
using NHibernate.Context;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;

namespace Chapter10.Views.Tests
{
    public class SqlServerDatabaseConfiguration : Configuration
    {
        public SqlServerDatabaseConfiguration()
        {
            this.DataBaseIntegration(db =>
            {
                db.ConnectionReleaseMode = ConnectionReleaseMode.OnClose;
                db.LogSqlInConsole = true;
                db.LogFormattedSql = true;
                db.Timeout = 30;
            });

            SetProperty(Environment.Dialect, typeof(MsSql2012Dialect).AssemblyQualifiedName);
            SetProperty(Environment.ConnectionDriver, typeof(SqlClientDriver).AssemblyQualifiedName);
            SetProperty(Environment.ConnectionString, @"Data Source=LAPTOP-SUHAS\SQLEXPRESS;Database=EmployeeBenefits;Trusted_Connection = yes;");
            SetProperty(Environment.BatchSize, "100");

            this.Cache(cache =>
            {
                cache.UseQueryCache = true;
                //cache.QueryCache<StandardQueryCache>();
                cache.Provider<HashtableCacheProvider>();
            });

            this.CurrentSessionContext<ThreadLocalSessionContext>();

            var modelMapper = new ModelMapper();
            modelMapper.AddMapping<EmployeeSubselectMapping>();


            AddMapping(modelMapper.CompileMappingForAllExplicitlyAddedEntities());
        }
    }
}