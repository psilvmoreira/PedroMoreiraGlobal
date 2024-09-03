using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PedroMoreira.Domain.Authentication.Aggregates;
using PedroMoreira.Domain.Authentication.ValueObjects;
using PedroMoreira.Domain.ErrorMessages;
using PedroMoreira.Domain.Members.Entities;
using PedroMoreira.Domain.Members.ValueObjects;
using PedroMoreira.Domain.Projects.ValueObjects;

namespace PedroMoreira.Infrastructure.Persistence.Configurations
{
    public class ProjectSecurityEntityTypeConfiguration
        : IEntityTypeConfiguration<ProjectSecurity>
    {
        public void Configure(EntityTypeBuilder<ProjectSecurity> builder)
        {
            builder.Property(prop => prop.Id)
                .HasConversion(
                    id => id.value,
                    value => ProjectSecurityId.Create(value).Value);
            
            builder.Property(prop => prop.MemberId)
                .HasConversion(
                    id => id.value,
                    value => MemberId.Create(value).Value);
            
            builder.Property(prop => prop.ProjectId)
                .HasConversion(
                    id => id.Value,
                    value => ProjectId.Create(value).Value);
            
            builder
                .HasIndex(projMem => new { projMem.ProjectId, projMem.MemberId })
                .IsUnique(true);

            builder
                .HasOne(memb => memb.Member)
                .WithMany(projSec => projSec.ProjectSecurity)
                .HasForeignKey(forkey => forkey.MemberId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder
                .HasOne(proj => proj.Project)
                .WithMany(projSec => projSec.ProjectSecurity)
                .HasForeignKey(forkey => forkey.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder
                .HasMany(claim => claim.Claims)
                .WithOne(projSec => projSec.ProjectSecurity)
                .HasForeignKey(forkey => forkey.ProjectSecurityId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(roles => roles.Roles)
                .WithOne(proj => proj.ProjectSecurity)
                .HasForeignKey(forkey => forkey.ProjectSecurityId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
