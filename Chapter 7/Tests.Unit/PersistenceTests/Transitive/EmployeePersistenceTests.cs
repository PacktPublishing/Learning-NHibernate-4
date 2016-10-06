using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using NHibernate.Linq;
using NHibernate.Mapping.ByCode;
using NUnit.Framework;

namespace Tests.Unit.PersistenceTests.Transitive
{
    [TestFixture]
    public class EmployeePersistenceTests : TestUsingInMemoryDatabase
    {
        [Test]
        public void UpdatesValueTypes()
        {
            object id = 0;
            using (var tx = Session.BeginTransaction())
            {
                id = Session.Save(new Employee
                {
                    Firstname = "John",
                    Lastname = "Smith"
                });

                tx.Commit();
            }

            Session.Clear();

            using (var tx = Session.BeginTransaction())
            {
                var emp = Session.Get<Employee>(id);
                emp.Firstname = "Hillary";
                emp.Lastname = "Gamble";
                tx.Commit();
            }

            Session.Clear();

            using (var tx = Session.BeginTransaction())
            {
                var employee = Session.Get<Employee>(id);
                Assert.That(employee.Firstname, Is.EqualTo("Hillary"));
                tx.Commit();
            }

            
        }
        
        [Test]
        public void UpdateEmployee()
        {
            object id = 0;
            using (var tx = Session.BeginTransaction())
            {
                id = Session.Save(new Employee
                {
                    Firstname = "John",
                    Lastname = "Smith"
                });

                tx.Commit();
            }

            Session.Clear();

            using (var tx = Session.BeginTransaction())
            {
                var emp = Session.Get<Employee>(id);

                emp.Firstname = "Hillary";
                emp.Lastname = "Gamble";
                
                tx.Commit();
            }

            Session.Clear();

            using (var tx = Session.BeginTransaction())
            {
                var employee = Session.Get<Employee>(id);
                Assert.That(employee.Firstname, Is.EqualTo("Hillary"));
                Assert.That(employee.Lastname, Is.EqualTo("Gamble"));
                tx.Commit();
            }
        }

        [Test]
        public void AddBenefit()
        {
            object id = 0;
            using (var tx = Session.BeginTransaction())
            {
                id = Session.Save(new Employee
                {
                    Firstname = "John",
                    Lastname = "Smith"
                });

                tx.Commit();
            }

            Session.Clear();

            using (var tx = Session.BeginTransaction())
            {
                var emp = Session.Get<Employee>(id);

                emp.Benefits.Add(new SeasonTicketLoan
                {
                    Amount = 1200,
                    StartDate = new DateTime(2014, 5, 12),
                    EndDate = new DateTime(2015, 12, 4),
                    MonthlyInstalment = 100,
                    Employee = emp
                });

                tx.Commit();
            }

            Session.Clear();

            using (var tx = Session.BeginTransaction())
            {
                var employee = Session.Get<Employee>(id);
                var benefit = employee.Benefits.First();

                Assert.That(benefit, Is.InstanceOf<SeasonTicketLoan>());

                var seasonTicketLoan = benefit as SeasonTicketLoan;

                Assert.That(seasonTicketLoan, Is.Not.Null);
                Assert.That(seasonTicketLoan.Employee.Id, Is.EqualTo(id));
                tx.Commit();
            }
        }

        [Test]
        public void RemovesBenefit()
        {
            object id = 0;
            using (var tx = Session.BeginTransaction())
            {
                var employee = new Employee
                {
                    Firstname = "John",
                    Lastname = "Smith"
                };
                employee.AddBenefit(new SeasonTicketLoan());
                employee.AddBenefit(new Leave());
                employee.AddBenefit(new SkillsEnhancementAllowance());
                id = Session.Save(employee);

                tx.Commit();
            }

            Session.Clear();

            using (var tx = Session.BeginTransaction())
            {
                var emp = Session.Get<Employee>(id);
                var leave = emp.Benefits.FirstOrDefault(b => b.GetType().GetBaseTypes().Contains(typeof (Leave)));

                emp.RemoveBenefit(leave);

                tx.Commit();
            }

            Session.Clear();

            using (var tx = Session.BeginTransaction())
            {
                var employee = Session.Get<Employee>(id);
                var leave = employee.Benefits.FirstOrDefault(b => b.GetType() == typeof(Leave));

                Assert.That(leave, Is.Null);
                tx.Commit();
            }
        }


        [Test]
        public void SetsAddress()
        {
            object id = 0;
            using (var tx = Session.BeginTransaction())
            {
                id = Session.Save(new Employee
                {
                    Firstname = "John",
                    Lastname = "Smith"
                });

                tx.Commit();
            }

            Session.Clear();

            using (var tx = Session.BeginTransaction())
            {
                var emp = Session.Get<Employee>(id);

                emp.ResidentialAddress = new Address
                {
                    AddressLine1 = "Address line 1",
                    AddressLine2 = "Address line 2",
                    City = "city",
                    Postcode = "Postcode",
                    Country = "country"
                };

                tx.Commit();
            }

            Session.Clear();

            using (var tx = Session.BeginTransaction())
            {
                var employee = Session.Get<Employee>(id);

                Assert.That(employee.ResidentialAddress, Is.Not.Null);
                Assert.That(employee.ResidentialAddress.Employee.Id, Is.EqualTo(id));
                tx.Commit();
            }
        }

        [Test]
        public void DeletesCommunity()
        {
            object id = 0;
            using (var tx = Session.BeginTransaction())
            {
                id = Session.Save(new Employee
                {
                    Firstname = "John",
                    Lastname = "Smith",
                    Communities = new HashSet<Community>
                    {
                        
                        new Community
                        {
                            Name = "NHibernate experts club"
                        },
                        new Community
                        {
                            Name = "NHibernate beginners club"
                        }
                    }
                });

                tx.Commit();
            }

            Session.Clear();

            using (var tx = Session.BeginTransaction())
            {
                var emp = Session.Get<Employee>(id);
                var community = Session.Query<Community>().FirstOrDefault(c => c.Name == "NHibernate beginners club");

                emp.RemoveCommunity(community);

                tx.Commit();
            }

            Session.Clear();

            using (var tx = Session.BeginTransaction())
            {
                var employee = Session.Get<Employee>(id);

                Assert.That(employee.Communities.Count, Is.EqualTo(1));
                tx.Commit();
            }
        }
    }
}