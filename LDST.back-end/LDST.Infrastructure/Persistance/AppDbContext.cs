using LDST.Domain.EFModels;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Reflection.Metadata;

namespace LDST.Infrastructure.Persistance;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
        Database.Migrate();
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Playground> Playgrounds { get; set; }
    public DbSet<Guest> Guests { get; set; }
    public DbSet<Bill> Bills { get; set; }
    public DbSet<GameReservation> GameReservations { get; set; }
    public DbSet<GameTimeslot> GameTimeslots { get; set; }
    public DbSet<Host> Hosts { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Sport> Sports { get; set; }
    public DbSet<CitySport> CitySports { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
