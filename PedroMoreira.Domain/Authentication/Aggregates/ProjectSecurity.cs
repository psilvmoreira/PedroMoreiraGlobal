using PedroMoreira.Domain.Authentication.Entities;
using PedroMoreira.Domain.Authentication.ValueObjects;
using PedroMoreira.Domain.Common.Abstractions;
using PedroMoreira.Domain.Common.Primitives;
using PedroMoreira.Domain.Members.Entities;
using PedroMoreira.Domain.Members.ValueObjects;
using PedroMoreira.Domain.Projects.Entities;
using System.Collections.ObjectModel;

namespace PedroMoreira.Domain.Authentication.Aggregates
{
    public sealed class ProjectSecurity : AggregateRoot<ProjectSecurityId>, IAuditableEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectSecurity"/> Class.
        /// </summary>
        /// <param name="member">The User.</param>
        /// <param name="project">The Project</param>
        /// <exception cref="ArgumentNullException"></exception>
        public ProjectSecurity(
            ProjectSecurityId id,
            MemberId member,
            ProjectSecurityId project)
            : base(id)
        {
            MemberId = member;
            ProjectId = project;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectSecurity"/> Class.
        /// </summary>
        /// <remarks>Required by EF Core.</remarks>
        private ProjectSecurity()
        {
        }

        /// <summary>
        /// Gets the Project Identifier
        /// </summary>
        public Guid ProjectId { get; private set; }

        /// <summary>
        /// Gets the Member Identifier
        /// </summary>
        public MemberId MemberId { get; private set; }

        /// <inheritdoc />
        public DateTime CreatedOnUtc { get; }

        /// <inheritdoc />
        public DateTime? ModifiedOnUtc { get; }

        public Project Project { get; set; }

        public Member Member { get; set; }

        public ICollection<ProjectSecurityRole> Roles { get; private set; }

        public ICollection<ProjectSecurityClaim> Claims { get; private set; }

        public static ProjectSecurity Create(ProjectSecurityId Id, MemberId MemberId, ProjectSecurityId ProjectId)
        {
            return new ProjectSecurity(Id, MemberId, ProjectId);
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
