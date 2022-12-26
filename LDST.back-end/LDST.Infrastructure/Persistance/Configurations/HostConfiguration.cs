using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using LDST.Domain.EFModels;
using LDST.Infrastructure.Persistance.Constants;

namespace LDST.Infrastructure.Persistance.Configurations;

internal sealed class HostConfiguration : IEntityTypeConfiguration<HostEntity>
{
    public void Configure(EntityTypeBuilder<HostEntity> builder)
    {
        builder.ToTable(TableNames.Hosts);

        builder.HasKey(x => x.Id);

        builder
            .HasMany(a => a.Playgrounds)
            .WithOne(b => b.Host);
    }
}
