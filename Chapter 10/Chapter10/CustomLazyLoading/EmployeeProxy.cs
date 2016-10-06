using System.Collections.Generic;

namespace Chapter10.CustomLazyLoading
{
    public class EmployeeProxy : Employee
    {
        private readonly StaffMember staffMember;

        public EmployeeProxy(StaffMember staffMember)
        {
            this.staffMember = staffMember;
        }

        public override ICollection<Benefit> Benefits
        {
            get { return staffMember.Benefits; }
            set { staffMember.Benefits = value; }
        }

        public override Address ResidentialAddress
        {
            get { return staffMember.ResidentialAddress; }
            set { staffMember.ResidentialAddress = value; }
        }
    }
}