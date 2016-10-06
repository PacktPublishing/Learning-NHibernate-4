using System;
using NHibernate.Exceptions;
using NUnit.Framework;

namespace Chapter10.Views.Tests
{
    [TestFixture]
    public class ViewTests
    {
        protected SqlServerDatabase Database;

        public ViewTests()
        {
            Database = new SqlServerDatabase();
            Database.Initialize();
        }

        [Test]
        public void ReadsFromView()
        {
            using (var transaction = Database.Session.BeginTransaction())
            {
                var employee = Database.Session.Get<Employee>(1155);
                Assert.That(employee, Is.Not.Null);
                employee.Firstname = "some other name";
                transaction.Commit();
            }
        }

        [Test]
        public void ThrowsExceptionOnWritingToView()
        {
            using (var transaction = Database.Session.BeginTransaction())
            {
                var employee = new Employee
                {
                    Firstname = "John",
                    Lastname = "Dove",
                    DateOfJoining = new DateTime(1999, 3, 21)
                };
                Database.Session.Save(employee);
                Assert.Throws<GenericADOException>(() => transaction.Commit()) ;
            }
        }
    }
}