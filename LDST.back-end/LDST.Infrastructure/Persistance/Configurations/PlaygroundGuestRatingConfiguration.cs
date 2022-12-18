using LDST.Domain.EFModels;
using LDST.Infrastructure.Persistance.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDST.Infrastructure.Persistance.Configurations;

internal sealed class PlaygroundGuestRatingConfiguration : IEntityTypeConfiguration<PlaygroundGuestRating>
{
    public void Configure(EntityTypeBuilder<PlaygroundGuestRating> builder)
    {
        builder.ToTable(TableNames.PlaygroundGuestRatings);
    }
}