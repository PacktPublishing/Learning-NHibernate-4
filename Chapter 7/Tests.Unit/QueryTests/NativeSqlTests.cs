using System.Linq;
using Domain;
using NUnit.Framework;

namespace Tests.Unit.QueryTests
{
    public class NativeSqlTests : QueryTest
    {
        [Test]
        public void QueryEmployessHavingFirstNameJohn()
        {
            using (var transaction = Database.Session.BeginTransaction())
            {
                var employeeQuery = Database.Session
                    .CreateSQLQuery("select * from Employee where Firstname = 'John'")
                    .AddEntity(typeof (Employee));

                var employees = employeeQuery.List<Employee>();

                Assert.That(employees.Count(), Is.EqualTo(1));

                transaction.Commit();
            }
        }
    }
}