using PedroMoreira.Domain.Members.Entities;
using PedroMoreira.Domain.Members.ValueObjects;

namespace PedroMoreira.Application.Common.Repository
{
    public interface IMemberRepository
    {
        /// <summary>
        /// Gets Member by Id.
        /// </summary>
        /// <param name="id">Member identifier of user.</param>
        /// <returns>Return user if any exist with this identifier.</returns>
        Task<Member?> GetBydIdAsync(MemberId id);

        /// <summary>
        /// Gets the Member by username.
        /// </summary>
        /// <param name="userName">Username from user.</param>
        /// <returns>Return user if any exist with this Username.</returns>
        Task<Member?> GetMemberByName(string userName);
    }
}
