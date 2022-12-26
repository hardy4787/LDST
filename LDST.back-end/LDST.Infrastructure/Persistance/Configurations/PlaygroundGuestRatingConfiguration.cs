using LDST.Domain.EFModels;
using LDST.Infrastructure.Persistance.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LDST.Infrastructure.Persistance.Configurations;

internal sealed class PlaygroundGuestRatingConfiguration : IEntityTypeConfiguration<PlaygroundGuestRatingEntity>
{
    public void Configure(EntityTypeBuilder<PlaygroundGuestRatingEntity> builder)
    {
        builder.ToTable(TableNames.PlaygroundGuestRatings);
    }
}