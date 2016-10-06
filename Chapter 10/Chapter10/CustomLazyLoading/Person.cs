using System;

namespace Chapter10.CustomLazyLoading
{
    public class Person
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string EmailAddress { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Address ResidentialAddress { get; set; }
    }


}
