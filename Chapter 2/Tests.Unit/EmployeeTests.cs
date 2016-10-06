using System.Collections.Generic;
using System.Linq;
using Domain;
using NUnit.Framework;

namespace Tests.Unit
{
    [TestFixture]
    public class EmployeeTests
    {
        [Test]
        public void EmployeeIsEntitledToPaidLeaves()
        {
            //Arrange
            var employee = new Employee();

            //Act
            employee.Benefits = new List<Benefit>{new Leave
            {
                Type = LeaveType.Paid,
                AvailableEntitlement = 15
            }};

            //Assert
            var paidLeave = employee.Benefits.FirstOrDefault() as Leave;
            Assert.That(paidLeave, Is.Not.Null);

            if (paidLeave != null) {
                Assert.That(paidLeave.AvailableEntitlement, Is.EqualTo(15));
            }
        }
    }
}
