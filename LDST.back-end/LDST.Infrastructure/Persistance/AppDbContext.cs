using LDST.Application.Interfaces.Persistance;
using LDST.Domain.EFModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace LDST.Infrastructure.Persistance;

public sealed class AppDbContext : IdentityDbContext, IAppDbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
        Database.Migrate();
    }

    public new DbSet<UserEntity> Users { get; set; }
    public DbSet<PlaygroundEntity> Playgrounds { get; set; }
    public DbSet<BillEntity> Bills { get; set; }
    public DbSet<GameReservationEntity> GameReservations { get; set; }
    public DbSet<GameTimeSlotEntity> GameTimeSlots { get; set; }
    public DbSet<CityEnity> Cities { get; set; }
    public DbSet<CountryEntity> Countries { get; set; }
    public DbSet<SportEntity> Sports { get; set; }
    public DbSet<CitySportEntity> CitySports { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
    }
}
