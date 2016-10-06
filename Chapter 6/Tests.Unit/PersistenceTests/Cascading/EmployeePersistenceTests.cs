using System.Linq;
using Domain;
using NUnit.Framework;

namespace Tests.Unit.PersistenceTests.Cascading
{
    [TestFixture]
    public class EmployeePersistenceTests : TestUsingInMemoryDatabase
    {
        
        [Test]
        public void DeleteEmployee()
        {
            object id = 0;
            using (var tx = Session.BeginTransaction())
            {
                var employee = new Employee
                {
                    Firstname = "John",
                    Lastname = "Smith"
                };
                employee.AddBenefit(new Leave
                {
                    AvailableEntitlement = 25,
                    RemainingEntitlement = 14
                });
                employee.AddCommunity(new Community
                {
                    Name = "New joiners"
                });

                id = Session.Save(employee);

                tx.Commit();
            }

            Session.Clear();

            using (var tx = Session.BeginTransaction())
            {
                var emp = Session.Get<Employee>(id);
                Session.Delete(emp);
                tx.Commit();
            }

            Session.Clear();

            using (var tx = Session.BeginTransaction())
            {
                var employee = Session.Get<Employee>(id);
                Assert.That(employee, Is.Null);
                tx.Commit();
            }
        }
    }
}