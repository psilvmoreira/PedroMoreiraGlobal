using ErrorOr;
using PedroMoreira.Domain.Common.Primitives;
using PedroMoreira.Domain.ErrorMessages;

namespace PedroMoreira.Domain.Authentication.ValueObjects
{
    /// <summary>
    /// Represents the <see cref="ProjectSecurityRoleId"/> Value Object
    /// </summary>
    public sealed class ProjectSecurityRoleId : ValueObject
    {

        /// <summary>
        /// Gets the ProjectSecurityRoleId
        /// </summary>
        public Guid Value { get; }

        /// <summary>
        /// Initializes a new Instance of the <see cref="ProjectSecurityRoleId"/>
        /// </summary>
        /// <param name="value"></param>
        private ProjectSecurityRoleId(Guid value) => this.Value = value;


        /// <summary>
        /// Creates a new <see cref="ProjectSecurityId"/> instance based on the specified value.
        /// </summary>
        /// <param name="id">The Id value.</param>
        /// <returns>The result of the id creation process containing the id or an error.</returns>
        public static ErrorOr<ProjectSecurityRoleId> Create(Guid id)
        {
            if (id == Guid.Empty)
                return Errors.Authentication.ValueCannotBeNullOrEmpty;

            return new ProjectSecurityRoleId(id);
        }

        protected override IEnumerable<object> GetAtomicVales()
        {
            yield return Value;
        }
    }
}
