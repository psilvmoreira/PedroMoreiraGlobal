using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PedroMoreira.Domain.Projects.Entities;

namespace PedroMoreira.Infrastructure.Persistence.Configurations
{
    public class ProjectEntityTypeConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder
                .HasMany(projeSec => projeSec.ProjectSecurity)
                .WithOne(proj => proj.Project)
                .HasForeignKey(forkey => forkey.ProjectId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}
