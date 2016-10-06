using System;
using System.Collections.Generic;
using Chapter10.StoredProcedure.Entities;
using NHibernate;
using NHibernate.Cfg;

namespace Chapter10.StoredProcedure.Tests
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