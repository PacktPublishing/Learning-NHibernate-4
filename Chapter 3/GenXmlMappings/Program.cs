using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;
using Persistence.Mappings.ByCode;
using Tests.Unit.Mappings;
using BenefitMappings = Persistence.Mappings.ByCode.TPC.BenefitMappings;
using LeaveMappings = Persistence.Mappings.ByCode.TPC.LeaveMappings;
using SeasonTicketLoanMappings = Persistence.Mappings.ByCode.TPC.SeasonTicketLoanMappings;
using SkillsEnhancementAllowanceMappings = Persistence.Mappings.ByCode.TPC.SkillsEnhancementAllowanceMappings;

namespace GenXmlMappings
{
    class Program
    {
        static void Main(string[] args)
        {
            //using (var ostrm = new FileStream("../../mappings.xml", FileMode.OpenOrCreate, FileAccess.Write))
            //{   
            //    var writer = new StreamWriter(ostrm);
            //    writer.AutoFlush = true; 
            //    Console.SetOut(writer);

            //    var modelMapper = new ModelMapper();
            //    modelMapper.AddMappings(new List<Type>
            //    {
            //        typeof(SeasonTicketLoanMappings),
            //        typeof(EmployeeMappings),
            //        typeof(BenefitMappings),
            //        typeof(AddressMappings)
            //    });

            //    var mappings = modelMapper.CompileMappingForAllExplicitlyAddedEntities();
            //    Console.Write(mappings.AsString());
                
            //}
            var configuration = new Configuration()
                             .DataBaseIntegration(db =>
                             {
                                 db.ConnectionString = @"Database=EmployeeBenefits;Server=LAPTOP-SUHAS\SQLEXPRESS;Trusted_Connection=True;";    
                                 //db.ConnectionString = @"Database=EmployeeBenefits;Server=localhost;Trusted_Connection=True;";
                                 db.Dialect<MsSql2008Dialect>();
                                 db.BatchSize = 100;
                             });
            //configuration.AddFile("Mappings/Xml/Benefit.hierarchy.hbm.xml");
            //configuration.AddFile("Mappings/Xml/Employee.hbm.xml");
            //configuration.AddFile("Mappings/Xml/Community.hbm.xml");
            //configuration.AddFile("Mappings/Xml/Address.hbm.xml");

            //var mapper = new ModelMapper();
            //mapper.AddMappings(new List<Type>
            //{
            //    typeof(AddressMappings),
            //    typeof(BenefitMappings),
            //    typeof(CommunityMappings),
            //    typeof(EmployeeMappings),
            //    typeof(LeaveMappings),
            //    typeof(SeasonTicketLoanMappings),
            //    typeof(SkillsEnhancementAllowanceMappings)
            //});
            //configuration.AddMapping(mapper.CompileMappingForAllExplicitlyAddedEntities());

            //var sessionFactory = configuration.BuildSessionFactory();
            //var session = sessionFactory.OpenSession();

            //var script = new StringBuilder();
            //var export = new SchemaExport(configuration);

            //export.Create(s => script.Append(s), false);

            //Console.Write(script);
            var mappingTests = new MappingTests();
            mappingTests.Setup();

            new SchemaExport(configuration).Execute(true, true, false, mappingTests.Session.Connection, Console.Out);
            Console.ReadLine();
            
        }
    }
}
