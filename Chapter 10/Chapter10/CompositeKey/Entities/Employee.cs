using System;
using System.Collections.Generic;

namespace Chapter10.CompositeKey.Entities
{
    public class Employee
    {
        public virtual EmployeeId Id { get; set; }
        public virtual DateTime DateOfJoining { get; set; }
        public virtual ICollection<Benefit> Benefits { get; set; }

        public virtual void AddBenefit(Benefit benefit)
        {
            benefit.Employee = this;
            if(Benefits == null) Benefits = new List<Benefit>();
            Benefits.Add(benefit);
        }
    }
}