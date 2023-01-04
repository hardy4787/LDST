using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using LDST.Domain.EFModels;
using LDST.Infrastructure.Persistance.Constants;
using Microsoft.Extensions.Logging;
using System.Reflection.Emit;

namespace LDST.Infrastructure.Persistance.Configurations;

internal sealed class PlaygroundConfiguration : IEntityTypeConfiguration<PlaygroundEntity>
{
    public void Configure(EntityTypeBuilder<PlaygroundEntity> builder)
    {
        builder.ToTable(TableNames.Playgrounds);

        builder.HasKey(x => x.Id);

        builder
            .HasOne(a => a.WeekSchedule)
            .WithOne(b => b.Playground)
            .HasForeignKey<WeekScheduleEntity>(b => b.PlaygroundId);

        builder
            .HasMany(x => x.PlaygroundGuestRatings)
            .WithOne(x => x.Playground);

        builder
            .HasMany(x => x.GameTimeSlots)
            .WithOne(x => x.Playground);
    }
}
