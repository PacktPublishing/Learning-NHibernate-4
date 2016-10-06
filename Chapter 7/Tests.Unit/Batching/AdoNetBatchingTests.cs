using System;
using Domain;
using NUnit.Framework;

namespace Tests.Unit.Batching
{
    [TestFixture]
    public class AdoNetBatchingTests
    {
        protected SqlServerDatabase Database;
        public AdoNetBatchingTests()
        {
            Database = new SqlServerDatabase();
            Database.Initialize();
        }

        [Test]
        public void InsertLargeNumberOfEmployeeRecords()
        {
            Database.Session.SetBatchSize(100);
            using (var transaction = Database.Session.BeginTransaction())
            {
                for (int i = 0; i < 100; i++)
                {
                    var employee = new Employee
                    {
                        DateOfBirth = new DateTime(1972, 3, 5),
                        DateOfJoining = new DateTime(2001, 5, 28),
                        ResidentialAddress = new Address()
                    };
                    employee.AddCommunity(new Community());

                    Database.Session.Save(employee);
                }

                transaction.Commit();
            }
        }
    }
}