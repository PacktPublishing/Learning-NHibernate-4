namespace Domain
{
    public class EntityBase<T> where T : EntityBase<T>
    {
        private int? hashCode;
        public override int GetHashCode()
        {
            if (hashCode.HasValue) return hashCode.Value;

            var transientEntity = Id == 0;
            if (transientEntity)
            {
                hashCode = base.GetHashCode();
                return hashCode.Value;
            }
            return Id.GetHashCode();
        }

        public virtual int Id { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as T;
            if (other == null) return false;

            var thisIsTransient = Id == 0;
            var otherIsTransient = other.Id == 0;

            if (thisIsTransient && otherIsTransient)
                return ReferenceEquals(this, other);

            return Id == other.Id;
        }
        public static bool operator ==(EntityBase<T> lhs, EntityBase<T> rhs)
        {
            return Equals(lhs, rhs);
        }
        public static bool operator !=(EntityBase<T> lhs, EntityBase<T> rhs)
        {
            return !Equals(lhs, rhs);
        }
    }
}