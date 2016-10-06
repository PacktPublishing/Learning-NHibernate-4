using System.Collections.Generic;
using System.Linq;
using Domain;
using NHibernate.Criterion;
using NHibernate.Linq;
using NUnit.Framework;

namespace Tests.Unit.QueryTests
{
    public class PaginationTests : QueryTest
    {

        [Test]
        public void Criteria()
        {
            IList<Employee> employees = null;
            using (var transaction = Database.Session.BeginTransaction())
            {
                employees = Database.Session.CreateCriteria<Employee>()
                                    .CreateAlias("ResidentialAddress", "address")
                                    .Add(Restrictions.Eq("address.Country", "United Kingdom"))
                                    .SetMaxResults(1)
                                    .SetFirstResult(1)
                                    .AddOrder(Order.Asc("Firstname"))
                                    .List<Employee>();
                transaction.Commit();
            }

            Assert.That(employees.Count, Is.EqualTo(1));
        }

        [Test]
        public void Hql()
        {
            using (var transaction = Database.Session.BeginTransaction())
            {
                var employeeQuery = Database.Session
                    .CreateQuery("select e from Employee as e where e.Firstname = :firstName")
                    .SetParameter("firstName", "John")
                    .SetMaxResults(1)
                    .SetFirstResult(0);
                var employees = employeeQuery.List<Employee>();

                Assert.That(employees.Count, Is.EqualTo(1));

                transaction.Commit();
            }
        }
        [Test]
        public void QueryOver()
        {
            IList<Employee> employees;
            using (var transaction = Database.Session.BeginTransaction())
            {
                employees = Database.Session.QueryOver<Employee>()
                                            .Where(x => x.Firstname == "John")
                                            .OrderBy(x => x.Firstname).Desc
                                            .Take(1)
                                            .Skip(0)
                                            .List<Employee>();

                transaction.Commit();

            }
            Assert.That(employees.Count, Is.EqualTo(1));
        }

        [Test]
        public void LambdaQueryEmployeeHavingFirstnameAsJohn()
        {
            using (var transaction = Database.Session.BeginTransaction())
            {
                var employees = Database.Session.Query<Employee>()
                    .Where(e => e.Firstname == "John")
                    .OrderByDescending(e => e.Firstname)
                    .Take(1)
                    .Skip(0);
                transaction.Commit();
                Assert.That(employees.Count(), Is.EqualTo(1));
            }
        }
    }
}