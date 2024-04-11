using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PedroMoreira.Domain.Members.Entities;

namespace PedroMoreira.Infrastructure.Persistence.Configurations
{
    public class MemberEntityTypeConfiguration : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder
                .HasOne(MenPro => MenPro.Profile)
                .WithOne(memb => memb.User)
                .HasForeignKey<MemberProfile>(b => b.MemberId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(Utok => Utok.UserTokens)
                .WithOne(memb => memb.Member)
                .HasForeignKey(forkey => forkey.MemberId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(projSec => projSec.ProjectSecurity)
                .WithOne(memb => memb.Member)
                .HasForeignKey(forkey => forkey.MemberId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
