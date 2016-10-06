namespace Chapter10.CompositeKey.Entities
{
    public class EmployeeId
    {
        public virtual string Firstname { get; set; }
        public virtual string Lastname { get; set; }

        public override bool Equals(object obj)
        {
            var otherEmployee = obj as EmployeeId;

            if (otherEmployee == null) return false;

            return Firstname.Equals(otherEmployee.Firstname) && Lastname.Equals(otherEmployee.Lastname);
        }

        public override int GetHashCode()
        {
            var hash = 17;
            hash = hash * 37 + Firstname.GetHashCode();
            hash = hash * 37 + Lastname.GetHashCode();
            return hash;
        }
    }
}