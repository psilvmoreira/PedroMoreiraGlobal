using ErrorOr;
using PedroMoreira.Domain.Common.Primitives;
using PedroMoreira.Domain.ErrorMessages;

namespace PedroMoreira.Domain.Authentication.ValueObjects
{
    /// <summary>
    /// Represents the <see cref="ProjectSecurityId"/> Value Object
    /// </summary>
    public sealed class ProjectSecurityId : ValueObject
    {

        /// <summary>
        /// Gets the ProjectSecurityId value.
        /// </summary>
        public Guid value { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectSecurityId"/> Class.
        /// </summary>
        /// <param name="value"></param>
        private ProjectSecurityId(Guid value) => this.value = value;

        public static implicit operator Guid(ProjectSecurityId memberId) => memberId.value;

        /// <summary>
        /// Creates a new <see cref="ProjectSecurityId"/> instance based on the specified value.
        /// </summary>
        /// <param name="id">The Id value.</param>
        /// <returns>The result of the id creation process containing the id or an error.</returns>
        public static ErrorOr<ProjectSecurityId> Create(Guid id)
        {
            if (id == Guid.Empty)
                return Errors.Authentication.ValueCannotBeNullOrEmpty;

            return new ProjectSecurityId(id);
        }

        /// <inheritdoc />
        protected override IEnumerable<object> GetAtomicVales()
        {
            yield return value;
        }
    }
}
