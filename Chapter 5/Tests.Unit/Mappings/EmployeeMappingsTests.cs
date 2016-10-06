using System;
using Domain;
using NUnit.Framework;

namespace Tests.Unit.Mappings
{
    [TestFixture]
    public class EmployeeMappingsTests : MappingTests
    {
        [Test]
        public void MapsPrimitiveProperties()
        {
            object id = 0;
            using (var transaction = Session.BeginTransaction())
            {
                id = Session.Save(new Employee
                {
                    EmployeeNumber = "5987123",
                    Firstname = "Hillary",
                    Lastname = "Gamble",
                    EmailAddress = "hillary.gamble@corporate.com",
                    DateOfBirth = new DateTime(1980, 4, 23),
                    DateOfJoining = new DateTime(2010, 7, 12),
                    IsAdmin = true,
                    Password = "Password"
                });
                transaction.Commit();
            }

            Session.Clear();

            using (var transaction = Session.BeginTransaction())
            {
                var employee = Session.Get<Employee>(id);
                Assert.That(employee.EmployeeNumber, Is.EqualTo("5987123"));
                Assert.That(employee.Firstname, Is.EqualTo("Hillary"));
                Assert.That(employee.Lastname, Is.EqualTo("Gamble"));
                Assert.That(employee.EmailAddress, Is.EqualTo("hillary.gamble@corporate.com"));
                Assert.That(employee.DateOfBirth.Year, Is.EqualTo(1980));
                Assert.That(employee.DateOfBirth.Month, Is.EqualTo(4));
                Assert.That(employee.DateOfBirth.Day, Is.EqualTo(23));
                Assert.That(employee.DateOfJoining.Year, Is.EqualTo(2010));
                Assert.That(employee.DateOfJoining.Month, Is.EqualTo(7));
                Assert.That(employee.DateOfJoining.Day, Is.EqualTo(12));
                Assert.That(employee.IsAdmin, Is.True);
                Assert.That(employee.Password, Is.EqualTo("Password"));
                transaction.Commit();
            }
        }

        [Test]
        public void MapsResidentialAddress()
        {
            object id = 0;
            using (var transaction = Session.BeginTransaction())
            {
                var residentialAddress = new Address
                {
                    AddressLine1 = "Address line 1",
                    AddressLine2 = "Address line 2",
                    Postcode = "postcode",
                    City = "city",
                    Country = "country"
                };

                var employee = new Employee
                {
                    EmployeeNumber = "123456789",
                    ResidentialAddress = residentialAddress
                };
                residentialAddress.Employee = employee;

                id = Session.Save(employee);
                transaction.Commit();
            }

            Session.Clear();

            using (var transaction = Session.BeginTransaction())
            {
                var employee = Session.Get<Employee>(id);
                Assert.That(employee.ResidentialAddress.AddressLine1, Is.EqualTo("Address line 1"));
                Assert.That(employee.ResidentialAddress.AddressLine2, Is.EqualTo("Address line 2"));
                Assert.That(employee.ResidentialAddress.Postcode, Is.EqualTo("postcode"));
                Assert.That(employee.ResidentialAddress.City, Is.EqualTo("city"));
                Assert.That(employee.ResidentialAddress.Country, Is.EqualTo("country"));
                Assert.That(employee.ResidentialAddress.Employee.EmployeeNumber, Is.EqualTo("123456789"));
                transaction.Commit();
            }
        }
    }
}

