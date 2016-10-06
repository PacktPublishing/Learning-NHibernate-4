using System.Linq;
using Domain;
using NHibernate.Linq;
using NUnit.Framework;
using Tests.Unit.QueryTests;

namespace Tests.Unit.Batching
{
    [TestFixture]
    public class LazyCollectionBatchingTests : QueryTest
    {
        [Test]
        public void MultipleLazyCollectionAreBatched()
        {
            using (var transaction = Database.Session.BeginTransaction())
            {
                var employees =
                    Database.Session.Query<Employee>()
                        .Where(e => e.ResidentialAddress.City == "London");

                foreach (var employee in employees)
                {
                    Assert.That(employee.Benefits.Count(), Is.GreaterThan(0));
                }
             
                transaction.Commit();
            }
        }

    }
}