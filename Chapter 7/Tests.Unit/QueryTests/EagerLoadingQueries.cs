using System.Collections.Generic;
using System.Linq;
using Domain;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using NHibernate.Transform;
using NUnit.Framework;

namespace Tests.Unit.QueryTests
{
    [TestFixture]
    public class EagerLoadingQueries : QueryTest
    {
        [Test]
        public void Hql()
        {
            using (var transaction = Database.Session.BeginTransaction())
            {
                var employeeQuery = Database.Session
                    .CreateQuery("select e from Employee as e join fetch e.Benefits where e.Firstname = :firstName");
                employeeQuery.SetParameter("firstName", "John");
                var employees = employeeQuery.List<Employee>();

                Assert.That(employees.Count(), Is.EqualTo(2));

                transaction.Commit();
            }
        }

        [Test]
        public void Criteria()
        {
            using (var transaction = Database.Session.BeginTransaction())
            {
                var employees = Database.Session.CreateCriteria<Employee>()
                    .Add(Restrictions.Eq("Firstname", "John"))
                    .SetFetchMode("Benefits", FetchMode.Join)
                    .List<Employee>();

                Assert.That(employees.Count, Is.EqualTo(2));
                transaction.Commit();
            }
        }

        [Test]
        public void QueryOver()
        {
            using (var transaction = Database.Session.BeginTransaction())
            {
                Address residentialAddress = null;
                var employees = Database.Session.QueryOver<Employee>()
                    .JoinAlias(e => e.ResidentialAddress, () => residentialAddress)
                    .Where(() => residentialAddress.City == "London")
                    .Fetch(e => e.Communities).Eager
                    .Fetch(e => e.Benefits).Eager
                    .TransformUsing(Transformers.DistinctRootEntity)
                    .List<Employee>();

                Assert.That(employees.Count, Is.EqualTo(3));
                transaction.Commit();
            }
        }

        [Test]
        public void QueryOverUsingFutureQuery()
        {
            using (var transaction = Database.Session.BeginTransaction())
            {
                Address residentialAddress = null;
                var employees = Database.Session.QueryOver<Employee>()
                    .JoinAlias(e => e.ResidentialAddress, () => residentialAddress)
                    .Where(() => residentialAddress.City == "London")
                    .Future<Employee>();

                Database.Session.QueryOver<Employee>()
                    .JoinAlias(e => e.ResidentialAddress, () => residentialAddress)
                    .Where(() => residentialAddress.City == "London")
                    .Fetch(e => e.Communities).Eager
                    .Future<Employee>();

                Database.Session.QueryOver<Employee>()
                    .JoinAlias(e => e.ResidentialAddress, () => residentialAddress)
                    .Where(() => residentialAddress.City == "London")
                    .Fetch(e => e.Benefits).Eager
                    .Future<Employee>();

                Assert.That(employees.Count(), Is.EqualTo(3));
                transaction.Commit();
            }
        }

        [Test]
        public void Linq()
        {
            using (var transaction = Database.Session.BeginTransaction())
            {
                var employees = Database.Session.Query<Employee>()
                    .Where(x => x.ResidentialAddress.City == "London")
                    .FetchMany(e => e.Communities)
                    .FetchMany(x => x.Benefits)
                    .ToList();

                Assert.That(employees.Count(), Is.EqualTo(3));
                transaction.Commit();
            }
        }

        [Test]
        public void LinqUsingFutureQuery()
        {
            using (var transaction = Database.Session.BeginTransaction())
            {
                var employees = Database.Session.Query<Employee>()
                    .Where(x => x.ResidentialAddress.City == "London")
                    .ToFuture();

                Database.Session.Query<Employee>()
                    .Where(x => x.ResidentialAddress.City == "London")
                    .FetchMany(e => e.Communities)
                    .ToFuture();

                Database.Session.Query<Employee>()
                    .Where(x => x.ResidentialAddress.City == "London")
                    .FetchMany(x => x.Benefits)
                    .ToFuture();

                Assert.That(employees.Count(), Is.EqualTo(3));
                transaction.Commit();
            }
        }
    }
}