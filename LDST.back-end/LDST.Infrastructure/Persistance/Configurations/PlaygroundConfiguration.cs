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

internal sealed class PlaygroundConfiguration : IEntityTypeConfiguration<Playground>
{
    public void Configure(EntityTypeBuilder<Playground> builder)
    {
        builder.ToTable(TableNames.Playgrounds);

        builder.HasKey(x => x.Id);

        builder
            .HasMany(x => x.PlaygroundGuestRatings)
            .WithOne(x => x.Playground);

        builder
            .HasMany(x => x.GameTimeslots)
            .WithOne(x => x.Playground);
    }
}
