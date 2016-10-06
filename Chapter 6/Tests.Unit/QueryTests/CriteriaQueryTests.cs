using System;
using System.Collections.Generic;
using Domain;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using NUnit.Framework;

namespace Tests.Unit.QueryTests
{
    [TestFixture]
    public class CriteriaQueryTests : QueryTest
    {
        [Test]
        public void QueryEmployeeByName()
        {
            IList<Employee> employees = null;
            using (var transaction = Database.Session.BeginTransaction())
            {
                employees = Database.Session.CreateCriteria<Employee>()
                                            .Add(Restrictions.Eq("Firstname", "John"))
                                            .AddOrder(Order.Asc("Firstname"))
                                            .List<Employee>();

                transaction.Commit();
    
            }

            Assert.That(employees.Count, Is.EqualTo(1));
        }

        [Test]
        public void QueryEmployeesWhoJoinedInLastYear()
        {
            IList<Employee> employees = null;
            using (var transaction = Database.Session.BeginTransaction())
            {
                employees = Database.Session.CreateCriteria<Employee>()
                                    .Add(Restrictions.Between("DateOfJoining", DateTime.Now.AddYears(-1), DateTime.Now))
                                    .List<Employee>();
                transaction.Commit();
            }
            Assert.That(employees.Count, Is.EqualTo(1));

        }

        [Test]
        public void QueryEmployeesWhoLiveInLondon()
        {
            IList<Employee> employees = null;
            using (var transaction = Database.Session.BeginTransaction())
            {
                employees = Database.Session.CreateCriteria<Employee>()
                                    .CreateCriteria("ResidentialAddress")
                                    .Add(Restrictions.Eq("City", "London"))
                                    .SetMaxResults(10)
                                    .List<Employee>();
                transaction.Commit();
            }

            Assert.That(employees.Count, Is.EqualTo(3));
        }

        [Test]
        public void QueryEmployeesWhoAreMemberOfPartcularCommunity()
        {
            IList<Employee> employees = null;
            using (var transaction = Database.Session.BeginTransaction())
            {
                employees = Database.Session.CreateCriteria<Employee>()
                                    .CreateCriteria("Communities")
                                    .Add(Restrictions.Eq("Name", "NHibernate Beginners"))
                                    .List<Employee>();
                transaction.Commit();
            }

            Assert.That(employees.Count, Is.EqualTo(1));
        }

        [Test]
        public void QueryEmployeesWhoLiveInLondonWithLimitedResultSet()
        {
            IList<Employee> employees = null;
            using (var transaction = Database.Session.BeginTransaction())
            {
                employees = Database.Session.CreateCriteria<Employee>()
                                    .CreateAlias("ResidentialAddress", "address")
                                    //.CreateCriteria("ResidentialAddress")
                                    .Add(Restrictions.Eq("address.City", "London"))
                                    .SetMaxResults(1)
                                    .SetFirstResult(1)
                                    .AddOrder(Order.Asc("Firstname"))
                                    .List<Employee>();
                transaction.Commit();
            }

            Assert.That(employees.Count, Is.EqualTo(1));
        }

    }
}