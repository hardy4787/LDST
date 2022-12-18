using LDST.Domain.EFModels;
using LDST.Infrastructure.Persistance.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LDST.Infrastructure.Persistance.Configurations;

internal sealed class CitySportConfiguration : IEntityTypeConfiguration<CitySport>
{
    public void Configure(EntityTypeBuilder<CitySport> builder)
    {
        builder.ToTable(TableNames.CitySports);

        builder.HasKey(x => new { x.CityId, x.SportId });

        builder
            .HasOne(x => x.Sport)
            .WithMany(x => x.CitySports)
            .HasForeignKey(x => x.SportId);

        builder
            .HasOne(x => x.City)
            .WithMany(x => x.CitySports)
            .HasForeignKey(x => x.CityId);
    }
}
