using LDST.Domain.EFModels;
using LDST.Infrastructure.Persistance.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LDST.Infrastructure.Persistance.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.ToTable(TableNames.Users);

        builder.HasKey(x => x.Id);

        builder.Property(e => e.FirstName)
            .HasMaxLength(50);

        builder.Property(e => e.FirstName)
            .HasMaxLength(50);

        builder
            .HasOne(a => a.Guest)
            .WithOne(b => b.User)
            .HasForeignKey<GuestEntity>(b => b.UserId);

        builder
            .HasOne(a => a.Host)
            .WithOne(b => b.User)
            .HasForeignKey<HostEntity>(b => b.UserId);
    }
}
