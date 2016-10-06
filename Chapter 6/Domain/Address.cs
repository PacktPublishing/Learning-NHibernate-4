namespace Domain
{
    public class Address : EntityBase<Address>
    {
        public virtual string AddressLine1 { get; set; }
        public virtual string AddressLine2 { get; set; }
        public virtual string Postcode { get; set; }
        public virtual string City { get; set; }
        public virtual string Country { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
