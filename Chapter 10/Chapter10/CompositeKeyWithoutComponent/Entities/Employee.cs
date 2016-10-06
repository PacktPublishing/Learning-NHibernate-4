using System;
using System.Collections.Generic;

namespace Chapter10.CompositeKeyWithoutComponent.Entities
{
    public class Employee
    {
        public virtual string Firstname { get; set; }
        public virtual string Lastname { get; set; }

        public virtual DateTime DateOfJoining { get; set; }

        public override bool Equals(object obj)
        {
            var otherEmployee = obj as Employee;

            if (otherEmployee == null) return false;

            return string.Equals(Firstname, otherEmployee.Firstname) && string.Equals(Lastname, otherEmployee.Lastname);
        }

        public override int GetHashCode()
        {
            var hash = 17;
            hash = hash*37 + Firstname.GetHashCode();
            hash = hash*37 + Lastname.GetHashCode();
            return hash;
        }
    }
}