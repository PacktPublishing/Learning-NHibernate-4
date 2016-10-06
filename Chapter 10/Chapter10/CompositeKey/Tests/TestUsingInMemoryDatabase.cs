using NHibernate;

namespace Chapter10.CompositeKey.Tests
{
    public class TestUsingInMemoryDatabase
    {
        public ISession Session;
        public TestUsingInMemoryDatabase()
        {
            var config = new ProgrammaticDatabaseConfiguration();
            Session = config.Session;
        }
    }
}