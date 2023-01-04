using LDST.Domain.EFModels;
using LDST.Infrastructure.Persistance.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LDST.Infrastructure.Persistance.Configurations;
internal sealed class DayScheduleConfiguration : IEntityTypeConfiguration<DayScheduleEntity>
{
    public void Configure(EntityTypeBuilder<DayScheduleEntity> builder)
    {
        builder.ToTable(TableNames.DaySchedules);

        builder.HasKey(x => x.Id);

        builder.Property(e => e.OpeningTime)
            .HasColumnType("time");

        builder.Property(e => e.ClosingTime)
            .HasColumnType("time");
    }
}
