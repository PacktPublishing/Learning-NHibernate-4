using System;
using System.Collections.Generic;
using Domain;
using NUnit.Framework;
using Tests.Unit.Cfg;
using Tests.Unit.Mappings;

namespace Tests.Unit
{
    [TestFixture]
    public class ObjectEqualityTests
    {
        [Test]
        public void SameEntityLoadedFromTwoDifferentSessionsMatches()
        {
            var config = new DatabaseConfigurationForSqlServer();

            object id = 0;
            var employee1 = new Employee
            {
                EmployeeNumber = "123456789",
                DateOfBirth = new DateTime(1980, 2, 23),
                DateOfJoining = new DateTime(2012, 3, 15)
            };

            using (var session = config.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    id = session.Save(employee1);
                    transaction.Commit();
                }
            }

            Employee employee2 = null;
            using (var session = config.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    employee2 = session.Get<Employee>(id);
                    transaction.Commit();
                }
            }

            Assert.That(employee1.Equals(employee2));
        }
    }
}