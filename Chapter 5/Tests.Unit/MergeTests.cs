using System;
using Domain;
using NUnit.Framework;

namespace Tests.Unit
{
    public class MergeTests : TestUsingInMemoryDatabase
    {
        [Test]
        public void MergeReturnsNewEntityInstance()
        {
            object id = 0;

            //This is out first employee instance
            var employee1 = new Employee
            {
                Firstname = "John",
                Lastname = "Smith"
            };

            //Lets first save it so that we have it in session
            using (var tx = Session.BeginTransaction())
            {
                id = Session.Save(employee1);

                tx.Commit();
            }

            //Let's create another instance of Employee with same id
            var employee2 = new Employee
            {
                Id = (int) id
            };

            //Let's merge this new entity with session
            var employee3 = Session.Merge(employee2);
            
            //Let's confirm that employee2 and employee3 are not the same
            Assert.That(Object.ReferenceEquals(employee2, employee3), Is.False);

            //Let's assert that employee1 and employee3 are same instances
            Assert.That(Object.ReferenceEquals(employee1, employee3), Is.True);

        }
    }
}