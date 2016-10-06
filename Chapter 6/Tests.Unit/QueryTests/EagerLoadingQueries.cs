using System.Collections.Generic;
using System.Linq;
using Domain;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
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
                var employees = Database.Session.QueryOver<Employee>()
                    .Where(x => x.Firstname == "John")
                    .Fetch(e => e.Communities).Eager
                    .Fetch(e => e.Communities.First().Members).Eager
                    .List<Employee>();

                Assert.That(employees.Count, Is.EqualTo(1));
                transaction.Commit();
            }
        }

        [Test]
        public void Linq()
        {
            using (var transaction = Database.Session.BeginTransaction())
            {
                var employees = Database.Session.Query<Employee>()
                    .Where(x => x.Firstname == "John")
                    .FetchMany(e => e.Communities)
                    .ThenFetch(c => c.Members)
                    .ToList();

                Assert.That(employees.Count(), Is.EqualTo(1));
                transaction.Commit();
            }
        }
    }
}