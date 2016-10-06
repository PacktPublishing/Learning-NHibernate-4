using System.Collections.Generic;
using Domain.Component;
using NUnit.Framework;

namespace Tests.Unit.CodeSamples
{
    [TestFixture]
    public class QueryOverQueries
    {
        private InMemoryDatabaseForCodeMappings database;

        public QueryOverQueries()
        {
            database = new InMemoryDatabaseForCodeMappings(false, "TPT");
        }

        [Test]
        public void SimpleQuery()
        {
            database.SeendUsing(new List<Employee>
            {
                new Employee
                {
                    EmployeeNumber = "123456"
                }
            });

            IList<Employee> employees = null;
            using (var transaction = database.Session.BeginTransaction())
            {
                var criteria = database.Session.CreateCriteria<Employee>();
                employees = criteria.List<Employee>();
                transaction.Commit();
            }

            Assert.That(employees.Count, Is.EqualTo(1));
        }
    }
}