using PedroMoreira.Application.Common.Repository.Member;
using PedroMoreira.Domain.Members.Entities;
using PedroMoreira.Domain.Members.ValueObjects;

namespace PedroMoreira.Infrastructure.Persistence.Repository
{
    /// <summary>
    /// Represents the member repository.
    /// </summary>
    internal sealed class MemberRepository : GenericRepository<Member, MemberId>, IMemberRepository
    {

        /// <summary>
        /// Initialize an instance of <see cref="MemberRepository"/>
        /// </summary>
        /// <param name="context"> The database context. </param>
        public MemberRepository(PostgresContext context)
            : base(context)
        {
        }
    }
}
