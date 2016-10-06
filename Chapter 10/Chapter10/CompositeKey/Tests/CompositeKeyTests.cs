using System;
using Chapter10.CompositeKey.Entities;
using NUnit.Framework;

namespace Chapter10.CompositeKey.Tests
{
    [TestFixture]
    public class CompositeKeyTests : TestUsingInMemoryDatabase 
    {
        [Test]
        public void EmployeeIsSavedCorrectly()
        {
            using (var tx = Session.BeginTransaction())
            {
                Session.Save(new Employee
                {
                    Id = new EmployeeId
                    {
                        Firstname = "firstName",
                        Lastname = "lastName",
                    },
                    DateOfJoining = new DateTime(1999, 2, 26)
                });

                tx.Commit();
            }

            Session.Clear();

            using (var tx = Session.BeginTransaction())
            {
                var id = new EmployeeId
                {
                    Firstname = "firstName",
                    Lastname = "lastName"
                };
                var employee = Session.Get<Employee>(id);
                Assert.That(employee.DateOfJoining.Year, Is.EqualTo(1999));
                tx.Commit();
            }
        }

        [Test]
        public void BenefitsAssociationIsSavedCorrectly()
        {
            using (var tx = Session.BeginTransaction())
            {
                var employee = new Employee
                {
                    Id = new EmployeeId
                    {
                        Firstname = "firstName",
                        Lastname = "lastName",
                    },
                    DateOfJoining = new DateTime(1999, 2, 26)
                };
                employee.AddBenefit(new Benefit());

                Session.Save(employee);

                tx.Commit();
            }

            Session.Clear();

            using (var tx = Session.BeginTransaction())
            {
                var id = new EmployeeId
                {
                    Firstname = "firstName",
                    Lastname = "lastName"
                };

                var employee = Session.Get<Employee>(id);
                Assert.That(employee.DateOfJoining.Year, Is.EqualTo(1999));
                Assert.That(employee.Benefits.Count, Is.EqualTo(1));
                tx.Commit();
            }
        }
    }
}