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
        public ISession Session;
        protected Configuration Configuration;
        protected string BenefitMappingStrategy;

        public InMemoryDatabase(bool isAddressMappedAsComponent, string benefitMappingStrategy)
        {
            BenefitMappingStrategy = benefitMappingStrategy;
            IsAddressMappedAsComponent = isAddressMappedAsComponent;
            Configuration = new InMemoryDatabaseConfiguration();
        }

        public virtual void Initialize()
        {
            AddMappings();
            SessionFactory = Configuration.BuildSessionFactory();
            Session = SessionFactory.OpenSession();
            new SchemaExport(Configuration).Execute(true, true, false, Session.Connection, Console.Out);    
        }

        protected virtual void AddMappings() {}

        public void Dispose()
        {
            Session.Dispose();
        }
    }
}