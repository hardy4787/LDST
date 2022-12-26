using LDST.Application.Interfaces.Persistance;
using LDST.Domain.EFModels;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace LDST.Infrastructure.Persistance;

public sealed class AppDbContext : DbContext, IAppDbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
        Database.Migrate();
    }

    public DbSet<UserEntity> Users { get; set; }
    public DbSet<PlaygroundEntity> Playgrounds { get; set; }
    public DbSet<GuestEntity> Guests { get; set; }
    public DbSet<BillEntity> Bills { get; set; }
    public DbSet<GameReservationEntity> GameReservations { get; set; }
    public DbSet<GameTimeSlotEntity> GameTimeSlots { get; set; }
    public DbSet<HostEntity> Hosts { get; set; }
    public DbSet<CityEnity> Cities { get; set; }
    public DbSet<CountryEntity> Countries { get; set; }
    public DbSet<SportEntity> Sports { get; set; }
    public DbSet<CitySportEntity> CitySports { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
}
