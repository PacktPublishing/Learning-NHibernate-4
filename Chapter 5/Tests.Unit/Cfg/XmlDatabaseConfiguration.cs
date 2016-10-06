using System;
using System.IO;
using System.Text;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace Tests.Unit.Cfg
{
    public class XmlDatabaseConfiguration
    {
        private readonly ISession session;

        public XmlDatabaseConfiguration()
        {
            var config = new Configuration();
            config.Configure();

            var sessionFactory = config.BuildSessionFactory();
            session = sessionFactory.OpenSession();

            //var script = new StringBuilder();
            //var scriptWriter = new StringWriter(script);

            //using (var tx = session.BeginTransaction())
            //{
            //    new SchemaExport(config).Execute(true, true, false, session.Connection, Console.Out);
            //    tx.Commit();
            //}
            //session.Clear();
            new SchemaExport(config).Create(Console.WriteLine, true);
        }

        public ISession Session {
            get { return session; }
        }
    }
}