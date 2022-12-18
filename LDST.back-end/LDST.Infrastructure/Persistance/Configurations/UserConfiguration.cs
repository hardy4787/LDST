using LDST.Domain.EFModels;
using LDST.Infrastructure.Persistance.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace LDST.Infrastructure.Persistance.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(TableNames.Users);

        builder.HasKey(x => x.Id);

        builder
            .HasOne(a => a.Guest)
            .WithOne(b => b.User)
            .HasForeignKey<Guest>(b => b.UserId);

        builder
            .HasOne(a => a.Host)
            .WithOne(b => b.User)
            .HasForeignKey<Host>(b => b.UserId);
    }
}
