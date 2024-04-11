using ErrorOr;
using PedroMoreira.Domain.Common.Primitives;
using PedroMoreira.Domain.ErrorMessages;

namespace PedroMoreira.Domain.Projects.ValueObjects
{
    /// <summary>
    /// Represents the MemberId Value Object
    /// </summary>
    public sealed class ProjectId : ValueObject
    {

        /// <summary>
        /// Gets the ProjectId value.
        /// </summary>
        public Guid value { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectSecurityId"/> Class.
        /// </summary>
        /// <param name="value"></param>
        private ProjectId(Guid value) => this.value = value;

        public static implicit operator Guid(ProjectId memberId) => memberId.value;

        /// <summary>
        /// Creates a new <see cref="ProjectSecurityId"/> instance based on the specified value.
        /// </summary>
        /// <param name="id">The Id value.</param>
        /// <returns>The result of the id creation process containing the email or an error.</returns>
        public static ErrorOr<ProjectId> Create(Guid id)
        {
            if (id == Guid.Empty)
                return Errors.Authentication.ValueCannotBeNullOrEmpty;

            return new ProjectId(id);
        }

        /// <inheritdoc />
        protected override IEnumerable<object> GetAtomicVales()
        {
            yield return value;
        }
    }
}
