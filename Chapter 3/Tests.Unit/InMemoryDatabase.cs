using System;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Tool.hbm2ddl;
using Environment = NHibernate.Cfg.Environment;

namespace Tests.Unit
{
    public class InMemoryDatabase : IDisposable
    {
        protected readonly bool IsAddressMappedAsComponent;
        protected ISessionFactory SessionFactory;
        protected Configuration Configuration;
        protected string BenefitMappingStrategy;

        public InMemoryDatabase(bool isAddressMappedAsComponent, string benefitMappingStrategy)
        {
            BenefitMappingStrategy = benefitMappingStrategy;
            IsAddressMappedAsComponent = isAddressMappedAsComponent;
            Configuration = new Configuration()
                .SetProperty(Environment.ReleaseConnections, "on_close")
                .SetProperty(Environment.Dialect, typeof(SQLiteDialect).AssemblyQualifiedName)
                .SetProperty(Environment.ConnectionDriver, typeof(SQLite20Driver).AssemblyQualifiedName)
                .SetProperty(Environment.ConnectionString, "data source=:memory:")
                .SetProperty(Environment.ShowSql, "true");
        }

        public virtual void Initialize()
        {
            AddMappings();
            SessionFactory = Configuration.BuildSessionFactory();
            Session = SessionFactory.OpenSession();
            new SchemaExport(Configuration).Execute(true, true, false, Session.Connection, Console.Out);    
        }

        protected virtual void AddMappings() {}

        public ISession Session { get; set; }

        public void Dispose()
        {
            Session.Dispose();
            SessionFactory.Dispose();
        }
    }
}