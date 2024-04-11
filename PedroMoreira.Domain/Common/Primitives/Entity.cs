using System.ComponentModel.DataAnnotations;

namespace PedroMoreira.Domain.Common.Primitives
{
    /// <summary>
    /// Represents the Base Class that all entities derive from
    /// </summary>
    /// <typeparam name="Tkey"></typeparam>
    public abstract class Entity<Tkey> : IEquatable<Entity<Tkey>>
    {

        /// <summary>
        /// Gets or sets the entity identifier.
        /// </summary>
        [Key]
        public Tkey Id { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Entity<Tkey>"/>
        /// </summary>
        /// <param name="id">The Enetity key Identifier </param>
        /// <exception cref="ArgumentNullException">Force Identifier not be null.</exception>
        protected Entity(Tkey id) : this()
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

        public static bool operator !=(Entity<Tkey> a, Entity<Tkey> b) => !(a == b);
        public static bool operator ==(Entity<Tkey> a, Entity<Tkey> b)
        {
            if (a is null && b is null) return true;
            if (a is null || b is null) return false;
            return ((IEquatable<Entity<Tkey>>)a).Equals(b);
        }

        /// <inheritdoc/>
        public bool Equals(Entity<Tkey>? other)
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

            if (!(obj is Entity<Tkey> other)) return false;

            if (string.IsNullOrEmpty(Id.ToString())) return false;

            //if (Id == Guid.Empty || other.Id == Guid.Empty) return false;

            return Id.Equals(other.Id);
        }
    }
}
