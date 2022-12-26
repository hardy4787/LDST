using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using LDST.Domain.EFModels;
using LDST.Infrastructure.Persistance.Constants;

namespace LDST.Infrastructure.Persistance.Configurations;

internal sealed class GameTimeSlotConfiguration : IEntityTypeConfiguration<GameTimeSlotEntity>
{
    public void Configure(EntityTypeBuilder<GameTimeSlotEntity> builder)
    {
        builder.ToTable(TableNames.GameTimeSlots);

        builder.HasKey(x => x.Id);

        builder
            .HasOne(a => a.GameReservation)
            .WithOne(b => b.GameTimeSlot)
            .HasForeignKey<GameReservationEntity>(b => b.GameTimeSlotId);
    }
}
