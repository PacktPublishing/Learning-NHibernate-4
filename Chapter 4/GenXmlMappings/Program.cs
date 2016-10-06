using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;
using Persistence.Mappings.ByCode;
using Tests.Unit.Mappings;
using BenefitMappings = Persistence.Mappings.ByCode.TPC.BenefitMappings;
using Environment = NHibernate.Cfg.Environment;
using LeaveMappings = Persistence.Mappings.ByCode.TPC.LeaveMappings;
using SeasonTicketLoanMappings = Persistence.Mappings.ByCode.TPC.SeasonTicketLoanMappings;
using SkillsEnhancementAllowanceMappings = Persistence.Mappings.ByCode.TPC.SkillsEnhancementAllowanceMappings;

namespace GenXmlMappings
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new Configuration()
                .SetProperty(Environment.ReleaseConnections, "on_close")
                .SetProperty(Environment.Dialect, typeof(MsSql2012Dialect).AssemblyQualifiedName)
                .SetProperty(Environment.ConnectionDriver, typeof(SqlClientDriver).AssemblyQualifiedName)
                .SetProperty(Environment.ConnectionString, @"Database=EmployeeBenefits;Server=LAPTOP-SUHAS\SQLEXPRESS;Trusted_Connection=True;")
                .SetProperty(Environment.ShowSql, "true");

            config.AddFile("Mappings/Xml/Community.hbm.xml")
                .AddFile("Mappings/Xml/Address.hbm.xml")
                .AddFile("Mappings/Xml/Employee.hbm.xml")
                .AddFile("Mappings/Xml/benefit.subclass.hbm.xml");

            var sessionFactory = config.BuildSessionFactory();
            var session = sessionFactory.OpenSession();

            //new SchemaExport(config).Execute(Console.WriteLine, true);
            new SchemaUpdate(config).Execute(Console.WriteLine, true);
            Console.ReadLine();
            
        }
    }
}
