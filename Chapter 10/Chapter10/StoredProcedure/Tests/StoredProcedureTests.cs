using Chapter10.StoredProcedure.Entities;
using NHibernate.Transform;
using NUnit.Framework;

namespace Chapter10.StoredProcedure.Tests
{
    [TestFixture]
    public class StoredProcedureTests
    {
        protected SqlServerDatabase Database;

        public StoredProcedureTests()
        {
            Database = new SqlServerDatabase();
            Database.Initialize();
        }

        [Test]
        public void ExecutesStoredProcedure()
        {
            using (var transaction = Database.Session.BeginTransaction())
            {
                var query = Database.Session.CreateSQLQuery(@"EXEC [dbo].[Get_Employee] @id = :id");
                query.SetInt32("id", 55);
                query.SetResultTransformer(Transformers.AliasToBean<Employee>());
                var employees = query.List<Employee>();
                Assert.That(employees.Count, Is.EqualTo(1));
            }
        }
    }
}