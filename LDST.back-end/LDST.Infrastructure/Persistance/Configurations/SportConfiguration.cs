using LDST.Domain.EFModels;
using LDST.Infrastructure.Persistance.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LDST.Infrastructure.Persistance.Configurations;

internal sealed class SportConfiguration : IEntityTypeConfiguration<SportEntity>
{
    public void Configure(EntityTypeBuilder<SportEntity> builder)
    {
        builder.ToTable(TableNames.Sports);

        builder.HasKey(x => x.Id);

        builder.HasAlternateKey(x => x.Name);
    }
}
