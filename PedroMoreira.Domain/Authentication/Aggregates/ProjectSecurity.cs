using PedroMoreira.Domain.Authentication.Entities;
using PedroMoreira.Domain.Authentication.ValueObjects;
using PedroMoreira.Domain.Common.Abstractions;
using PedroMoreira.Domain.Common.Primitives;
using PedroMoreira.Domain.Members.Entities;
using PedroMoreira.Domain.Members.ValueObjects;
using PedroMoreira.Domain.Projects.Entities;
using System.Collections.ObjectModel;
using PedroMoreira.Domain.Projects.ValueObjects;

namespace PedroMoreira.Domain.Authentication.Aggregates
{
    
    
    public sealed class ProjectSecurity : AggregateRoot<ProjectSecurityId>, IAuditableEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectSecurity"/> Class.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="member">The User.</param>
        /// <param name="project">The Project</param>
        /// <exception cref="ArgumentNullException"></exception>
        public ProjectSecurity(
            ProjectSecurityId id,
            MemberId member,
            ProjectId project)
            : base(id)
        {
            MemberId = member;
            ProjectId = project;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectSecurity"/> Class.
        /// </summary>
        /// <remarks>Required by EF Core.</remarks>
        public ProjectSecurity(): base() { }

        /// <summary>
        /// Gets the Project Identifier
        /// </summary>
        public ProjectId ProjectId { get; private set; }

        /// <summary>
        /// Gets the Member Identifier
        /// </summary>
        public MemberId MemberId { get; private set; }

        /// <inheritdoc />
        public DateTime CreatedOnUtc { get; }

        /// <inheritdoc />
        public DateTime? ModifiedOnUtc { get; }

        public Project Project { get; init; }

        public Member Member { get; init; }

        public ICollection<ProjectSecurityRole> Roles { get; private set; }

        public ICollection<ProjectSecurityClaim> Claims { get; private set; }

        public static ProjectSecurity Create(
            ProjectSecurityId id, 
            MemberId memberId,
            ProjectId projectId)
        {
            return new ProjectSecurity(id, memberId, projectId);
        }

        public void AddRole(ProjectSecurityRole role)
        {
            if (!Roles.Any())
                Roles = new Collection<ProjectSecurityRole>();

            Roles.Add(role);
        }

        public void AddClaim(ProjectSecurityClaim claim)
        {
            if (!Claims.Any())
                Claims = new Collection<ProjectSecurityClaim>();

            Claims.Add(claim);
        }
    }
}
