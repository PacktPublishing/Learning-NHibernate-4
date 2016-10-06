using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using NHibernate.Criterion;
using NHibernate.Linq;
using NUnit.Framework;

// ReSharper disable AssignNullToNotNullAttribute

namespace Tests.Unit.QueryTests
{
    [TestFixture]
    public class LinqTests : QueryTest
    {
        [Test]
        public void QueryEmployeeHavingFirstnameAsJohn()
        {
            using (var transaction = Database.Session.BeginTransaction())
            {
                var employees = from e in Database.Session.Query<Employee>()
                    where e.Firstname == "John"
                    select e;
                transaction.Commit();
                Assert.That(employees.Count(), Is.EqualTo(1));
            }
        }

        [Test]
        public void LambdaQueryEmployeeHavingFirstnameAsJohn()
        {
            using (var transaction = Database.Session.BeginTransaction())
            {
                var employees = Database.Session.Query<Employee>().Where(e => e.Firstname == "John");
                transaction.Commit();
                Assert.That(employees.Count(), Is.EqualTo(1));
            }
        }

        [Test]
        public void QueryEmployeesWhoHaveJoinedInLast12Months()
        {
            using (var transaction = Database.Session.BeginTransaction())
            {
                var employees = from e in Database.Session.Query<Employee>()
                                where e.DateOfJoining > DateTime.Now.AddYears(-1) && 
                                      e.DateOfJoining < DateTime.Now
                                select e;
                transaction.Commit();
                Assert.That(employees.Count(), Is.EqualTo(1));
            }
        }

        [Test]
        public void LambdaQueryEmployeesWhoHaveJoinedInLast12Months()
        {
            using (var transaction = Database.Session.BeginTransaction())
            {
                var employees = Database.Session.Query<Employee>()
                                .Where(e => e.DateOfJoining > DateTime.Now.AddYears(-1) && 
                                            e.DateOfJoining < DateTime.Now);
                transaction.Commit();
                Assert.That(employees.Count(), Is.EqualTo(1));
            }
        }

        public void JoinLambda()
        {
            using (var transaction = Database.Session.BeginTransaction())
            {
                var employees = Database.Session.Query<Employee>()
                    .Where(e => e.Benefits.Any(b => b.Name == ""));

                transaction.Commit();
                Assert.That(employees.Count(), Is.EqualTo(2));

            }
        }

        [Test]
        public void QueryEmployeesWhoseSeasonTicketLoanInstallmentIsGreaterThan100()
        {
            using (var transaction = Database.Session.BeginTransaction())
            {
                var employees = from e in Database.Session.Query<Employee>()
                    join b in Database.Session.Query<SeasonTicketLoan>() on e equals b.Employee
                    //where b.MonthlyInstalment > 100
                    select e;
                
                Assert.That(employees.Count(), Is.EqualTo(2));

                transaction.Commit();
            }
        }
       
        [Test]
        public void QueryEmployeesWhoHaveOptedForSeasonTicketLoan()
        {
            using (var transaction = Database.Session.BeginTransaction())
            {
                var employees = Database.Session.Query<Employee>()
                    .Where(e => e.Benefits
                        .Any(b => b is SeasonTicketLoan));

                Assert.That(employees.Count(), Is.EqualTo(2));

                transaction.Commit();
            }
        }

        [Test]
        public void QueryEmployeesWhoHaveOptedForSeasonTicketLoanHavingAmountGreateThan1000()
        {
            using (var transaction = Database.Session.BeginTransaction())
            {
                var employees = from e in Database.Session.Query<Employee>()
                                join s in Database.Session.Query<SeasonTicketLoan>() on e equals s.Employee
                                where s.Amount > 1000
                                select e;

                Assert.That(employees.Count(), Is.EqualTo(1));

                transaction.Commit();
            }
        }

