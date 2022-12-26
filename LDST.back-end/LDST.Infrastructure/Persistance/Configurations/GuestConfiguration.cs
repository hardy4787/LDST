using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using LDST.Domain.EFModels;
using LDST.Infrastructure.Persistance.Constants;

namespace LDST.Infrastructure.Persistance.Configurations;

internal sealed class GuestConfiguration : IEntityTypeConfiguration<GuestEntity>
{
    public void Configure(EntityTypeBuilder<GuestEntity> builder)
    {
        builder.ToTable(TableNames.Guests);

        builder.HasKey(x => x.Id);

        builder
            .HasMany(c => c.Bills)
            .WithOne(e => e.Guest);

        builder
            .HasMany(x => x.PlaygroundGuestRatings)
            .WithOne(x => x.Guest);

        builder
            .HasMany(x => x.GameReservations)
            .WithOne(x => x.Guest);
    }
}
