using System;
using System.Collections.Generic;
using System.IO;
using Domain;
using log4net.Config;
using NHibernate;

namespace Tests.Unit
{
    public class InMemoryDatabase : IDisposable
    {
        protected ISessionFactory SessionFactory;
        public ISession Session;
        protected InMemoryDatabaseConfiguration Configuration;
        protected string BenefitMappingStrategy;

        public InMemoryDatabase()
        {
            Configuration = new InMemoryDatabaseConfiguration();
        }

        public virtual void Initialize()
        {
            AddMappings();
            //SessionFactory = Configuration.BuildSessionFactory();
            //Session = SessionFactory.OpenSession();
            Session = Configuration.Session;
            //new SchemaExport(Configuration).Execute(true, true, false, Session.Connection, Console.Out);
            XmlConfigurator.Configure(new FileInfo("log4net.config"));
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