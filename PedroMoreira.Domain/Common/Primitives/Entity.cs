using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PedroMoreira.Domain.Common.Primitives
{
    /// <summary>
    /// Represents the Base Class that all entities derive from
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public abstract class Entity<TKey> : IEquatable<Entity<TKey>>
    {

        /// <summary>
        /// Gets or sets the entity identifier.
        /// </summary>
        [Key]
        public TKey Id { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Entity<Tkey>"/>
        /// </summary>
        /// <param name="id">The Enetity key Identifier </param>
        /// <exception cref="ArgumentNullException">Force Identifier not be null.</exception>
        protected Entity(TKey id) : this()
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id), "Id cannot be null.");

            Id = id;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Entity"/> class.
        /// </summary>
        /// <remarks>
        /// Required by EF Core.
        /// </remarks>
        protected Entity() { }

        public static bool operator !=(Entity<TKey> a, Entity<TKey> b) => !(a == b);
        public static bool operator ==(Entity<TKey>? a, Entity<TKey>? b)
        {
            if (a is null && b is null) return true;
            if (a is null || b is null) return false;
            return ((IEquatable<Entity<TKey>?>)a).Equals(b);
        }

        /// <inheritdoc/>
        public bool Equals(Entity<TKey>? other)
        {
            if (other is null) return false;
            return ReferenceEquals(this, other) || Id!.Equals(other.Id);
        }

        /// <inheritdoc/>
        public override int GetHashCode() => Id.GetHashCode() * 41;

        /// <inheritdoc/>
        public override bool Equals(object? obj)
        {
            if (obj is null) return false;

            if (ReferenceEquals(this, obj)) return true;

            if (ReferenceEquals(obj, null)) return false;

            if (obj.GetType() != GetType()) return false;

            if (!(obj is Entity<TKey> other)) return false;

            if (string.IsNullOrEmpty(Id.ToString())) return false;

            //if (Id == Guid.Empty || other.Id == Guid.Empty) return false;

            return Id.Equals(other.Id);
        }
    }
}
