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

internal sealed class GameTimeslotConfiguration : IEntityTypeConfiguration<GameTimeslot>
{
    public void Configure(EntityTypeBuilder<GameTimeslot> builder)
    {
        builder.ToTable(TableNames.GameTimeslots);

        builder.HasKey(x => x.Id);

        builder
            .HasOne(a => a.GameReservation)
            .WithOne(b => b.GameTimeslot)
            .HasForeignKey<GameReservation>(b => b.GameTimeslotId);
    }
}