        [Test]
        public void QueryEmployeesWithFirstNameJohnAndWhoLiveInLondon()
        {
            using (var transaction = Database.Session.BeginTransaction())
            {
                var employees = Database.Session.Query<Employee>()
                    .Where(e => e.Firstname == "John" && e.ResidentialAddress.City == "London")
                    .ToList();
                transaction.Commit();
                Assert.That(employees.Count, Is.EqualTo(1));
            }
        }

        [Test]
        public void QueryEmployeesWhoHaveOptedForSeasonTicketLoanInstallment()
        {
            using (var transaction = Database.Session.BeginTransaction())
            {
                var employeeQuery =
                    Database.Session.Query<Employee>().Where(e => e.Benefits.Any(b => b is SeasonTicketLoan));

                Assert.That(employeeQuery.Count(), Is.EqualTo(2));

                transaction.Commit();
            }
        }



        [Test]
        public void QueryEmployeesHavingSeasonTicketLoanAmountGreaterThan1000()
        {
            using (var transaction = Database.Session.BeginTransaction())
            {
                var employeeQuery =
                    Database.Session.Query<Employee>().Where(e => e.Benefits.Any(b => ((SeasonTicketLoan)b).Amount > 1000));

                Assert.That(employeeQuery.Count(), Is.EqualTo(1));

                transaction.Commit();
            }
        }

        [Test]
        public void LeftJoinQuery()
        {
            using (var transaction = Database.Session.BeginTransaction())
            {
                var employees = (from e in Database.Session.Query<Employee>()
                                    from b in e.Benefits.DefaultIfEmpty()
                                    select e).ToList();

                Assert.That(employees.Count, Is.EqualTo(4));

                transaction.Commit();
            }
        }

        [Test]
        public void QueryEmployeesWhoLiveInLondon()
        {
            IList<Employee> employees = null;
            using (var transaction = Database.Session.BeginTransaction())
            {
                employees =
                    Database.Session.Query<Employee>().Where(e => e.ResidentialAddress.City == "London").ToList();
                transaction.Commit();
            }

            Assert.That(employees.Count, Is.EqualTo(3));
        }

        [Test]
        public void ThetaJoinQueryEmployeesWhoLiveInLondon()
        {
            using (var transaction = Database.Session.BeginTransaction())
            {
                var employees = from e in Database.Session.Query<Employee>()
                    join a in Database.Session.Query<Address>() on e equals a.Employee
                    where a.City == "London"
                    select e;

                Assert.That(employees.Count(), Is.EqualTo(3));

                transaction.Commit();
            }
        }

        [Test]
        public void EmployeeIsProxiedWhenAddressIsLoaded()
        {
            IList<Address> addresses = null;
            using (var transaction = Database.Session.BeginTransaction())
            {
                addresses =
                    Database.Session.Query<Address>().Where(e => e.City == "London").ToList();
                transaction.Commit();
            }

            Assert.That(addresses.Count, Is.EqualTo(3));
        }

        [Test]
        public void FetchModeSubSelectTest()
        {
            using (var transaction = Database.Session.BeginTransaction())
            {
                var employeeQuery = Database.Session.Query<Employee>();
                var employees = (from e in employeeQuery
                                select e).Take(2);
                transaction.Commit();

                foreach (var employee in employees)
                {
                    Assert.That(employee.Benefits.Count(), Is.GreaterThan(0));
                }
            }
        }

        [Test]
        public void FetchModeJoinTest()
        {
            using (var transaction = Database.Session.BeginTransaction())
            {
                var employeeQuery = Database.Session.Query<Employee>();
                var employees = from e in employeeQuery
                                select e;
                transaction.Commit();

                foreach (var employee in employees)
                {
                    Assert.That(employee.Communities.Count(), Is.GreaterThan(0));
                }
            }
        }

        [Test]

        [Ignore("This test needs changes to mapping in order to pass")]
        public void LoadDoesNotReturnProxyIfEntityIfNotLazy()
        {
            using (var transaction = Database.Session.BeginTransaction())
            {
                var employee = Database.Session.Load<Employee>(101);
                transaction.Commit();
                Assert.That(employee, Is.TypeOf<Employee>());
            }
        }
    }
}