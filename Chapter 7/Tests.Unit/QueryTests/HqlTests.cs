using System;
using System.Linq;
using Domain;
using NHibernate;
using NHibernate.Linq;
using NUnit.Framework;

namespace Tests.Unit.QueryTests
{
    public class HqlTests : QueryTest
    {
        [Test]
        public void QueryEmployessHavingFirstNameJohn()
        {
            using (var transaction = Database.Session.BeginTransaction())
            {
                var employeeQuery = Database.Session
                    .CreateQuery("select e from Employee as e where e.Firstname = :firstName");
                employeeQuery.SetParameter("firstName", "John");
                var employees = employeeQuery.List<Employee>();

                Assert.That(employees.Count(), Is.EqualTo(1));

                transaction.Commit();
            }
        }

        [Test]
        public void QueryEmployeesWhoHaveJoinedInLast12Months()
        {
            using (var transaction = Database.Session.BeginTransaction())
            {
                var employees = Database.Session
                    .CreateQuery("select e from Employee as e where e.DateOfJoining between :startdate and :enddate")
                    .SetParameter("startdate", DateTime.Now.AddYears(-1))
                    .SetParameter("enddate", DateTime.Now)
                    .List<Employee>();

                Assert.That(employees.Count(), Is.EqualTo(1));

                transaction.Commit();
            }
        }

        [Test]
        public void LeftJoinQuery()
        {
            using (var transaction = Database.Session.BeginTransaction())
            {
                var employeeQuery = Database.Session
                    .CreateQuery("select e from Employee as e left join e.Benefits as s ").List<Employee>();

                Assert.That(employeeQuery.Count(), Is.EqualTo(4));

                transaction.Commit();
            }
        }
    }
}