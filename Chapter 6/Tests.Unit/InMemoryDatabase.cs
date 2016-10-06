using System;
using System.Collections.Generic;
using System.IO;
using Domain;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace Tests.Unit
{
    public class InMemoryDatabase : IDisposable
    {
        protected ISessionFactory SessionFactory;
        public ISession Session;
        protected Configuration Configuration;
        protected string BenefitMappingStrategy;

        public InMemoryDatabase()
        {
            Configuration = new InMemoryDatabaseConfiguration();
        }

        public virtual void Initialize()
        {
            AddMappings();
            SessionFactory = Configuration.BuildSessionFactory();
            Session = SessionFactory.OpenSession();
            new SchemaExport(Configuration).Execute(true, true, false, Session.Connection, Console.Out);
            log4net.Config.XmlConfigurator.Configure();
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