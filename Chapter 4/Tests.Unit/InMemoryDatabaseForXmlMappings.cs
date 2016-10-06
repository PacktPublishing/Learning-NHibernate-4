namespace Tests.Unit
{
    public class InMemoryDatabaseForXmlMappings : InMemoryDatabase
    {
        public InMemoryDatabaseForXmlMappings(bool isAddressMappedAsComponent, string benefitMapppingStrategy) : base(isAddressMappedAsComponent, benefitMapppingStrategy)
        {
        }

        protected override void AddMappings()
        {   
            if (IsAddressMappedAsComponent)
            {
                Configuration
                    .AddFile("Mappings/Xml/Employee.address.component.hbm.xml");
            }
            else
            {
                Configuration
                    .AddFile("Mappings/Xml/Community.hbm.xml")
                    .AddFile("Mappings/Xml/Address.hbm.xml")
                    .AddFile("Mappings/Xml/Employee.hbm.xml");

                if (BenefitMappingStrategy == "TPC")
                {
                    Configuration.AddFile("Mappings/Xml/benefit.concrete.hbm.xml");
                }
                else if (BenefitMappingStrategy == "TPH")
                {
                    Configuration.AddFile("Mappings/Xml/benefit.hierarchy.hbm.xml");
                }
                else
                {
                    Configuration.AddFile("Mappings/Xml/benefit.subclass.hbm.xml");
                }
            }
        }
    }
}
