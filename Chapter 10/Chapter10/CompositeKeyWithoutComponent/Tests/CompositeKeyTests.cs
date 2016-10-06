using System;
using Chapter10.CompositeKeyWithoutComponent.Entities;
using NUnit.Framework;

namespace Chapter10.CompositeKeyWithoutComponent.Tests
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
                    Firstname = "firstName",
                    Lastname = "lastName",
                    DateOfJoining = new DateTime(1999, 2, 26)
                });

                tx.Commit();
            }

            Session.Clear();

            using (var tx = Session.BeginTransaction())
            {
                var id = new Employee
                {
                    Firstname = "firstName",
                    Lastname = "lastName"
                };
                var employee = Session.Get<Employee>(id);
                Assert.That(employee.DateOfJoining.Year, Is.EqualTo(1999));
                tx.Commit();
            }
        }
    }
}