using NHibernate;
using NUnit.Framework;
using Tests.Unit.Cfg;

namespace Tests.Unit.Mappings
{
    public class MappingTests
    {
        public ISession Session;

        [TestFixtureSetUp]
        public void Setup()
        {
            var config = new ProgrammaticDatabaseConfiguration();
            Session = config.Session;

            log4net.Config.XmlConfigurator.Configure();
        }
    }
}