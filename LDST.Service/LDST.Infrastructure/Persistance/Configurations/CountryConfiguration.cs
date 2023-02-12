using LDST.Domain.EFModels;
using LDST.Infrastructure.Persistance.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LDST.Infrastructure.Persistance.Configurations;

internal sealed class CountryConfiguration : IEntityTypeConfiguration<CountryEntity>
{
    public void Configure(EntityTypeBuilder<CountryEntity> builder)
    {
        builder.ToTable(TableNames.Countries);

        builder.HasKey(x => x.Id);

        builder.Property(e => e.Name)
            .HasMaxLength(50);
    }
}
