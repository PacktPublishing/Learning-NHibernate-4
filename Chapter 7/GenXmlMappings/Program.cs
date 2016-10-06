using System;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Tool.hbm2ddl;
using Tests.Unit.Cfg;
using Environment = NHibernate.Cfg.Environment;

namespace GenXmlMappings
{
    class Program
    {
        static void Main(string[] args)
        {
            //var fluentConfig = new FluentDatabaseConfiguration();

            //new SchemaExport(fluentConfig.Configuration).Create(Console.WriteLine, true);
            //new SchemaUpdate(config).Execute(Console.WriteLine, true);
            Console.ReadLine();
            
        }
    }
}
