using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using LDST.Domain.EFModels;
using LDST.Infrastructure.Persistance.Constants;

namespace LDST.Infrastructure.Persistance.Configurations;

internal sealed class BillConfiguration : IEntityTypeConfiguration<BillEntity>
{
    public void Configure(EntityTypeBuilder<BillEntity> builder)
    {
        builder.ToTable(TableNames.Bills);

        builder.HasKey(x => x.Id);
    }
}
