using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using NHibernate.Criterion;
using NHibernate.Linq;
using NHibernate.Util;
using NUnit.Framework;

namespace Tests.Unit.QueryTests
{
    public class QueryOverTests : QueryTest
    {
        
        [Test]
        public void QueryEmployeeByName()
        {
            IList<Employee> employees;
            using (var transaction = Database.Session.BeginTransaction())
            {
                employees = Database.Session.QueryOver<Employee>()
                                            .Where(x => x.Firstname == "John")
                                            .List<Employee>();

                transaction.Commit();

            }
            Assert.That(employees.Count, Is.EqualTo(1));
        }

        [Test]
        public void QueryEmployeesWhoJoinedInLastYear()
        {
            IList<Employee> employees;
            using (var transaction = Database.Session.BeginTransaction())
            {
                employees = Database.Session.QueryOver<Employee>()
                                    .WhereRestrictionOn(x => x.DateOfJoining).IsBetween(DateTime.Now.AddYears(-1)).And(DateTime.Now)
                                    .List<Employee>();
                transaction.Commit();
            }
            Assert.That(employees.Count, Is.EqualTo(1));

        }


        [Test]
        public void QueryEmployeesWhoLiveInLondon()
        {
            using (var transaction = Database.Session.BeginTransaction())
            {
                var employees = Database.Session.QueryOver<Employee>()
                    .JoinQueryOver(x => x.ResidentialAddress)
                    .Where(r => r.City == "London")
                    .List<Employee>();
                transaction.Commit();
                Assert.That(employees.Count, Is.EqualTo(3));
            }
        }


        [Test]
        public void QueryEmployeesWhoLiveInLondonWithLimitedResultSet()
        {
            using (var transaction = Database.Session.BeginTransaction())
            {
                var employees = Database.Session.QueryOver<Employee>()
                    .JoinQueryOver(x => x.ResidentialAddress)
                    .Where(r => r.City == "London")
                    .Take(1)
                    .Skip(1)
                    .List<Employee>();
                transaction.Commit();
                Assert.That(employees.Count, Is.EqualTo(1));
            }
        }

        [Test]
        public void QueryEmployeesWithFirstNameJohnAndWhoLiveInLondon()
        {
            using (var transaction = Database.Session.BeginTransaction())
            {
                Address addressAlias = null;
                var employees = Database.Session.QueryOver<Employee>()
                    .JoinAlias(x => x.ResidentialAddress, () => addressAlias)
                    .Where(e => e.Firstname == "John" && addressAlias.City == "London")
                    .List<Employee>();
                transaction.Commit();
                Assert.That(employees.Count, Is.EqualTo(1));
            }
        }

        [Test]
        public void QueryEmployeesWhoAreMemberOfPartcularCommunity()
        {
            IList<Employee> employees;
            using (var transaction = Database.Session.BeginTransaction())
            {
                employees = Database.Session.QueryOver<Employee>()
                                    .JoinQueryOver<Community>(x => x.Communities)
                                    .Where(c => c.Name == "NHibernate Beginners")
                                    .List<Employee>();
                transaction.Commit();
            }

            Assert.That(employees.Count, Is.EqualTo(1));
        }


        [Test]
        public void LoadSeasonTicketLoanBenefitsHavingAmountGreaterThan1000()
        {
            using (var transaction = Database.Session.BeginTransaction())
            {
                var seasonTicketLoans = Database.Session.QueryOver<SeasonTicketLoan>()
                                            .Where(s => s.Amount > 1000)
                                            .List<SeasonTicketLoan>();
                Assert.That(seasonTicketLoans.Count, Is.EqualTo(1));
                Assert.That(seasonTicketLoans[0].Employee, Is.Not.Null);

                transaction.Commit();
            }
        }
    }
}