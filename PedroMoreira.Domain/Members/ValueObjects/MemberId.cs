using ErrorOr;
using PedroMoreira.Domain.Common.Primitives;
using PedroMoreira.Domain.ErrorMessages;

namespace PedroMoreira.Domain.Members.ValueObjects
{
    /// <summary>
    /// Represents the MemberId Value Object
    /// </summary>
    public sealed class MemberId : ValueObject
    {

        /// <summary>
        /// Gets the memberId value.
        /// </summary>
        public Guid value { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MemberId"/> Class.
        /// </summary>
        /// <param name="value"></param>
        private MemberId(Guid value) => this.value = value;

        public static implicit operator Guid(MemberId memberId) => memberId.value;

        /// <summary>
        /// Creates a new <see cref="MemberId"/> instance based on the specified value.
        /// </summary>
        /// <param name="id">The Id value.</param>
        /// <returns>The result of the id creation process containing the email or an error.</returns>
        public static ErrorOr<MemberId> Create(Guid id)
        {
            if (id == Guid.Empty)
                return Errors.Common.UnknownError;

            return new MemberId(id);
        }

        /// <inheritdoc />
        protected override IEnumerable<object> GetAtomicVales()
        {
            yield return value;
        }
    }
}
