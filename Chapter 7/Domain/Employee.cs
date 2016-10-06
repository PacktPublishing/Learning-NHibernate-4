using System;
using System.Collections.Generic;

namespace Domain
{
    public class Employee : EntityBase<Employee>
    {
        public Employee()
        {
            Benefits = new HashSet<Benefit>();
            Communities = new HashSet<Community>();
        }
        public virtual string EmployeeNumber { get; set; }
        public virtual string Firstname { get; set; }
        public virtual string Lastname { get; set; }
        public virtual string EmailAddress { get; set; }
        public virtual string Designation { get; set; }
        public virtual DateTime DateOfBirth { get; set; }
        public virtual DateTime DateOfJoining { get; set; }

        private Address residentialAddress;
        public virtual Address ResidentialAddress {
            get { return residentialAddress; }
            set
            {
                residentialAddress = value;
                if (value != null) residentialAddress.Employee = this;
            }
        }
        public virtual bool IsAdmin { get; set; }
        public virtual string Password { get; set; }
        public virtual ICollection<Benefit> Benefits { get; set; }
        public virtual ICollection<Community> Communities { get; set; }

        public virtual void AddBenefit(Benefit benefit)
        {
            benefit.Employee = this;
            Benefits.Add(benefit);
        }
        public virtual void RemoveBenefit(Benefit benefit)
        {
            benefit.Employee = null;
            Benefits.Remove(benefit);
        }

        public virtual void AddCommunity(Community community)
        {
            community.Members.Add(this);
            Communities.Add(community);
        }

        public virtual void RemoveCommunity(Community community)
        {
            Communities.Remove(community);
            community.Members.Remove(this);
        }
    }
}
