using System.IO;
using NHibernate;
using Tests.Unit.Cfg;

namespace Tests.Unit
{
    public class TestUsingInMemoryDatabase
    {
        public ISession Session;
        public TestUsingInMemoryDatabase()
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo("log4net.config"));
            var config = new ProgrammaticDatabaseConfiguration();
            Session = config.Session;
        }
    }
}