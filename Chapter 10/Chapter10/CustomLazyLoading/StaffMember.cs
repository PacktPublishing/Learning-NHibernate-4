using System;
using System.Collections.Generic;

namespace Chapter10.CustomLazyLoading
{
    public class StaffMember : Person
    {
        public string EmployeeNumber { get; set; }
        public DateTime DateOfJoining { get; set; }
        public ICollection<Benefit> Benefits { get; set; }
    }
}