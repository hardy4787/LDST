using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using LDST.Domain.EFModels;
using LDST.Infrastructure.Persistance.Constants;

namespace LDST.Infrastructure.Persistance.Configurations;

internal sealed class PlaygroundConfiguration : IEntityTypeConfiguration<PlaygroundEntity>
{
    public void Configure(EntityTypeBuilder<PlaygroundEntity> builder)
    {
        builder.ToTable(TableNames.Playgrounds);

        builder.HasKey(x => x.Id);

        builder
            .HasMany(x => x.PlaygroundGuestRatings)
            .WithOne(x => x.Playground);

        builder
            .HasMany(x => x.GameTimeSlots)
            .WithOne(x => x.Playground);
    }
}
