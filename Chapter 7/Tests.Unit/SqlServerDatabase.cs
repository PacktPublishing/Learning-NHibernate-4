using System;
using System.Collections.Generic;
using System.IO;
using Domain;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace Tests.Unit
{
    public class SqlServerDatabase : IDisposable
    {
        protected ISessionFactory SessionFactory;
        public ISession Session;
        protected Configuration Configuration;
        protected string BenefitMappingStrategy;

        public SqlServerDatabase()
        {
            Configuration = new SqlServerDatabaseConfiguration();
        }

        public virtual void Initialize()
        {
            AddMappings();
            SessionFactory = Configuration.BuildSessionFactory();
            Session = SessionFactory.OpenSession();
            log4net.Config.XmlConfigurator.Configure(new FileInfo("log4net.config"));
        }

        protected virtual void AddMappings() {}

        public void Dispose()
        {
            Session.Dispose();
        }

        public void SeedUsing(List<Employee> employees)
        {
            using (var transaction = Session.BeginTransaction())
            {
                foreach (var employee in employees)
                {
                    Session.Save(employee);
                }
                transaction.Commit();
            }
            Session.Clear();
        }
    }
}