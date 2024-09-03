using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PedroMoreira.Domain.Authentication.Entities;
using PedroMoreira.Domain.Authentication.ValueObjects;

namespace PedroMoreira.Infrastructure.Persistence.Configurations;

public class ProjectSecurityRoleConfiguration
: IEntityTypeConfiguration<ProjectSecurityRole>
{
    public void Configure(EntityTypeBuilder<ProjectSecurityRole> builder)
    {
        builder.Property(x => x.Id)
            .HasConversion(
                id => id.Value,
                value => ProjectSecurityRoleId.Create(value).Value
            );
        
        builder
            .HasIndex(projmem => new { projmem.Id });
        
        builder
            .HasOne(proj => proj.ProjectSecurity)
            .WithMany(projRoles => projRoles.Roles)
            .HasForeignKey(fkey => fkey.ProjectSecurityId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}