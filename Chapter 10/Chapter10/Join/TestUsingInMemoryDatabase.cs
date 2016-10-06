using NHibernate;

namespace Chapter10.Join
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