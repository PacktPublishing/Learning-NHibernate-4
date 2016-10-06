using NHibernate;
using NUnit.Framework;

namespace Tests.Unit.Mappings
{
    public class MappingTests
    {
        protected string MappingType;
        protected InMemoryDatabase DatabaseXml;
        public ISession SessionXml;
        protected bool IsAddressMappedAsComponent;

        private const string Xml = "Xml";
        private const string ByCode = "ByCode";
        private const string Fluent = "Fluent";

        protected object[] MappingMethods =
        {
            new object[] {"Xml", "TPC"},
            new object[] {"Xml", "TPH"},
            new object[] {"Xml", "TPT"},
            new object[] {"ByCode", "TPC"},
            new object[] { "ByCode", "TPH"},
            new object[] { "ByCode", "TPT"},
            new object[] {"Fluent", "TPC"},
            new object[] { "Fluent", "TPH"},
            new object[] { "Fluent", "TPT"},
        };

        protected object[] MappingMethodsExceptConcrete =
        {
            new object[] {"Xml", "TPH"},
            new object[] {"Xml", "TPT"},
            new object[] { "ByCode", "TPH"},
            new object[] { "ByCode", "TPT"},
            new object[] { "Fluent", "TPH"},
            new object[] { "Fluent", "TPT"},
        };

        protected object[] MappingMethodsWithConcreteOnly =
        {
            new object[] {"Xml", "TPC"},
            new object[] {"ByCode", "TPC"},
            new object[] {"Fluent", "TPC"},
        };

        protected object[] MappingMethodsWithSubclassOnly =
        {
            new object[] {"Xml", "TPT"},
            new object[] {"ByCode", "TPT"},
            new object[] {"Fluent", "TPT"},
        };

        private InMemoryDatabaseForCodeMappings databaseByCode;
        private InMemoryDatabaseForFluentMappings databaseFluent;
        private ISession sessionByCode;
        private ISession sessionFluent;

        //This method is called from console application used to generate database scripts using SchemaExport
        //while running the programme, one of the sections of the below code needs to be uncommented
        [TestFixtureSetUp]
        public void Setup()
        {
            //DatabaseXml = new InMemoryDatabaseForXmlMappings(IsAddressMappedAsComponent);
            //DatabaseXml.Initialize();
            //SessionXml = DatabaseXml.Session;

            //databaseByCode = new InMemoryDatabaseForCodeMappings(IsAddressMappedAsComponent);
            //databaseByCode.Initialize();
            //sessionByCode = databaseByCode.Session;

            //databaseFluent = new InMemoryDatabaseForFluentMappings(IsAddressMappedAsComponent);
            //databaseFluent.Initialize();
            //sessionFluent = databaseFluent.Session;


        }

        public ISession Session
        {
            get
            {
                if (MappingType == Xml)
                {
                    return SessionXml;
                }
                if (MappingType == ByCode)
                {
                    return sessionByCode;
                }
                if (MappingType == Fluent)
                {
                    return sessionFluent;
                }

                return null;
            }
        }

        public void Use(string mappingMethod, string benefitMappingStrategy)
        {
            MappingType = mappingMethod;

            if (MappingType == Xml)
            {
                DatabaseXml = new InMemoryDatabaseForXmlMappings(IsAddressMappedAsComponent, benefitMappingStrategy);
                DatabaseXml.Initialize();
                SessionXml = DatabaseXml.Session;
            }
            if (MappingType == ByCode)
            {
                databaseByCode = new InMemoryDatabaseForCodeMappings(IsAddressMappedAsComponent, benefitMappingStrategy);
                databaseByCode.Initialize();
                sessionByCode = databaseByCode.Session;
            }
            if (MappingType == Fluent)
            {
                databaseFluent = new InMemoryDatabaseForFluentMappings(IsAddressMappedAsComponent, benefitMappingStrategy);
                databaseFluent.Initialize();
                sessionFluent = databaseFluent.Session;
            }
        }
    }
}