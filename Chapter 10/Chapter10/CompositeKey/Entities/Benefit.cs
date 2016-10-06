namespace Chapter10.CompositeKey.Entities
{
    public class Benefit
    {
        public virtual int Id { get; set; }
        public virtual Employee Employee { get; set; }
    }
}