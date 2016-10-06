using NUnit.Framework;

namespace Chapter10.Join
{
    [TestFixture]
    public class JoinMappingTests : TestUsingInMemoryDatabase 
    {
        [Test]
        public void AddressIsSavedCorrectly()
        {
            object id = 0;
            using (var tx = Session.BeginTransaction())
            {
                id = Session.Save(new Employee
                {
                    AddressLine1 = "address line 1",
                    AddressLine2 = "address line 2"
                });

                tx.Commit();
            }

            Session.Clear();

            using (var tx = Session.BeginTransaction())
            {
                var employee = Session.Get<Employee>(id);
                Assert.That(employee.AddressLine1, Is.EqualTo("address line 1"));
                tx.Commit();
            }
        }
    }
}