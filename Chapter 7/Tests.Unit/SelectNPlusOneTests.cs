using System.Linq;
using Domain;
using NHibernate.Linq;
using NUnit.Framework;

namespace Tests.Unit.QueryTests
{
    [TestFixture]
    public class SelectNPlusOneTests : QueryTest
    {
        [Test]
        public void WithSelectNPlusOneIssue()
        {
            using (var transaction = Database.Session.BeginTransaction())
            {
                var employees = Database.Session.Query<Employee>().Where(e => e.ResidentialAddress.City == "London");

                foreach (var employee in employees)
                {
                    foreach (var benefit in employee.Benefits)
                    {
                        Assert.That(benefit.Employee, Is.Not.Null);
                    }
                }
                transaction.Commit();
            }
        }
    }
}