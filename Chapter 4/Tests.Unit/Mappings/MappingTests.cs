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
            var config = new LoquaciousConfiguration();
            Session = config.Session;
        }
    }
}