using ErrorOr;
using PedroMoreira.Domain.Common.Primitives;
using PedroMoreira.Domain.ErrorMessages;

namespace PedroMoreira.Domain.Authentication.ValueObjects
{
    /// <summary>
    /// Represents the <see cref="ProjectSecurityClaimId"/> Value Object
    /// </summary>
    public sealed class ProjectSecurityClaimId : ValueObject
    {

        /// <summary>
        /// Gets the ProjectSecurityRoleId
        /// </summary>
        public Guid Value { get; }

        /// <summary>
        /// Initializes a new Instance of the <see cref="ProjectSecurityClaimId"/>
        /// </summary>
        /// <param name="value"></param>
        private ProjectSecurityClaimId(Guid value) => this.Value = value;


        /// <summary>
        /// Creates a new <see cref="ProjectSecurityId"/> instance based on the specified value.
        /// </summary>
        /// <param name="id">The Id value.</param>
        /// <returns>The result of the id creation process containing the id or an error.</returns>
        public static ErrorOr<ProjectSecurityClaimId> Create(Guid id)
        {
            if (id == Guid.Empty)
                return Errors.Authentication.ValueCannotBeNullOrEmpty;

            return new ProjectSecurityClaimId(id);
        }

        protected override IEnumerable<object> GetAtomicVales()
        {
            yield return Value;
        }
    }
}
