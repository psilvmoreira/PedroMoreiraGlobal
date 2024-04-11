
namespace PedroMoreira.Domain.Common.Primitives
{
    public abstract class ValueObject : IEquatable<ValueObject>
    {

        public static bool operator ==(ValueObject? a, ValueObject? b) {
            if(a is null && b is null) return true;
            if(a is null || b is null) return false;
            return a.Equals(b);
        }

        public static bool operator !=(ValueObject a, ValueObject b) => !(a == b);

        /// <inheritdoc />
        public bool Equals(ValueObject? other) => !(other is null) && GetAtomicVales().SequenceEqual(other.GetAtomicVales());
        
        /// <inheritdoc />
        public override bool Equals(object? obj)
        {
            if (obj == null)
                return false;

            if (GetType() != obj.GetType())
                return false;

            if (!(obj is ValueObject valueObject))
                return false;

            return GetAtomicVales().SequenceEqual(valueObject.GetAtomicVales());
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            HashCode code = default;

            foreach (var atomicVale in GetAtomicVales())
            {
                code.Add(atomicVale);
            }
            
            return code.ToHashCode();
        }

        /// <summary>
        /// Gets the atomic values of the value object.
        /// </summary>
        /// <returns>The collection of objects representing the value object values.</returns>
        protected abstract IEnumerable<object> GetAtomicVales();
    }
}
