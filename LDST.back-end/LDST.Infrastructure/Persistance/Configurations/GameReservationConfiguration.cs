using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using LDST.Domain.EFModels;
using LDST.Infrastructure.Persistance.Constants;

namespace LDST.Infrastructure.Persistance.Configurations;

internal sealed class GameReservationConfiguration : IEntityTypeConfiguration<GameReservationEntity>
{
    public void Configure(EntityTypeBuilder<GameReservationEntity> builder)
    {
        builder.ToTable(TableNames.GameReservations);

        builder.HasKey(x => x.Id);

        builder
            .HasOne(a => a.Bill)
            .WithOne(b => b.GameReservation)
            .HasForeignKey<BillEntity>(b => b.GameReservationId);
    }
}
