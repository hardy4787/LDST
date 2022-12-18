using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LDST.Domain.EFModels;
using LDST.Infrastructure.Persistance.Constants;

namespace LDST.Infrastructure.Persistance.Configurations;

internal sealed class GameReservationConfiguration : IEntityTypeConfiguration<GameReservation>
{
    public void Configure(EntityTypeBuilder<GameReservation> builder)
    {
        builder.ToTable(TableNames.GameResarvations);

        builder.HasKey(x => x.Id);

        builder
            .HasOne(a => a.Bill)
            .WithOne(b => b.GameReservation)
            .HasForeignKey<Bill>(b => b.GameReservationId);
    }
}
