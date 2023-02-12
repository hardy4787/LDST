using LDST.Domain.EFModels;
using LDST.Infrastructure.Persistance.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LDST.Infrastructure.Persistance.Configurations;

internal sealed class CityConfiguration : IEntityTypeConfiguration<CityEnity>
{
    public void Configure(EntityTypeBuilder<CityEnity> builder)
    {
        builder.ToTable(TableNames.Cities);
        builder.HasKey(x => x.Id);

        builder.HasAlternateKey(x => x.Name);

        builder.Property(e => e.Name)
            .HasMaxLength(50);
    }
}
