using LDST.Domain.EFModels;
using LDST.Infrastructure.Persistance.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LDST.Infrastructure.Persistance.Configurations;

internal sealed class WeekScheduleConfiguration : IEntityTypeConfiguration<WeekScheduleEntity>
{
    public void Configure(EntityTypeBuilder<WeekScheduleEntity> builder)
    {
        builder.ToTable(TableNames.WeekSchedules);

        builder.HasKey(x => x.Id);
    }
}
