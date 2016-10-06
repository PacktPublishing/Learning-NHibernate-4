using Domain.Component;
using NUnit.Framework;

namespace Tests.Unit.Mappings
{
    [TestFixture]
    public class AddressComponenetMappingTests : MappingTests
    {
        public AddressComponenetMappingTests()
        {
            IsAddressMappedAsComponent = true;
        }
        [Test, TestCaseSource("MappingMethods")]
        public void MapsResidentialAddressAsComponent(string mappingMethod, string benefitMappingStrategy)
        {
            Use(mappingMethod, benefitMappingStrategy);
            object id = 0;
            using (var transaction = Session.BeginTransaction())
            {
                var employee = new Employee
                {
                    EmployeeNumber = "123456789",
                    ResidentialAddress = new Address
                    {
                        AddressLine1 = "Address line 1",
                        AddressLine2 = "Address line 2",
                        Postcode = "postcode",
                        City = "city",
                        Country = "country"
                    }
                };

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
                transaction.Commit();
            }
        }
    }
}