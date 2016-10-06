using System.Linq;
using Domain;
using NHibernate.Linq;
using NUnit.Framework;
using Tests.Unit.QueryTests;

namespace Tests.Unit.Batching
{
    [TestFixture]
    public class FutureQueryTests : QueryTest
    {
        [Test]
        public void FutureQueriesAreBatched()
        {
            using (var transaction = Database.Session.BeginTransaction())
            {
                var employees =
                    Database.Session.Query<Employee>()
                        .ToFutureValue(e => e.Count());
                
                var communities =
                    Database.Session.Query<Community>()
                        .ToFuture();

                var topCommunities =
                    Database.Session.Query<Community>()
                    .OrderByDescending(c => c.Members.Count())
                    .Take(5)
                        .ToFuture();

                var benefits =
                    Database.Session.Query<Benefit>()
                        .ToFuture();

                Assert.That(employees.Value, Is.EqualTo(3));
                Assert.That(communities.Count(), Is.EqualTo(4));
                Assert.That(topCommunities.Count(), Is.EqualTo(4));
                Assert.That(benefits.Count(), Is.EqualTo(4));
                transaction.Commit();
            }
        }    
    }
}