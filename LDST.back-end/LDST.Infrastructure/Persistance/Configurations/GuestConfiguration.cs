using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LDST.Domain.EFModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Reflection.Emit;
using LDST.Infrastructure.Persistance.Constants;

namespace LDST.Infrastructure.Persistance.Configurations;

internal sealed class GuestConfiguration : IEntityTypeConfiguration<Guest>
{
    public void Configure(EntityTypeBuilder<Guest> builder)
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
