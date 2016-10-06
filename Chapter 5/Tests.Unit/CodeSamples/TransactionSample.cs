using System;
using Domain;
using Tests.Unit.Cfg;
using Tests.Unit.Mappings;

namespace Tests.Unit.CodeSamples
{
    public class TransactionSample : MappingTests
    {
        public void Sample()
        {
            var config = new DatabaseConfigurationForSqlServer();

            using (var session = config.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    try
                    {
                        //Database operations here
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                    }

                }
            }
        }
    }
}