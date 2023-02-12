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

        builder.Property(e => e.FirstName)
            .HasMaxLength(50);

        builder.Property(e => e.FirstName)
            .HasMaxLength(50);

        builder
            .HasMany(c => c.Bills)
            .WithOne(e => e.Guest);

        builder
            .HasMany(x => x.PlaygroundGuestRatings)
            .WithOne(x => x.Guest);

        builder
            .HasMany(x => x.GameReservations)
            .WithOne(x => x.Guest);

        builder
            .HasMany(a => a.Playgrounds)
            .WithOne(b => b.Host);
    }
}
